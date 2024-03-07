using phoneApi.Models.Domain;

namespace phoneApi.Models.Res
{
    public interface IHomeRes
    {
        List<Product> Search(string item = "", int genreId = 0);
        List<Category> GetAllCategory();
        List<Product> GetAllProduct();
    }
}