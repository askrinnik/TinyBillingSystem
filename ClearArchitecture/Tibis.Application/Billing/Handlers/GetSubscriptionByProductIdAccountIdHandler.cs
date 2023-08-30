using MediatR;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.Billing.Handlers;

public class GetSubscriptionByProductIdAccountIdHandler : IRequestHandler<GetSubscriptionByProductIdAccountIdRequest, SubscriptionDto>
{
    private readonly IRetrieve<Guid, Guid, Subscription> _repository;

    public GetSubscriptionByProductIdAccountIdHandler(IRetrieve<Guid, Guid, Subscription> repository) => 
        _repository = repository;

    public async Task<SubscriptionDto> Handle(GetSubscriptionByProductIdAccountIdRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.ProductId, request.AccountId);
        return item == null ? throw new SubscriptionNotFoundException(request.ProductId, request.AccountId) : SubscriptionDto.From(item);
    }
}