using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Models.Res
{
    public interface ICatagoryRes
    {
        public List<Category> Getall();
        public Category Getbyid(int id);
        public void Adding(Category Item);
        public void Delete(int Id);
        public void Edite(CatagotyImage item, int Id);
        Category GetDetails(int Id);


    }
}