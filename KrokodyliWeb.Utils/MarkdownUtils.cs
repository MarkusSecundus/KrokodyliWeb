using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public static class MarkdownUtils
    {
        private static class MarkdownPipeline_LazyInit
        {
            public static readonly MarkdownPipeline Value = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseBootstrap().Build();
        }
        public static string MarkdownToHtml(string markdown)
        {
            return Markdown.ToHtml(markdown, MarkdownPipeline_LazyInit.Value);
        }
    }
}
