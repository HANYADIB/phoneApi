using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Models.Res
{
    public interface IProductRes
    {
        List<Product> Getall();
        public Product Getbyid(int Id);
        public Product GetbyNamebyId(string Name, int Product_Id=0);
        Task<Product> Adding(Product Item);
        public void Delete(int Id);
        public void Edite(proDto item, int Id);
        public Product GetDetails(int Id);
        List<Product> Search(string item = "", int catagorgsId = 0, int suppliersId = 0);
        List<Product> GetproductbycatategoryId(int Id);
    }
}