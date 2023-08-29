using MediatR;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.Billing.Handlers;

public class GetSubscriptionByIdHandler: IRequestHandler<GetSubscriptionByIdRequest, SubscriptionDto>
{
    private readonly IRetrieve<Guid, Subscription> _repository;

    public GetSubscriptionByIdHandler(IRetrieve<Guid, Subscription> repository) => 
        _repository = repository;

    public async Task<SubscriptionDto> Handle(GetSubscriptionByIdRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.TryRetrieveAsync(request.Id);
        return item == null ? throw new SubscriptionNotFoundException(request.Id) : SubscriptionDto.From(item);
    }
}