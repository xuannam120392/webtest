using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BIDVSmartContent.Helpers
{
    public static class CustomeCheckboxHelper
    {
        public static MvcHtmlString CustomerCheckBox(this HtmlHelper htmlHelper, string listFuncs, string funcId, object htmlAttributes)
        {
            var checkbox = new TagBuilder("input");
            checkbox.Attributes.Add("type", "checkbox");
            checkbox.Attributes.Add("value", funcId);
            checkbox.Attributes.Add("id", funcId);
            var str = listFuncs.Split(',');
            var ids = (from s in str where !string.IsNullOrEmpty(s) select Convert.ToInt32(s)).ToList();
            if (ids.Contains(Convert.ToInt32(funcId)))
            {
                checkbox.Attributes.Add("checked", "checked");
            }
            checkbox.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(checkbox.ToString(TagRenderMode.Normal));
        }
    }
}