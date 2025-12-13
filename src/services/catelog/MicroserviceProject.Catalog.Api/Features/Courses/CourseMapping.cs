using MicroserviceProject.Catalog.Api.Features.Courses.Create;

namespace MicroserviceProject.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
        }
    }
}
