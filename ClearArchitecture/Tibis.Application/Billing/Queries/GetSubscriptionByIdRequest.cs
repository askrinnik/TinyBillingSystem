using MediatR;
using Tibis.Application.Billing.Models;

namespace Tibis.Application.Billing.Queries;

public record GetSubscriptionByIdRequest(Guid Id) : IRequest<SubscriptionDto>;