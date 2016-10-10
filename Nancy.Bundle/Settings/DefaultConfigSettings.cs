

using System.Collections.Generic;

namespace Nancy.Bundle.Settings
{
    public abstract class DefaultConfigSettings : AbastractConfigSettings
    {
        public DefaultConfigSettings(List<IContentsGroup> contents) : base(contents)
        {
        }
    }
}