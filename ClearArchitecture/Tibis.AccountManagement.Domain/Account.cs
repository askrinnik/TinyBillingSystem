namespace Tibis.AccountManagement.Domain;

public record Account(Guid Id, string Name)
{
    /// <summary>
    /// Create a new instance of <see cref="Account"/>/>.
    /// </summary>
    /// <param name="name">The account name</param>
    public Account(string name) : this(Guid.Empty, name)
    { }
}