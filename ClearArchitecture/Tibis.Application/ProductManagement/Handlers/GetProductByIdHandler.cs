using MediatR;
using Tibis.Application.ProductManagement.Models;
using Tibis.Application.ProductManagement.Queries;
using Tibis.Domain.ProductManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.ProductManagement.Handlers
{
    public class GetProductByIdHandler: IRequestHandler<GetProductByIdRequest, ProductDto>
    {
        private readonly IRetrieve<Guid, Product> _repository;

        public GetProductByIdHandler(IRetrieve<Guid, Product> repository) => 
            _repository = repository;

        public async Task<ProductDto> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _repository.TryRetrieveAsync(request.Id);
            return item == null ? throw new ProductNotFoundException(request.Id) : ProductDto.From(item);
        }
    }
}
