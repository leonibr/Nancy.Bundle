using System;

namespace Nancy.Bundle.Settings
{
    /// <summary>
    /// An exception related to an invalid configuration created within <see cref="IConfigSettings"/>
    /// </summary>
    public class ConfigException : Exception
    {
        public ConfigException(string message) : base(message)
        {

        }

        public ConfigException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
