﻿using System.Collections.Generic;

namespace Nancy.Bundle
{
    public interface IContentsGroup
    {

        string ReleaseKey();
        string ReleaseUrl();
        string DebugKey { get; }
        //string DebugUrl();


        List<IContentItem> Contents();
    }

    public interface IJsType : IContentsGroup
    {
    }

    public interface ICssType : IContentsGroup
    {

    }
}
