using System.Collections.Generic;

namespace Nancy.Bundle.Settings
{

    /// <summary>
    /// Abstract class to be used by <see cref="DefaultConfigSettings"/>
    /// </summary>
    public abstract class AbastractConfigSettings : IConfigSettings
    {
        private List<IContentsGroup> listOfGroups;
        private string _commonAssetsRoute = "/assets";

        public AbastractConfigSettings()
        {
            this.listOfGroups = new List<IContentsGroup>();
        }
        /// <summary>
        /// Common route path used by Nancy.Bundle to provide the assets. Override this if you want a different url or if you are already using this path in one of your own modules.
        /// </summary>
        public virtual string CommonAssetsRoute
        {
            get
            {
                return _commonAssetsRoute;
            }
            set
            {
                _commonAssetsRoute = value;
            }
        }

        /// <summary>List  of Group of Bundles (CSS or JS) stored in memory
        /// </summary>
        public IEnumerable<IContentsGroup> ListOfContentGroups
        {
            get
            {
                return listOfGroups;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group">
        /// </param>
        public void AddContentGroup(IContentsGroup group)
        {
            this.listOfGroups.Add(group);
        }


    }
}
