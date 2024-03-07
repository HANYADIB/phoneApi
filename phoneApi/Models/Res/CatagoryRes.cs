using Microsoft.EntityFrameworkCore;
using phoneApi.Models.Domain;
using phoneApi.Models.DTO;
using System.Linq;
using System.Security.Cryptography;

namespace phoneApi.Models.Res
{
    public class CatagoryRes : ICatagoryRes
    {
        private readonly DatabaseContext context;

        public CatagoryRes(DatabaseContext _context)
        {
            context = _context;
        }
        public List<Category> Getall()
        {
            return context.Categories.Include(x => x.Products).ToList();
            //return context.Categories.Include(x => x.Products).Select(x => new CatagoryDto() 
            //{
            //    Cat_Id = x.Cat_Id,
            //    Namecat = x.Namecat,  
            //}).ToList();

        }
        public Category Getbyid(int id)
        {
            return context.Categories.Include(x => x.Products).FirstOrDefault(x => x.Cat_Id == id);
        }
        public void Adding(Category Item)
        {

            context.Categories.Add(Item);
            context.SaveChanges();
        }
        public void Delete(int Id)
        {
            // Product Item = Getbyid(Id);
            Category Item = context.Categories.FirstOrDefault(d => d.Cat_Id == Id);
            context.Categories.Remove(Item);
            context.SaveChanges();
        }
        public void Edite(CatagotyImage item, int Id)
        {
            Category olditem = Getbyid(Id);
            if (item.Img != null)
            {
                using var stream = new MemoryStream();
                item.Img.CopyToAsync(stream);
                olditem.Img = stream.ToArray();
            }
            olditem.Namecat = item.Namecat;
            
            context.SaveChanges();
        }
        public Category GetDetails(int Id)
        {
            return context.Categories.FirstOrDefault(c => c.Cat_Id == Id);
        }
    }
}