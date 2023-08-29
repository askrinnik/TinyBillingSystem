using MediatR;
using Tibis.Application.AccountManagement;
using Tibis.Application.AccountManagement.Services;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;
using Tibis.Application.ProductManagement;
using Tibis.Application.ProductManagement.Services;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;
using Tibis.Domain.ProductManagement;

namespace Tibis.Application.Billing.Handlers;

public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionRequest, SubscriptionDto>
{
    private readonly ICreate<Subscription> _subscriptionRepository;
    private readonly ICreate<AccountUsage> _accountUsageRepository;
    private readonly IProductClient _productClient;
    private readonly IAccountClient _accountClient;

    public CreateSubscriptionHandler(ICreate<Subscription> subscriptionRepository, IProductClient productClient, IAccountClient accountClient, ICreate<AccountUsage> accountUsageRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _productClient = productClient;
        _accountClient = accountClient;
        _accountUsageRepository = accountUsageRepository;
    }

    public async Task<SubscriptionDto> Handle(CreateSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var product = await _productClient.GetProductAsync(request.ProductId) ?? throw new ProductNotFoundException(request.ProductId);
        _ = await _accountClient.GetAccountAsync(request.AccountId) ?? throw new AccountNotFoundException(request.AccountId);

        var subscription = await _subscriptionRepository.CreateAsync(new(request.ProductId, request.AccountId));

        if (product.ProductType == (int)ProductType.RecurringCharge)
            await _accountUsageRepository.CreateAsync(new(subscription.Id, DateTime.Now, product.Rate));

        return SubscriptionDto.From(subscription);
    }
}