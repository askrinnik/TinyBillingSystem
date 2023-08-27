using MediatR;
using Tibis.Application.ProductManagement.Models;
using Tibis.Application.ProductManagement.Queries;
using Tibis.Domain.Interfaces;
using Tibis.Domain.ProductManagement;

namespace Tibis.Application.ProductManagement.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, IEnumerable<ProductDto>>
{
    private readonly IRetrieveMany<Product> _repository;

    public GetAllProductsHandler(IRetrieveMany<Product> repository) => 
        _repository = repository;

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken) =>
        await _repository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(ProductDto.From(item)))
            .ToArrayAsync(cancellationToken);
}