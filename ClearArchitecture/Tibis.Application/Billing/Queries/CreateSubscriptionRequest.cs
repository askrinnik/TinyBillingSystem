using MediatR;
using Tibis.Application.Billing.Models;

namespace Tibis.Application.Billing.Queries;

public record CreateSubscriptionRequest(Guid ProductId, Guid AccountId) : IRequest<SubscriptionDto>;