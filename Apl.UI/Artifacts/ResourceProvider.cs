using System;
using System.Configuration;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Configuration;

namespace Apl.UI.Artifacts
{
  public sealed class ResourceProvider
  {
    public static string GetResourceString(string resource)
    {
        var resourceManager = new ResourceManager("model.ui", Assembly.GetExecutingAssembly());
      return resourceManager.GetString(resource, Thread.CurrentThread.CurrentCulture);
    }

    public static string GetThemeImage(string file)
    {
      return GetApplicationPath() + "App_Themes/" + GetCurrentTheme() + "/images/" + file;
    }

    public static string GetThemeScript(string file)
    {
      return GetApplicationPath() + "App_Themes/" + GetCurrentTheme() + "/" + file;
    }

    private static string GetCurrentTheme()
    {
      return ((PagesSection)ConfigurationManager.GetSection("system.web/pages")).Theme;
    }

    public static string GetApplicationPath()
    {
      return HttpContext.Current.Request.ApplicationPath.EndsWith("/")
                 ? HttpContext.Current.Request.ApplicationPath
                 : HttpContext.Current.Request.ApplicationPath + "/";
    }

    public static Uri GetBaseUri(HttpContext httpContext)
    {
      var requestUrl = httpContext.Request.Url.AbsoluteUri;
      var prefix = httpContext.Request.Path.Equals("/")
          ? requestUrl
          : requestUrl.Substring(0, requestUrl.IndexOf(httpContext.Request.Path));

      return new Uri(prefix + ResourceProvider.GetApplicationPath());
    }
  }
}
