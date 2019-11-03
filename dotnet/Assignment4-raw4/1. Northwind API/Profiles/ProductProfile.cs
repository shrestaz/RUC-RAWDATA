using _0._Models;
using _1._Northwind_API.Models;
using AutoMapper;

namespace _1._Northwind_API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductForCreation, Product>();
        }
    }
}
