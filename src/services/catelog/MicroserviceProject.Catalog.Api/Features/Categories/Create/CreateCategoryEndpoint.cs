namespace MicroserviceProject.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand Command, IMediator mediator) =>
            {
                var result = await mediator.Send(Command);
                return result.ToGenericResult();
            }).WithName("CreateCategory").AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;
        }
    }
}
