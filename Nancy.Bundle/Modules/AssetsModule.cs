using Nancy.Bundle.Settings;
using SquishIt.Framework;
using System.IO;
using System.Linq;
using System.Text;

namespace Nancy.Bundle.Modules
{
    public class AssetsModule : NancyModule
    {
        public AssetsModule(IConfigSettings config) : base(config.CommonAssetsRoute)
        {


            var cssGroups = config.ListOfContentGroups.OfType<ICssType>();
            var jsGroups = config.ListOfContentGroups.OfType<IJsType>();
            foreach (ICssType item in cssGroups.ToList())
            {

#if !DEBUG

                Get[item.ReleaseUrl()] = _ => CreateResponse(SquishIt.Framework.Bundle.Css().RenderCached(item.ReleaseKey()), Configuration.Instance.CssMimeType);
#endif

            }


            foreach (IJsType item in jsGroups.ToList())
            {

#if !DEBUG

                Get[item.ReleaseUrl()] = _ => CreateResponse(SquishIt.Framework.Bundle.JavaScript().RenderCached(item.ReleaseKey()), Configuration.Instance.JavascriptMimeType);
#endif




            }

        }

        Response CreateResponse(string content, string contentType)
        {
            return Response
                .FromStream(() => new MemoryStream(Encoding.UTF8.GetBytes(content)), contentType)
                .WithHeader("Cache-Control", "max-age=15552000");
        }
    }
}
