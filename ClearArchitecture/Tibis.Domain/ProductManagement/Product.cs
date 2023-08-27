namespace Tibis.Domain.ProductManagement;

public record Product(Guid Id, string Name)
{
    /// <summary>
    /// Create a new instance of <see cref="Product"/>/>.
    /// </summary>
    /// <param name="name">The account name</param>
    public Product(string name) : this(Guid.Empty, name)
    { }
}