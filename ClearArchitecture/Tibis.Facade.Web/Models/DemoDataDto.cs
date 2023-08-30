using Tibis.Application.AccountManagement.Models;
using Tibis.Application.ProductManagement.Models;

namespace Tibis.Facade.Web.Models
{
    public record DemoDataDto(ProductDto Product, AccountDto Account, Guid SubscriptionId);
}
