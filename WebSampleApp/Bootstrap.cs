using Nancy.Bootstrapper;
using Nancy.Bundle;
using Nancy.TinyIoc;
using WebSampleApp.Bundles;

namespace WebSampleApp
{
    public class Bootstrap : Nancy.DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {

            var config = new MyBundleConfig();

            config.AddContentGroup(new MyCustomCssBundle());
            config.AddContentGroup(new MyJsBundle());
            NancyBundle.Attach(container, config);
        }
    }
}
