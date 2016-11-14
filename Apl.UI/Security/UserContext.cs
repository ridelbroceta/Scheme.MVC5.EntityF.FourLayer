using System.Globalization;
using System.Web;
using System.Web.Security;
using Apl.Entities.Domain;
using Apl.BusinessLayer.Services;
using Apl.UI.Artifacts;

namespace Apl.UI.Security
{
  public static class UserContext
  {

    public static void Login(user user, bool rememberMe)
    {
      AuthenticationHelper.StoreAuthenticationData(user);
      LastLoggedUserName = user.Email;
      RememberMe = rememberMe;
    }

    public static void Logout()
    {
        HttpContext.Current.Session.Abandon();
        FormsAuthentication.SignOut();
        RememberMe = false;
        ServiceUser.DesconnectedUser(CurrentUserId);
    }

    public static bool IsInRole(string role)
    {
        return HttpContext.Current.User.IsInRole(role);
    }

    public static bool IsAuthenticated
    {
      get
      {
        return HttpContext.
               Current.
               User.
               Identity.
               IsAuthenticated;
      }
    }

    public static int CurrentUserId
    {
        get
        {
            var identity = (HttpContext.Current.User.Identity as UserIdentity);
            return identity != null ? identity.UserId : 0;
        }
    }

    public static string LastLoggedUserName
    {
      get
      {
        return PersistenceHelper.
               GetCookieData("QL_LOGIN", string.Empty);
      }
      set
      {
        PersistenceHelper.
        SetCookieData("QL_LOGIN", value);
      }
    }

    public static bool RememberMe
    {
      get
      {
        return bool.Parse(PersistenceHelper.
                           GetCookieData("QL_REMEMBER_ME", false.ToString(CultureInfo.InvariantCulture))
                         );
      }
      set
      {
        PersistenceHelper.
        SetCookieData("QL_REMEMBER_ME", value.ToString(CultureInfo.InvariantCulture));
      }
    }
  }
}
