using MediatR;
using Tibis.Application.AccountManagement;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;
using Tibis.Application.Billing.Services;
using Tibis.Application.ProductManagement;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.Billing.Handlers;

public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionRequest, SubscriptionDto>
{
    private readonly ICreate<Subscription> _repository;
    private readonly IProductClient _productClient;
    private readonly IAccountClient _accountClient;

    public CreateSubscriptionHandler(ICreate<Subscription> repository, IProductClient productClient, IAccountClient accountClient)
    {
        _repository = repository;
        _productClient = productClient;
        _accountClient = accountClient;
    }

    public async Task<SubscriptionDto> Handle(CreateSubscriptionRequest request, CancellationToken cancellationToken)
    {
        _ = await _productClient.GetProductAsync(request.ProductId) ?? throw new ProductNotFoundException(request.ProductId);
        _ = await _accountClient.GetAccountAsync(request.AccountId) ?? throw new AccountNotFoundException(request.AccountId);

        var item = await _repository.CreateAsync(new(request.ProductId, request.AccountId));
        return SubscriptionDto.From(item);
    }
}