using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using phoneApi.Models.Domain;
using phoneApi.Models.Res;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.
//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(option => option.SerializerSettings.
 ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
builder.Services.AddSwaggerGen(con =>
        { con.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
            securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Enter the Bearer Authorization:`Bearer Generated-JWT-Token` "
    
    } );
         con.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                    }
                    },
                    new string[] { }
                    }
                });
});
//builder.Services.AddSwaggerGen(swagger =>
//{
//    //This is to generate the Default UI of Swagger Documentation    
//    swagger.SwaggerDoc("v2", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "ASP.NET 5 Web API",
//        Description = " ITI Projrcy"
//    });

//    // To Enable authorization using Swagger (JWT)    
//    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
//    });
//    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                    new OpenApiSecurityScheme
//                    {
//                    Reference = new OpenApiReference
//                    {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                    }
//                    },
//                    new string[] {}
//                    }
//                });
//});




builder.Services.AddTransient<IHomeRes, HomeRes>();
builder.Services.AddTransient<ICartRes, CartRes>();
builder.Services.AddTransient<ISupplierRes, SupplierRes>();
builder.Services.AddTransient<IProductRes, ProductRes>();
builder.Services.AddTransient<ICatagoryRes, CatagoryRes>();
builder.Services.AddTransient<IUserOrderRes, UserOrderRes>();

builder.Services.AddTransient<ITokenService, TokenService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors(options =>
            options.WithOrigins("*").
            AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
