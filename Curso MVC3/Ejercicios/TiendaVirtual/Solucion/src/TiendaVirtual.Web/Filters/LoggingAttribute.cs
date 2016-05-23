using System.Web.Mvc;
using log4net;
using System.Reflection;
using System.Web.Routing;
using System.Linq;

namespace TiendaVirtual.Web.Filters
{
    //[AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class LoggingAttribute : FilterAttribute, IActionFilter, IExceptionFilter
    {
        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string parameters = filterContext.ActionParameters
                            .Select(x => x.Value).Aggregate((a, b) => a + "," + b).ToString();
            log.Info(this.GetMessage("OnActionExecuting", filterContext.RouteData, parameters));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string resultType = filterContext.Result.GetType().Name;
            log.Info(this.GetMessage("OnActionExecuted", filterContext.RouteData, resultType: resultType));
        }

        public void OnException(ExceptionContext filterContext)
        {
            log.Error(this.GetMessage("OnException", filterContext.RouteData)
                , filterContext.Exception);
            //filterContext.Result = new ViewResult { ViewName = "Error" };
            //filterContext.ExceptionHandled = true;
        }

        public string GetMessage(string methodName, RouteData routeData, string parameters = "", string resultType = "")
        {
            return string.Format("{0} controller:{1} action:{2} parameters:[{3}] result:{4}", methodName,
                   routeData.Values["controller"], routeData.Values["action"], parameters, resultType);
        }
    }
}