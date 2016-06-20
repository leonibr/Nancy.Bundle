using Nancy.Bundle.Settings;
using Nancy.TinyIoc;
using SquishIt.Framework.CSS;
using SquishIt.Framework.JavaScript;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Bundle
{
    public class NancyBundle
    {
        protected static string BasePathForTesting = "";

        public static void Attach(TinyIoCContainer container, IConfigSettings config)
        {

            Setup(config, "");
            container.Register<IConfigSettings>(config);
        }

        protected static JavaScriptBundle BuildJavaScriptBundle(List<IContentItem> contents)
        {
            NancyJavascriptBundleGroup bundle = new NancyJavascriptBundleGroup();
            AddFilesToBundle(contents, bundle);
            AddFoldersToBundle(contents, bundle);
            return bundle;
        }

        protected static CSSBundle BuildCssBundle(List<IContentItem> contents)
        {

            NancyCssBundleGroup bundle = new NancyCssBundleGroup();
            AddFilesToBundle(contents, bundle);
            AddFoldersToBundle(contents, bundle);
            return bundle;
        }

        internal static void Setup(IConfigSettings config, string basePathForTesting = "")
        {
            try
            {
                BasePathForTesting = basePathForTesting;
                IEnumerable<ICssType> cssGroups = config.ListOfContentGroups.OfType<ICssType>().ToList();

                foreach (ICssType item in cssGroups)
                {
                    BuildCssBundle(item.Contents()).ForceRelease().AsCached(item.ReleaseKey(), config.CommonAssetsRoute + item.ReleaseUrl());
                    BuildCssBundle(item.Contents()).ForceDebug().AsNamed(item.DebugKey(), string.Empty);

                }

                IEnumerable<IJsType> jsGroups = config.ListOfContentGroups.OfType<IJsType>().ToList();
                foreach (IJsType item in jsGroups)
                {
                    BuildJavaScriptBundle(item.Contents()).ForceRelease().AsCached(item.ReleaseKey(), config.CommonAssetsRoute + item.ReleaseUrl());
                    BuildJavaScriptBundle(item.Contents()).ForceDebug().AsNamed(item.DebugKey(), string.Empty);
                }


            }
            catch (System.Exception e)
            {

                throw new ConfigException("One or more files or folders could not be found. Check List of 'IContent' in descendent class from JsFile or CssFile.", e);
            }



        }

        private static void AddFoldersToBundle(List<IContentItem> listOfContentFolder, IBundleGroup bundle)
        {
            var folders = listOfContentFolder.OfType<ContentFolder>().ToList();
            foreach (ContentFolder item in folders)
            {
                var path = item.SourcePath;

                if (!string.IsNullOrWhiteSpace(BasePathForTesting))
                {
                    path = BasePathForTesting + item.SourcePath.Replace("~", "");
                }

                if (item.Minify == eMinify.MinifyIt)
                {
                    bundle.AddDirectory(path,
                            (item.Recursive == eRecursive.ThisFolderAndChildrenFolders));
                }
                else if (item.Minify == eMinify.DoNotMinifyIt)
                {
                    bundle.AddMinifiedDirectory(path);
                }
            }
        }

        private static void AddFilesToBundle(List<IContentItem> listOfContentFile, IBundleGroup bundle)
        {

            var files = listOfContentFile.OfType<ContentFile>().ToList();
            foreach (ContentFile item in files)
            {
                var path = item.SourcePath;

                if (!string.IsNullOrWhiteSpace(BasePathForTesting))
                {
                    path = BasePathForTesting + item.SourcePath.Replace("~", "");
                }

                if (item.Minify == eMinify.MinifyIt)
                {
                    bundle.Add(path);
                }
                else if (item.Minify == eMinify.DoNotMinifyIt)
                {
                    bundle.AddMinified(path);
                }
            }
        }

    }
}
