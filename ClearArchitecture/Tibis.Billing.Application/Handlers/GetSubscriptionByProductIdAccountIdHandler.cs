using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;
using Tibis.Billing.Domain;
using Tibis.Billing.Domain.Exceptions;
using Tibis.Contracts.Interfaces;

namespace Tibis.Billing.Application.Handlers;

internal class GetSubscriptionByProductIdAccountIdHandler : IRequestHandler<GetSubscriptionByProductIdAccountIdRequest, SubscriptionDto>
{
    private readonly IRetrieve<Guid, Guid, Subscription> _repository;

    public GetSubscriptionByProductIdAccountIdHandler(IRetrieve<Guid, Guid, Subscription> repository) => 
        _repository = repository;

    public async Task<SubscriptionDto> Handle(GetSubscriptionByProductIdAccountIdRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.ProductId, request.AccountId);
        return item == null ? throw new SubscriptionNotFoundException(request.ProductId, request.AccountId) : item.ToDto();
    }
}