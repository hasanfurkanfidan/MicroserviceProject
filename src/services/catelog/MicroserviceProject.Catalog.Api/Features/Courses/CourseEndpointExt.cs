using Asp.Versioning.Builder;
using MicroserviceProject.Catalog.Api.Features.Courses.Create;
using MicroserviceProject.Catalog.Api.Features.Courses.Delete;
using MicroserviceProject.Catalog.Api.Features.Courses.GetAll;
using MicroserviceProject.Catalog.Api.Features.Courses.GetAllByUserId;
using MicroserviceProject.Catalog.Api.Features.Courses.GetById;
using MicroserviceProject.Catalog.Api.Features.Courses.Update;

namespace MicroserviceProject.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v:{version:apiversion}/courses").WithTags("Courses").WithApiVersionSet(apiVersionSet).CreateCourseGroupItemEndpoint().GetAllCourseGroupItemEndpoint().GetByIdCourseGroupItemEndpoint().UpdateCourseGroupItemEndpoint().DeleteCourseGroupItemEndpoint().GetAllCoursesByUserIdGroupItemEndpoint();
        }
    }
}
