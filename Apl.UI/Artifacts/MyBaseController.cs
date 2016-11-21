using System;
using System.Web.Mvc;
using Apl.Entities.Domain;
using Apl.BusinessLayer.MainServices;

namespace Apl.UI.Artifacts
{
    public class MyBaseController : Controller
    {
        FrameworkServiceFactory _serviceFactory;


        #region protected

        protected override void Dispose(bool disposing)
        {
            if ((_serviceFactory != null) && (disposing))
            {
                _serviceFactory.Dispose();
            }
            base.Dispose(disposing);
        }

       /* protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            var cultureName = "es-ES";
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
            var cultureInfo = new CultureInfo(cultureName)
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
                    ShortDatePattern = "dd/MM/yyyy",
                    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
                }
            };
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;


            base.Initialize(requestContext);
        }*/

        #endregion

        public FrameworkServiceFactory CServices
        {//things
            get { return _serviceFactory ?? (_serviceFactory = new FrameworkServiceFactory() ); }
        }

    }
}