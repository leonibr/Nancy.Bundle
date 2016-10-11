using Nancy.Bundle.Settings;

namespace WebSampleApp.Bundles
{
    public class MyBundleConfig : DefaultConfigSettings
    {
        public override string CommonAssetsRoute
        {
            get
            {
                return "/cli-bundles";
            }
        }
    }
}
