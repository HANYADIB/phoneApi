using AutoMapper;
using phoneApi.Models.Domain;
using phoneApi.Models.DTO;

namespace phoneApi.Mapping
{
    public class Map:Profile
    {
        public Map()
        {
            CreateMap<Product, ProductsDto>();
            CreateMap<Category, CatagoryDto>();
       //     CreateMap<proDto, Product>();

        }
    }
}
