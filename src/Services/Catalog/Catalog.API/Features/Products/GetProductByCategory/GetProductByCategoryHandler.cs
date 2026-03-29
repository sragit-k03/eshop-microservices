using Marten.Linq.QueryHandlers;

namespace Catalog.API.Features.Products.GetProductByCategory
{
    public record GetProductByCatagoryQuery(string catagory) : IQuery<GetProductByCatagoryResult>;

        public record GetProductByCatagoryResult(IEnumerable<Product> product);
    public class GetProductByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCatagoryQuery, GetProductByCatagoryResult>
    {
        public async Task<GetProductByCatagoryResult> Handle(GetProductByCatagoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(x => x.Category.Contains(query.catagory)).ToListAsync();
            return new GetProductByCatagoryResult(products);
        }
    }
}
