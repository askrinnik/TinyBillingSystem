namespace Tibis.Contracts.Interfaces;

/// <summary>
/// Represents an object that can retrieve a value for a given key.
/// </summary>
/// <typeparam name="TKey">The type of the key used to look up values.</typeparam>
/// <typeparam name="TOut">The type of value retrieved.</typeparam>
public interface IRetrieve<in TKey, TOut> where TOut : notnull
{
    /// <summary>
    /// Tries to retrieve a <typeparamref name="TOut"/> value associated with <paramref name="key"/>,
    /// using the specified transaction.
    /// </summary>
    /// <param name="key">The key used to look up the value.</param>
    /// <returns>
    /// The <typeparamref name="TOut"/> value associated with <paramref name="key"/>,
    /// or null if the item is not found.
    /// </returns>
    Task<TOut?> TryRetrieveAsync(TKey key);
}
/// <summary>
/// Represents an object that can retrieve a value for a given key.
/// </summary>
/// <typeparam name="TKey1">The type of the first key used to look up values.</typeparam>
/// <typeparam name="TKey2">The type of the second key used to look up values.</typeparam>
/// <typeparam name="TOut">The type of value retrieved.</typeparam>
public interface IRetrieve<in TKey1, in TKey2, TOut> where TOut : notnull
{
    /// <summary>
    /// Tries to retrieve a <typeparamref name="TOut"/> value associated with <paramref name="key1"/> and <paramref name="key2"/>,
    /// using the specified transaction.
    /// </summary>
    /// <param name="key1">The first key used to look up the value.</param>
    /// <param name="key2">The second key used to look up the value.</param>
    /// <returns>
    /// The <typeparamref name="TOut"/> value associated with <paramref name="key1"/> and <paramref name="key2"/>,
    /// or null if the item is not found.
    /// </returns>
    Task<TOut?> TryRetrieveAsync(TKey1 key1, TKey2 key2);
}

/// <summary>
/// Represents an object that can retrieve an enumeration of values for a given key.
/// </summary>
/// <typeparam name="TOut">The type of value retrieved.</typeparam>
public interface IRetrieveMany<out TOut>
{
    /// <summary>
    /// Retrieves an enumerable of <typeparamref name="TOut"/> values, optionally using the specified transaction.
    /// </summary>
    /// <returns>An <see cref="IAsyncEnumerable{TOut}"/>.</returns>
    IAsyncEnumerable<TOut> RetrieveManyAsync();
}
