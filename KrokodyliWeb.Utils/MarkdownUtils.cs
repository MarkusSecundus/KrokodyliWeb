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
            var ret = Markdown.ToHtml(markdown, MarkdownPipeline_LazyInit.Value);
            ret = BeautifyHtml(ret);
            return ret;
        }

        private static string BeautifyHtml(string html)
        {
            return html.Replace("<ol>", @"<ol class=""list-group list-group-numbered text-white"">");
        }
    }
}
