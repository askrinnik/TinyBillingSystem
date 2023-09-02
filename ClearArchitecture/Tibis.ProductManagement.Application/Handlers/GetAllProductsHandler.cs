using MediatR;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;
using Tibis.ProductManagement.Domain;

namespace Tibis.ProductManagement.Application.Handlers;

internal class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, IEnumerable<ProductDto>>
{
    private readonly IRetrieveMany<Product> _repository;

    public GetAllProductsHandler(IRetrieveMany<Product> repository) => 
        _repository = repository;

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken) =>
        await _repository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(item.ToDto()))
            .ToArrayAsync(cancellationToken);
}