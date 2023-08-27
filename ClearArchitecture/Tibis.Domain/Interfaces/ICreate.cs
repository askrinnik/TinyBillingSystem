namespace Tibis.Domain.Interfaces;

/// <summary>
/// Represents an object that can persist a new instance of <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type persisted.</typeparam>
public interface ICreate<T>
{
    /// <summary>
    /// Persists a new instance of <typeparamref name="T"/> using the specified
    /// transaction. This may modify <paramref name="item" />.
    /// </summary>
    /// <param name="item">The entity to create. May be modified.</param>
    /// <returns>
    /// An instance of <typeparamref name="T"/>, either copied from <paramref name="item"/> or
    /// the modified instance <paramref name="item"/>.
    /// </returns>
    Task<T> CreateAsync(T item);
}