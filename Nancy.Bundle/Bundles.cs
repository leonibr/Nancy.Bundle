using Nancy.Bundle.Settings;

namespace Nancy.Bundle
{

    public class Bundles
    {
        public static string GetCssKey(string key)
        {
            try
            {
#if DEBUG
                string  linkTags = SquishIt.Framework.Bundle.Css().RenderNamed(key + "-debug");
#endif
#if !DEBUG
                string linkTags = SquishIt.Framework.Bundle.Css().RenderCachedAssetTag(key);
#endif
                return linkTags;
            }
            catch (System.Exception e)
            {

                throw new ConfigException("Key for the CSS bundle not found. Did you AddContentGroup(new MyCssBundle()) at your bootstrap class? Or check the key name used in view?", e);

            }

        }

        public static string GetJsKey(string key)
        {
            try
            {
#if DEBUG
            string scriptTags = SquishIt.Framework.Bundle.JavaScript().RenderNamed(key + "-debug");
#endif
#if !DEBUG
                string scriptTags = SquishIt.Framework.Bundle.JavaScript().RenderCachedAssetTag(key);
#endif
                return scriptTags;
            }
            catch (System.Exception e)
            {

                throw new ConfigException("Key for the Javascript bundle not found. Did you AddContentGroup(new MyJsBundle()) at your bootstrap class? Or check the key name used in view?", e);

            }

        }
    }
}
