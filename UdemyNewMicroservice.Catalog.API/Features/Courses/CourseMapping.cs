using AutoMapper;
using UdemyNewMicroservice.Catalog.API.Features.Courses.Create;


namespace UdemyNewMicroservice.Catalog.API.Features.Courses
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
        }
        
    }
}
