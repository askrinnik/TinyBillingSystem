namespace Tibis.Domain.ProductManagement;

public record Product(Guid Id, string Name, ProductType ProductType, int Rate)
{
    /// <summary>
    /// Create a new instance of <see cref="Product"/>/>.
    /// </summary>
    public Product(string name, ProductType productType, int rate) : this(Guid.Empty, name, productType, rate)
    { }
}