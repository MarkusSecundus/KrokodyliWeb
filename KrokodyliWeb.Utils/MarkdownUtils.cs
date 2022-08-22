using HtmlAgilityPack;
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
            var doc = CreateHtmlDocument(html);

            HtmlNodeCollection ols = doc.DocumentNode.SelectNodes("descendant::ol");
            if (ols == null) return html;

            foreach (var ol in ols)
                ol.AddClass("list-group list-group-numbered text-white");

            return doc.SaveString();
        }




        private static HtmlDocument CreateHtmlDocument(string html)
        {
            HtmlDocument ret = new();
            ret.LoadHtml(html);
            return ret;
        }
        private static string SaveString(this HtmlDocument self)
        {
            using var ret = new StringWriter();
            self.Save(ret);
            return ret.ToString();
        }
    }
}
