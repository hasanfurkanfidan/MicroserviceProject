using MicroserviceProject.Catalog.Api.Features.Courses.Create;
using MicroserviceProject.Catalog.Api.Features.Courses.Dtos;
using MicroserviceProject.Catalog.Api.Features.Courses.Update;

namespace MicroserviceProject.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<UpdateCourseCommand, Course>();
        }
    }
}
