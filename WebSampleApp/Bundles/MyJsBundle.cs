using Nancy.Bundle;
using System.Collections.Generic;

namespace WebSampleApp.Bundles
{
    public class MyJsBundle : JSFiles
    {


        public override List<IContentItem> Contents()
        {
            return new List<IContentItem>() {
                new ContentFile("~/content/lib/jquery-3.0.0.js", eMinify.DoNotMinifyIt),
                new ContentFolder("~/content/app",eRecursive.ThisFolderAndChildrenFolders, eMinify.MinifyIt)
            };
        }

        public override string ReleaseKey()
        {
            return "js-key";
        }

        public override string ReleaseUrl()
        {
            return "/js/broker";
        }
    }

}
