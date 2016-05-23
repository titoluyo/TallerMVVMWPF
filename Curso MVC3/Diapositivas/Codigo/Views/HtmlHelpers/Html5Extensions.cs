using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// ReSharper disable ConvertToConstant.Local
namespace Views.HtmlHelpers
{
    using System.Web.Mvc;

    public static class Html5Extensions
    {
        public static MvcHtmlString Video(this HtmlHelper htmlHelper,
                                          string src){
            var videoTag = @"<video src='{0}' controls='controls'>
                             Tag no soportado
                            </video>";
            return MvcHtmlString.Create(String.Format(videoTag, src));
        }
    }
}
// ReSharper restore ConvertToConstant.Local