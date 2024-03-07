using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using phoneApi.Models.Domain;
using phoneApi.Models.Dto;
using phoneApi.Models.Res;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace phoneApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> signInMAnager;

        public AccountController(DatabaseContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInMAnager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService
            )
        {
            this._context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._tokenService = tokenService;
             this.signInMAnager = signInMAnager;
        }

        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        //{
        //    var status = new Status();
        //    // check validations
        //    if (!ModelState.IsValid)
        //    {
        //        status.StatusCode = 0;
        //        status.Message = "please pass all the valid fields";
        //        return Ok(status);
        //    }
        //    // lets find the user
        //    var user = await userManager.FindByNameAsync(model.Username);
        //    if (user is null)
        //    {
        //        status.StatusCode = 0;
        //        status.Message = "invalid username";
        //        return Ok(status);
        //    }
        //    // check current password
        //    if (!await userManager.CheckPasswordAsync(user, model.CurrentPassword))
        //    {
        //        status.StatusCode = 0;
        //        status.Message = "invalid current password";
        //        return Ok(status);
        //    }

        //    // change password here
        //    var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //    if (!result.Succeeded)
        //    {
        //        status.StatusCode = 0;
        //        status.Message = "Failed to change password";
        //        return Ok(status);
        //    }
        //    status.StatusCode = 1;
        //    status.Message = "Password has changed successfully";
        //    return Ok(result);
        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = _tokenService.GetToken(authClaims);
                var refreshToken = _tokenService.GetRefreshToken();
                var tokenInfo = _context.TokenInfo.FirstOrDefault(a => a.Usernamee == user.UserName);
                if (tokenInfo == null)
                {
                    var info = new TokenInfo
                    {
                        Usernamee = user.UserName,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.Now.AddDays(1)
                       

                    };
                    _context.TokenInfo.Add(info);

                }

                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(1);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(new LoginResponse
                {
                    Name = user.Name,
                    Username = user.UserName,
                    Token = token.TokenString,
                    Email = user.Email,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    UserId = user.Id,
                    StatusCode = 1,
                    Message = "Logged in",
                    role = userRoles.FirstOrDefault(),

                }); 

            }
            //login failed condition

            return Ok(
                new LoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid Username or Password",
                    Token = "",
                    Expiration = null
                });
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationModel model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }
            //check if user exists
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return Ok(status);
            }
            //check if Email exists
            var EmailExists = await userManager.FindByEmailAsync(model.Email);
            if (EmailExists != null)
            {

                status.StatusCode = 0;
                status.Message = "Invalid Email";
                return Ok(status);
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                Name = model.Name
            };
            // create a user here
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return Ok(status);
            }

            // add roles here
            // for admin registration UserRoles.Admin instead of UserRoles.Roles
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
            status.StatusCode = 1;
            status.Message = "Sucessfully registered";
            return Ok(status);

        }

        // after registering admin we will comment this code, because i want only one admin in this application
        //[HttpPost]
        //public async Task<IActionResult> RegistrationAdmin([FromBody] RegistrationModel model)
        //{
        //    var status = new Status();
        //    if (!ModelState.IsValid)
        //    {
        //        status.StatusCode = 0;
        //        status.Message = "Please pass all the required fields";
        //        return Ok(status);
        //    }
        //    // check if user exists
        //    var userExists = await userManager.FindByNameAsync(model.Username);
        //    if (userExists != null)
        //    {
        //        status.StatusCode = 0;
        //        status.Message = "Invalid username";
        //        return Ok(status);
        //    }
        //    var user = new ApplicationUser
        //    {
        //        UserName = model.Username,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        Email = model.Email,
        //        Name = model.Name
        //    };
        //    // create a user here
        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //    {
        //        status.StatusCode = 0;
        //        status.Message = "User creation failed";
        //        return Ok(status);
        //    }

        //    // add roles here
        //    // for admin registration UserRoles.Admin instead of UserRoles.Roles
        //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

        //    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await userManager.AddToRoleAsync(user, UserRoles.Admin);
        //    }
        //    status.StatusCode = 1;
        //    status.Message = "Sucessfully registered";
        //    return Ok(status);

        //}


        //[HttpPost("logout")]
        //public async Task<IActionResult> logout()
        //{
        //    var status = new Status();
        //    await signInMAnager.SignOutAsync();

        //    status.StatusCode = 300;
        //    status.Message = "logout Sucessfully";
        //    return Ok(status);

        //}
        [HttpPost("RegistrationtoAdmin")]
        public async Task<IActionResult> RegistrationtoAmin([FromBody] RegistrationModel model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return Ok(status);
            }
            //check if user exists
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return Ok(status);
            }
            //check if Email exists
            var EmailExists = await userManager.FindByEmailAsync(model.Email);
            if (EmailExists != null)
            {

                status.StatusCode = 0;
                status.Message = "Invalid Email";
                return Ok(status);
            }
            var Admin = new ApplicationUser
            {
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                Name = model.Name,
                
                
            };
            // create a user here
            var result = await userManager.CreateAsync(Admin, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return Ok(status);
            }

            // add roles here
            // for admin registration UserRoles.Admin instead of UserRoles.Roles
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(Admin, UserRoles.Admin);
            }
            status.StatusCode = 1;
            status.Message = "Sucessfully registered TO Admin";
            return Ok(status);

        }
    }
}