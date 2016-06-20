namespace Nancy.Bundle
{

    public class ContentFolder : IContentFolder
    {

        public ContentFolder(string path, eRecursive recursive = eRecursive.ThisFolderAndChildrenFolders, eMinify minify = eMinify.MinifyIt)
        {
            SourcePath = path;
            Recursive = recursive;
            Minify = minify;
        }



        public string SourcePath { get; set; }
        public eMinify Minify { get; set; }

        public eRecursive Recursive { get; set; }

    }
}
