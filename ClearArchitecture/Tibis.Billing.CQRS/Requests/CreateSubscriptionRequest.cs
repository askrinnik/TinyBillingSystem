using MediatR;
using Tibis.Billing.CQRS.Models;

namespace Tibis.Billing.CQRS.Requests;

public record CreateSubscriptionRequest(Guid ProductId, Guid AccountId) : IRequest<SubscriptionDto>;