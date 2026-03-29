namespace Catalog.API.Features.Products.UpdateProduct
{
    public record UpdateProudctCommand(Guid Id, string Name, List<string> Catagory, string Description, string imageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProudctCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty()
                
                .WithMessage("Name is required").Length(2,150).WithMessage("Name must be between 2 and 150 charectors");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Greater than 0");
        }
    }
    public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProudctCommand, UpdateProductResult>
    {
        public  async Task<UpdateProductResult> Handle(UpdateProudctCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            product.Name = command.Name;
            product.Category = command.Catagory;
            product.Description = command.Description;
            product.ImageFile = command.imageFile; 
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken); 

            return new UpdateProductResult(true);

        }
    }
}
