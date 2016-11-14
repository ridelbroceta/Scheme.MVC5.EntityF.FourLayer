using System;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Apl.UI.Code52.i18n;
using Apl.UI.Security;

namespace Apl.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            var cultureName = "en-En";
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
            var cultureInfo = new System.Globalization.CultureInfo(cultureName)
            {
                NumberFormat =
                {
                    CurrencyDecimalDigits = 2,
                    CurrencyDecimalSeparator = ".",
                    CurrencyGroupSeparator = "",
                    NumberDecimalDigits = 2,
                    NumberDecimalSeparator = ".",
                    NumberGroupSeparator = ""
                },
                DateTimeFormat =
                {
                    DateSeparator = "/",
                    ShortDatePattern = "MM/dd/yyyy",
                    LongDatePattern = "MM/dd/yyyy hh:mm:ss tt"
                }
            };
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            AuthenticationHelper.LoadAuthenticationData();
            /*if (User.Identity.IsAuthenticated)
            {
                AuthenticationHelper.LoadAuthenticationData();
            }*/
        }

    }
}
