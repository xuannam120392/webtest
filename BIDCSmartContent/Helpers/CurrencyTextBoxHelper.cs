using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BIDVSmartContent.Helpers
{
    public static class CurrencyTextBoxHelper
    {
        public static MvcHtmlString CurrencyTextbox(this HtmlHelper htmlHelper, string val, object htmlAttributes)
        {
            var currency = new TagBuilder("input");
            currency.Attributes.Add("type", "text");
            if (!string.IsNullOrEmpty(val))
            {
                currency.Attributes.Add("value", Convert.ToDecimal(val).ToString("N0"));
            }
            else
            {
                currency.Attributes.Add("value", "0");
            }
            currency.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(currency.ToString(TagRenderMode.Normal));
        }
    }
}