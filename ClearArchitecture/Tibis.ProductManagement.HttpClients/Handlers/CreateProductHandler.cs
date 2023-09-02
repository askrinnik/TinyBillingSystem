using MediatR;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.ProductManagement.HttpClients.Handlers;

internal class CreateProductHandler : IRequestHandler<CreateProductRequest, ProductDto>
{
    private readonly IProductManagementClient _client;

    public CreateProductHandler(IProductManagementClient client) => _client = client;

    public async Task<ProductDto> Handle(CreateProductRequest request, CancellationToken cancellationToken) => 
        await _client.CreateProductAsync(new(request.Name, request.ProductType, request.Rate), cancellationToken);
}