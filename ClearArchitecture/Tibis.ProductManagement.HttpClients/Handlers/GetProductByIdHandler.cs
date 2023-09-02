using MediatR;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.ProductManagement.HttpClients.Handlers;

internal class GetProductByIdHandler: IRequestHandler<GetProductByIdRequest, ProductDto>
{
    private readonly IProductManagementClient _client;

    public GetProductByIdHandler(IProductManagementClient client) => _client = client;

    public async Task<ProductDto> Handle(GetProductByIdRequest request, CancellationToken cancellationToken) => 
        await _client.GetProductAsync(request.Id, cancellationToken);
}