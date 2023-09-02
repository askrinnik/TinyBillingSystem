using MediatR;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;
using Tibis.ProductManagement.Domain;
using Tibis.ProductManagement.Domain.Exceptions;

namespace Tibis.ProductManagement.Application.Handlers;

internal class GetProductByNameHandler: IRequestHandler<GetProductByNameRequest, ProductDto>
{
    private readonly IRetrieve<string, Product> _repository;

    public GetProductByNameHandler(IRetrieve<string, Product> repository) => 
        _repository = repository;

    public async Task<ProductDto> Handle(GetProductByNameRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.Name);
        return item == null ? throw new ProductNotFoundException(request.Name) :item.ToDto();
    }
}