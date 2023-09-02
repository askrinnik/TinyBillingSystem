using Tibis.AccountManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Models;

namespace Tibis.Facade.Web.Models
{
    public record DemoDataDto(ProductDto Product, AccountDto Account, Guid SubscriptionId);
}
