namespace WebSampleApp.Modules
{
    public class HomeModule : Nancy.NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["home"];
            };
            Get["/test"] = _ =>
            {
                return View["test"];
            };
        }
    }
}
