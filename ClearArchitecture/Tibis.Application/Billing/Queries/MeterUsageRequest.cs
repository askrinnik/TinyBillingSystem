using MediatR;
using Tibis.Application.Billing.Models;

namespace Tibis.Application.Billing.Queries;

public record MeterUsageRequest(string ProductName, string AccountName, DateTime Date, int Count) : IRequest<AccountUsageDto>;