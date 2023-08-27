using MediatR;
using Tibis.Application.ProductManagement.Models;
using Tibis.Application.ProductManagement.Queries;
using Tibis.Domain.ProductManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.ProductManagement.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductRequest, ProductDto>
{
    private readonly ICreate<Product> _repository;

    public CreateProductHandler(ICreate<Product> repository) =>
        _repository = repository;

    public async Task<ProductDto> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.CreateAsync(new(request.Name));
        return ProductDto.From(item);
    }
}