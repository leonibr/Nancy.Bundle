using Nancy.Bundle;
using System.Collections.Generic;

namespace WebSampleApp.Bundles
{
    public class PublicCss : CSSFiles
    {
        public PublicCss() : base("public-css")
        {
            this.Contents = new List<IContent>()
            {
                new ContentFile("~/content/css/style1.css", eMinify.MinifyIt),
                new ContentFile("~/content/css/style2.css", eMinify.MinifyIt)
            };
        }
    }
}
