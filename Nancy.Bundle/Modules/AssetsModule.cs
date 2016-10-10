using Nancy.Bundle.Settings;
using SquishIt.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                Get["Nancy.Bundle(key:" + item.ReleaseKey() + ")", item.ReleaseUrl(), true] = async (c, x) => await CreateResponse(SquishIt.Framework.Bundle.Css().RenderCached(item.ReleaseKey()), Configuration.Instance.CssMimeType);
            }


            foreach (IJsType item in jsGroups.ToList())
            {
                Get["Nancy.Bundle(key:" + item.ReleaseKey() + ")", item.ReleaseUrl(), true] = async (c, x) => await CreateResponse(SquishIt.Framework.Bundle.JavaScript().RenderCached(item.ReleaseKey()), Configuration.Instance.JavascriptMimeType);
            }

        }

        private static Task<Response> AddEtagToResponseHeader(NancyContext context, CancellationToken cancel)
        {
            if (cancel.IsCancellationRequested)
                return NullResponse;
            return Task<Response>.Factory.StartNew(() =>
            {
                string queryParam = context.Request.Query["r"];
                if (queryParam != null)
                    context.Response.WithHeader("ETag", queryParam);
                context.Response.WithHeader("Date", DateTime.Now.ToUniversalTime().ToString());
                return context.Response;
            });

        }


        private static Task<Response> CheckCacheHeaderFromRequest(NancyContext context, CancellationToken cancel)
        {

            try
            {
                if (cancel.IsCancellationRequested)
                    return NullResponse;

                string queryParam = context.Request.Query["r"];
                string ifNoneMatch = context.Request.Headers.IfNoneMatch.FirstOrDefault();

                if (ifNoneMatch != null && queryParam != null && ifNoneMatch.Contains(queryParam))
                {
                    return NotModifiedResponse;
                }
                return NullResponse;
            }
            catch (Exception e)
            {
                Debug.Write(e);
                return NullResponse;
            }



        }

        private async Task<Response> CreateResponse(string content, string contentType)
        {

            var response = new Nancy.Response();
            response.Headers.Add("Content-Type", contentType);
            response.Headers.Add("Cache-Control", "max-age=90000");
            var data = Encoding.UTF8.GetBytes(content);


            response.Contents = async stream =>
           {
               await stream.WriteAsync(data, 0, data.Length);
           };

            response.StatusCode = HttpStatusCode.OK;

            return await Task<Response>.Factory.StartNew(() =>
            {
                return response;
            });


        }

        private static Task<Response> NullResponse
        {
            get
            {
                return Task<Response>.Factory.StartNew(() =>
                 {
                     return null;
                 });
            }
        }

        private static Task<Response> NotModifiedResponse
        {
            get
            {
                return Task<Response>.Factory.StartNew(() =>
                {
                    return HttpStatusCode.NotModified;
                });
            }
        }

    }
}
