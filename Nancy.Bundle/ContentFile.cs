namespace Nancy.Bundle
{
    public class ContentFile : IContentItem
    {

        public ContentFile(string path, eMinify minify = eMinify.DoNotMinifyIt)
        {
            SourcePath = path;
            Minify = minify;
        }

        public string SourcePath { get; set; }

        public eMinify Minify { get; set; }


    }
}
