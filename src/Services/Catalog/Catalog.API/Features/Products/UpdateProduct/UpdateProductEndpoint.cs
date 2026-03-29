using Catalog.API.Features.Products.CreateProduct;

namespace Catalog.API.Features.Products.UpdateProduct
{
    public record UpdateProudctRequest(Guid Id, string Name, List<string> Catagory, string Description, string imageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPut("/products", async(UpdateProudctRequest request , ISender sender) =>
           {
               var command = request.Adapt<UpdateProudctCommand>();

               var result = await sender.Send(command);

               var response = result.Adapt<UpdateProductResponse>();

               Results.Ok(response);
           }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
