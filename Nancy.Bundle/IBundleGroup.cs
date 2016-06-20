using SquishIt.Framework.CSS;
using SquishIt.Framework.JavaScript;

namespace Nancy.Bundle
{
    public interface IBundleGroup
    {
        void Add(string path);
        void AddMinified(string path);
        void AddDirectory(string path, bool v);
        void AddMinifiedDirectory(string path);
    }

    public class NancyJavascriptBundleGroup : JavaScriptBundle, IBundleGroup
    {
        public void AddMinifiedDirectory(string path)
        {
            base.AddMinifiedDirectory(path);
        }

        void IBundleGroup.Add(string path)
        {
            base.Add(path);
        }

        void IBundleGroup.AddDirectory(string path, bool isRecursive)
        {
            base.AddDirectory(path, isRecursive);
        }

        void IBundleGroup.AddMinified(string path)
        {
            base.AddMinified(path);
        }
    }
    public class NancyCssBundleGroup : CSSBundle, IBundleGroup
    {

        public void AddMinifiedDirectory(string path)
        {
            base.AddMinifiedDirectory(path);
        }

        void IBundleGroup.Add(string path)
        {
            base.Add(path);
        }

        void IBundleGroup.AddDirectory(string path, bool isRecursive)
        {
            base.AddDirectory(path, isRecursive);
        }

        void IBundleGroup.AddMinified(string path)
        {
            base.AddMinified(path);
        }
    }
}
