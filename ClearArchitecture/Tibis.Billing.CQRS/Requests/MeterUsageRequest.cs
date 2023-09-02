using MediatR;
using Tibis.Billing.CQRS.Models;

namespace Tibis.Billing.CQRS.Requests;

public record MeterUsageRequest(string ProductName, string AccountName, DateTime Date, int Count) : IRequest<AccountUsageDto>;