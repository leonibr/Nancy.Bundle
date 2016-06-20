using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nancy.Bundle
{

    public interface IContentItem
    {
        string SourcePath { get; set; }
        eMinify Minify { get; set; }
    }

    public interface IContentFolder : IContentItem
    {
        eRecursive Recursive { get; set; }
    }

}
