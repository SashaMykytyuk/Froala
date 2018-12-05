using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace TestIIS.Helpers
{
    public static class PaddingHelper
    {
        public static string PageLinks(this HtmlHelper html, int currentPage, int totalPage, Func<int,string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 1;i<=totalPage;i++)
            {
                TagBuilder tagLI = new TagBuilder("li");
                TagBuilder tagA = new TagBuilder("a");
                tagA.MergeAttribute("href", pageUrl(i));
                tagA.InnerHtml = i.ToString();
                if (i == currentPage)
                    tagLI.AddCssClass("active");
                tagLI.InnerHtml = tagA.ToString();
                result.AppendLine(tagLI.ToString());
            }
            TagBuilder tagDIV = new TagBuilder("div");
            TagBuilder tagUL = new TagBuilder("ul");
            tagUL.AddCssClass("pagination");
            tagUL.InnerHtml = result.ToString();
            tagDIV.InnerHtml = tagUL.ToString();

            return tagDIV.ToString();
        }
    }
}