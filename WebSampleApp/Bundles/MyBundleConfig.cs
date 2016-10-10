using Nancy.Bundle;
using Nancy.Bundle.Settings;
using System.Collections.Generic;

namespace WebSampleApp.Bundles
{
    public class MyBundleConfig : DefaultConfigSettings
    {
        public MyBundleConfig(List<IContentsGroup> contents) : base(contents)
        {
        }

        public override string CommonAssetsRoute
        {
            get
            {
                return "/cli-bundles";
            }
        }
    }
}
