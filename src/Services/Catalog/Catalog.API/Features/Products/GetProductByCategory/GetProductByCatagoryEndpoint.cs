using Catalog.API.Features.Products.GetProduct;

namespace Catalog.API.Features.Products.GetProductByCategory
{
    public record GetProductByCategoryRequest();

    public record GetProductByCategoryResponse(Product product);
    public class GetProductByCatagoryEndpoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/catagory/{category}",async(string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCatagoryQuery(category));
                var response = result.Adapt<GetProductByCategoryResponse>(); 
                
                return Results.Ok(response);
            })
             .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product ByCategory")
            .WithDescription("Get Product By Category");
        }
    }
}
