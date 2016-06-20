using System.Collections.Generic;

namespace Nancy.Bundle.Settings
{
    public interface IConfigSettings
    {
        string CommonAssetsRoute { get; }

        IEnumerable<IContentsGroup> ListOfContentGroups { get; }

        void AddContentGroup(IContentsGroup group);

    }
}