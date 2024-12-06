using AutoMapper;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Create;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Dto;


namespace UdemyNewMicroservice.Catalog.API.Features.Courses
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
        
    }
}
