using MediatR;
using Tibis.Application.ProductManagement.Models;
using Tibis.Application.ProductManagement.Queries;
using Tibis.Domain.ProductManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.ProductManagement.Handlers;

public class GetProductByNameHandler: IRequestHandler<GetProductByNameRequest, ProductDto>
{
    private readonly IRetrieve<string, Product> _repository;

    public GetProductByNameHandler(IRetrieve<string, Product> repository) => 
        _repository = repository;

    public async Task<ProductDto> Handle(GetProductByNameRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.Name);
        return item == null ? throw new ProductNotFoundException(request.Name) : ProductDto.From(item);
    }
}