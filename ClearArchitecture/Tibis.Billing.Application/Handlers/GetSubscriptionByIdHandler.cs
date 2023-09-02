using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;
using Tibis.Billing.Domain;
using Tibis.Billing.Domain.Exceptions;
using Tibis.Contracts.Interfaces;

namespace Tibis.Billing.Application.Handlers;

internal class GetSubscriptionByIdHandler: IRequestHandler<GetSubscriptionByIdRequest, SubscriptionDto>
{
    private readonly IRetrieve<Guid, Subscription> _repository;

    public GetSubscriptionByIdHandler(IRetrieve<Guid, Subscription> repository) => 
        _repository = repository;

    public async Task<SubscriptionDto> Handle(GetSubscriptionByIdRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.Id);
        return item == null ? throw new SubscriptionNotFoundException(request.Id) : item.ToDto();
    }
}