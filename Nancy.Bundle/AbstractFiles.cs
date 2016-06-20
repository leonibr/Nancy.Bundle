using System.Collections.Generic;

namespace Nancy.Bundle
{
    public abstract class AbstractFiles : IContentsGroup
    {


        abstract public string ReleaseKey();

        abstract public string ReleaseUrl();

        abstract public List<IContentItem> Contents();

        public virtual string DebugKey()
        {
            return this.ReleaseKey() + "-debug";
        }

        //public virtual string DebugUrl()
        //{
        //    return this.ReleaseUrl();
        //}






    }
}
