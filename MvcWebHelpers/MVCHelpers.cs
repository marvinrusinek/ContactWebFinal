using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;


namespace MVCWebHelpers
{
    public static class MVCHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper helper, PagingModel model)
        {
            var stringBuilder = new StringBuilder("<ul class='pager'>");
            for(var i = 1; i <= model.MaxPages; i++)
            {
                stringBuilder.Append(String.Format("<li {2}><a href='{1}'>{0}</a></li>", i, model.UrlGeneratorFunction(i), i==model.CurrentPage ? "class=Selected" : String.Empty));
            }
            stringBuilder.Append("</ul>");
            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        public static MvcHtmlString AlphaLinks(this HtmlHelper helper, AlphaModel model)
        {
            var stringBuilder = new StringBuilder("<ul class='pager'>");
foreach (var letter in model.LetterDictionary.Keys)
{
    if (model.LetterDictionary[letter] == 0)
        stringBuilder.Append(String.Format("<li class='Empty'><span>{0}</span></li>", letter));
    else
        stringBuilder.Append(String.Format("<li {0}><a href='{1}' >{2}</a></li>",
                                  model.SelectedLetter == letter ? "class=Selected" : String.Empty,
                                  model.UrlGeneratorFunction(letter),
                                  letter
                                  ));
}
            stringBuilder.Append("</ul>");
            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        public static MvcHtmlString NavLink(this HtmlHelper helper, string controller, string action, Func<string, string, MvcHtmlString> func, bool showLink)
        {
            if (!showLink)
                return MvcHtmlString.Create(String.Empty);

            var routeData = helper.ViewContext.RouteData.Values;
            string html = String.Format("<li {0}>{1}</li>",
                                        routeData["controller"].ToString().ToLower() == controller &&
                                        routeData["action"].ToString().ToLower() == action
                                            ? "class=Selected"
                                            : String.Empty, func(action, controller));
            return MvcHtmlString.Create(html);
        }
    }
}