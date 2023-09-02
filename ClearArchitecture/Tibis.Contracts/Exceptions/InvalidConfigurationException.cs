namespace Tibis.Contracts.Exceptions;

public class InvalidConfigurationException : TibisException
{
    public InvalidConfigurationException(string configurationKey) : base($"A configuration value with key {configurationKey} was not found.")
    { }
}