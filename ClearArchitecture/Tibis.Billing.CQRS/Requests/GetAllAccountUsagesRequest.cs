using MediatR;
using Tibis.Billing.CQRS.Models;

namespace Tibis.Billing.CQRS.Requests;

public record GetAllAccountUsagesRequest : IRequest<IEnumerable<AccountUsageDto>>;