using MediatR;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;
using Tibis.ProductManagement.Domain;
using Tibis.ProductManagement.Domain.Exceptions;

namespace Tibis.ProductManagement.Application.Handlers;

internal class GetProductByIdHandler: IRequestHandler<GetProductByIdRequest, ProductDto>
{
    private readonly IRetrieve<Guid, Product> _repository;

    public GetProductByIdHandler(IRetrieve<Guid, Product> repository) => 
        _repository = repository;

    public async Task<ProductDto> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.Id);
        return item == null ? throw new ProductNotFoundException(request.Id) : item.ToDto();
    }
}