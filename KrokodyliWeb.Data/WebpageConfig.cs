using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Data
{
    public class WebpageConfig
    {
        public string DataFileURI { get; init; } = null!;

        public MarkdownPagesInfo MarkdownPagesConfig { get; init; } = new();

        public class MarkdownPagesInfo
        {
            public string RootURI { get; init; } = @"https://raw.githubusercontent.com/MarkusSecundus/KrokodyliWeb/data/pages/";

            public string FileExtension { get; init; } = ".md";
        }
    }
}
