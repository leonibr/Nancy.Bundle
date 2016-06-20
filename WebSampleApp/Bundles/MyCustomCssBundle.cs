using Nancy.Bundle;
using System.Collections.Generic;

namespace WebSampleApp.Bundles
{
    public class MyCustomCssBundle : CSSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<Nancy.Bundle.IContentItem>()
            {
                new ContentFile("~/css/style2.css", eMinify.MinifyIt),
                new ContentFile("~/css/style1.css", eMinify.MinifyIt)

            };
        }

        public override string ReleaseKey()
        {
            return "my-css";
        }

        public override string ReleaseUrl()
        {
            return "/x-css-url";
        }
    }
}
