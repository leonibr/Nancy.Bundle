using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bundle;
using Nancy.Bundle.Settings;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using WebSampleApp.Bundles;

namespace WebSampleApp
{
    public class Bootstrap : Nancy.DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {

            StaticConfiguration.EnableRequestTracing = true;
            //NancyBundle.Attach();
            var config = container.Resolve<IConfigSettings>();
            
            container.AttachNancyBundle<MyBundleConfig>(cfg =>
            {
                cfg.AddContentGroup(new MyCustomCssBundle());
                cfg.AddContentGroup(new MyJsBundle());
                cfg.AddContentGroup(new PageStyle());
            });

        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get
            {
                return new DiagnosticsConfiguration() { Password = @"diagnostic" };
            }
        }
    }
}
