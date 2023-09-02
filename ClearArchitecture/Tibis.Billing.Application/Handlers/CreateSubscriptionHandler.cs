using MediatR;
using Tibis.AccountManagement.CQRS.Requests;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;
using Tibis.Billing.Domain;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.Billing.Application.Handlers;

internal class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionRequest, SubscriptionDto>
{
    private readonly ICreate<Subscription> _subscriptionRepository;
    private readonly ICreate<AccountUsage> _accountUsageRepository;
    private readonly ISender _sender;

    public CreateSubscriptionHandler(ICreate<Subscription> subscriptionRepository, ICreate<AccountUsage> accountUsageRepository, ISender sender)
    {
        _subscriptionRepository = subscriptionRepository;
        _accountUsageRepository = accountUsageRepository;
        _sender = sender;
    }

    public async Task<SubscriptionDto> Handle(CreateSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var product = await _sender.Send(new GetProductByIdRequest(request.ProductId), cancellationToken);
        _ = await _sender.Send(new GetAccountByIdRequest(request.AccountId), cancellationToken);

        var subscription = await _subscriptionRepository.CreateAsync(new(request.ProductId, request.AccountId));

        if (product.ProductType == (int)ProductType.RecurringCharge)
            await _accountUsageRepository.CreateAsync(new(subscription.Id, DateTime.Now, product.Rate));

        return subscription.ToDto();
    }
}