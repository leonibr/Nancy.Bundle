using Nancy.Bundle;
using System.Collections.Generic;

namespace WebSampleApp.Bundles
{
    public class PageStyle : MyJsBundle
    {

        public override List<IContentItem> Contents()
        {
            var contents = base.Contents();
            contents.Add(new ContentFile("~/content/test/test.js", eMinify.MinifyIt));
            return contents;
        }

        public override string ReleaseKey()
        {
            return "my-js-page";
        }

        public override string ReleaseUrl()
        {
            return "/page-js";
        }
    }
}
