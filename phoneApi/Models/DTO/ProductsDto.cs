using phoneApi.Models.Domain;


namespace phoneApi.Models.DTO
{
    public class ProductsDto
    {
        public int Product_Id { get; set; }
      
        public string Name { get; set; }

        public double Price { get; set; }
        //public string? Image { get; set; }
       
        public string? Description { get; set; }
        public string Cat_Name { get; set; }
        public int Cat_Id { get; set; }
        public int Sup_Id { get; set; }
        public Category Categories { get; set; }

       // public int Category_Id { get; set; }
       // public string CategorysNamecat { get; set; }
       //public int Supplier_Id { get; set; }
       // public string SuppliersNamesup { get; set; }

    }
}
