namespace WebSampleApp.Bundles
{
    public class MyBundleConfig : Nancy.Bundle.Settings.DefaultConfigSettings
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
