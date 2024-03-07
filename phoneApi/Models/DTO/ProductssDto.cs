namespace phoneApi.Models.DTO
{
    public class ProductssDto
    {
        public int Product_Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }
        public int Cat_Id { get; set; }
        public string Cat_Name { get; set; }
        public int Sup_Id { get; set; }
        public string Sub_Name { get; set; }
        public string Image { get; set; }
    }
}
