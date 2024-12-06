using AutoMapper;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
