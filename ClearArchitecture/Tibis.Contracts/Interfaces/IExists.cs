namespace Tibis.Contracts.Interfaces;

/// <summary>
/// Determines the existence of an item.
/// </summary>
/// <typeparam name="TKey">The type of the key used to look up values.</typeparam>
/// <typeparam name="TValue">The type of value retrieved, such as Account.</typeparam>
public interface IExists<in TKey, TValue> where TValue : notnull
{
    /// <summary>
    /// Determines whether or not an object with the specified primary key exists.
    /// </summary>
    /// <param name="key">The key used to look up the value.</param>
    /// <returns>A boolean indicating whether or not an object with the specified key exists.</returns>
    Task<bool> ExistsAsync(TKey key);
}