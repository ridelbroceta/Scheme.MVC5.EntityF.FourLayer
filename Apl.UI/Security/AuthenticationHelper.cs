using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Apl.Entities.Domain;
using Apl.BusinessLayer.MainServices;


namespace Apl.UI.Security
{
  public static class AuthenticationHelper
  {
    public static void StoreAuthenticationData(user user)
    {
        using (var servicios = new FrameworkServiceFactory())
        {
            user = servicios.ServiceUser.Find(user.Id);
            var userData = user.Id + ";" + servicios.ServiceUser.RolesToString(user);
            var ticket = new FormsAuthenticationTicket(1,
                                                     string.Format("{0}", user.Email),
                                                     DateTime.Now,
                                                     DateTime.Now.AddMinutes(30),
                                                     true,
                                                     userData,
                                                     FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }

    [SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
    public static void LoadAuthenticationData()
    {
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        if (authCookie != null)
        {
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            var identity = new UserIdentity(authTicket);
            var roles = authTicket.UserData.Split(';')[1].Split(',');
            var newUser = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = newUser;
        }

    }
  }
}
