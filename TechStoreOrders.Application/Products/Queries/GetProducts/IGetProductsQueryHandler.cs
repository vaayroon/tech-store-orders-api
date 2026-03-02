using TechStoreOrders.Application.Products.Dtos;

namespace TechStoreOrders.Application.Products.Queries.GetProducts;

public interface IGetProductsQueryHandler
{
    Task<ProductListDto> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken = default);
}
