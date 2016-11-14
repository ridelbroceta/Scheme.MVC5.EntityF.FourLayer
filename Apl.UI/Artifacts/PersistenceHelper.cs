using System;
using System.Web;
using System.Web.UI;

namespace Apl.UI.Artifacts
{
  public static class PersistenceHelper
  {
    public static T GetHttpContextData<T>(string id, T defaultValue)
    {
        
      return HttpContext.Current.Items.Contains(id) ? (T)HttpContext.Current.Items[id] : SetHttpContextData(id, defaultValue);
    }

    public static T GetHttpContextData<T>(string id)
    {
      return GetHttpContextData(id, default(T));
    }

    public static T GetHttpContextData<T>(string id, Func<T> initializer)
    {
      return HttpContext.Current.Items.Contains(id) ? (T)HttpContext.Current.Items[id] : SetHttpContextData(id, initializer());
    }

    public static T SetHttpContextData<T>(string id, T value)
    {
      HttpContext.Current.Items[id] = value;
      return value;
    }

    public static T GetViewStateData<T>(StateBag viewState, string id, T defaultValue)
    {
      return (viewState[id] != null) ? (T)viewState[id] : SetViewStateData(viewState, id, defaultValue);
    }

    public static T GetViewStateData<T>(StateBag viewState, string id)
    {
      return GetViewStateData(viewState, id, default(T));
    }

    public static T GetViewStateData<T>(StateBag viewState, string id, Func<T> initializer)
    {
      return (viewState[id] != null) ? (T)viewState[id] : SetViewStateData(viewState, id, initializer());
    }

    public static T SetViewStateData<T>(StateBag viewState, string id, T value)
    {
      viewState[id] = value;
      return value;
    }

    public static T GetSessionData<T>(string id, T defaultValue)
    {
      return (HttpContext.Current.Session[id] != null) ? (T)HttpContext.Current.Session[id] : SetSessionData(id, defaultValue);
    }

    public static T GetSessionData<T>(string id)
    {
      return GetSessionData(id, default(T));
    }

    public static T GetSessionData<T>(string id, Func<T> initializer)
    {
      return (HttpContext.Current.Session[id] != null) ? (T)HttpContext.Current.Session[id] : SetSessionData(id, initializer());
    }

    public static T SetSessionData<T>(string id, T value)
    {
      HttpContext.Current.Session[id] = value;
      return value;
    }

    public static string GetCookieData(string id, string defaultValue)
    {
      var cookie = HttpContext.Current.Request.Cookies.Get(id);
      return (cookie != null) ? cookie.Value : SetCookieData(id, defaultValue).Value;
    }

    public static HttpCookie SetCookieData(string id, string value)
    {
      HttpContext.Current.Response.Cookies[id].Value = value;
      HttpContext.Current.Response.Cookies[id].Expires = DateTime.Now.AddMonths(1);
      return HttpContext.Current.Response.Cookies[id];
    }
      
  }
}
