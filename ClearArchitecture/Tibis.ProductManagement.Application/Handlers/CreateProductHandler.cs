using MediatR;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;
using Tibis.ProductManagement.Domain;

namespace Tibis.ProductManagement.Application.Handlers;

internal class CreateProductHandler : IRequestHandler<CreateProductRequest, ProductDto>
{
    private readonly ICreate<Product> _repository;

    public CreateProductHandler(ICreate<Product> repository) =>
        _repository = repository;

    public async Task<ProductDto> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.CreateAsync(new(request.Name, (Domain.ProductType)request.ProductType, request.Rate));
        return item.ToDto();
    }
}