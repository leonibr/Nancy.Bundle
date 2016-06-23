using Nancy.Bundle.Settings;
using SquishIt.Framework;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Diagnostics;

namespace Nancy.Bundle.Modules
{
    public class AssetsModule : NancyModule
    {
        public AssetsModule(IConfigSettings config) : base(config.CommonAssetsRoute)
        {

            Before.AddItemToStartOfPipeline(CheckCacheHeaderFromRequest);

            After.AddItemToEndOfPipeline(AddEtagToResponseHeader);

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

        private static Task AddEtagToResponseHeader(NancyContext context, CancellationToken cancel)
        {
            return new Task(() =>
            {
                string queryParam = context.Request.Query["r"];
                if (queryParam != null)
                    context.Response.WithHeader("ETag", queryParam);
                context.Response.WithHeader("Date", DateTime.Now.ToUniversalTime().ToString());

            });

        }


        private Response CheckCacheHeaderFromRequest(NancyContext context)
        {

            try
            {
                string queryParam = Context.Request.Query["r"];
                string ifNoneMatch = Context.Request.Headers.IfNoneMatch.FirstOrDefault();

                if ( ifNoneMatch!=null && queryParam != null && ifNoneMatch.Contains(queryParam))
                {
                    return HttpStatusCode.NotModified;
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.Write(e);
                return null;
            }



        }

        private Response CreateResponse(string content, string contentType)
        {

            var response = new Nancy.Response();
            response.Headers.Add("Content-Type", contentType);
            response.Headers.Add("Cache-Control", "max-age=90000");
            var data = Encoding.UTF8.GetBytes(content);


            response.Contents = stream =>
           {
               //stream.Seek(0, SeekOrigin.Begin);
               stream.Write(data, 0, data.Length);
           };

            response.StatusCode = HttpStatusCode.OK;




            return response;


        }
    }
}
