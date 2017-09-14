
using GlossaryLogging;
using GlossaryWebUI.Helpers;
using System.Web;
using System.Web.Mvc;
namespace GlossaryWebUI.Filters
{
    public class ExceptionHandlingFilter:ActionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionHandlingFilter()
        {
            _logger = DependencyResolver.Current.GetService<ILogger>(); ;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                try
                {
                    string errorMsg;
                    var url = filterContext.HttpContext.Request.UrlReferrer != null
                        ? filterContext.HttpContext.Request.UrlReferrer.PathAndQuery
                        : string.Empty;
                    errorMsg = filterContext.Exception.Message;

                    _logger.WriteLog(GlobalHelper.FormatError(filterContext.Exception), true);
                    if (filterContext.Exception is HttpAntiForgeryException)
                        _logger.WriteLog(string.Format("Anti forgery exeption {0} {1}", url, errorMsg),true);
                    HttpContext.Current.Response.Redirect("~/GeneralError.html");
                }
                catch
                {
                    _logger.WriteLog("Generic error", true);
                }
            }
        }
    }
}