using System;
using System.Web.Mvc;
using Apl.BusinessLayer.Services;
using Apl.UI.Artifacts;
using Apl.UI.Security;
using Apl.UI.Models;

namespace Apl.UI.Controllers
{
    [Authorize]
    public class AccountController : MyBaseController
    {

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if ((Request.IsAuthenticated) && (User.Identity is UserIdentity))
            {
                try
                {
                    UserContext.Logout();
                }
                catch (Exception e)
                {

                    ModelState.AddModelError("", e.Message);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        //
        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            /*string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }

            }*/

           // return RedirectToAction("Manage", new { Message = message });
            return RedirectToAction("Manage", new { Message = "" });
        }

        //
        // GET: /Account/Manage

        [Authorize(Roles = "Admins, Others")]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been settled down."
                : "";
            if (User.Identity is UserIdentity)
            {
                ViewBag.ViewOptions = new ViewOptions
                {
                    IsPrincipal = true,
                    IsFixed = true,
                };
                var user = CServices.ServiceUser.Find((User.Identity as UserIdentity).UserId);
                if (user != null)
                {
                    ViewBag.HasLocalPassword = true;
                    //((ViewOptions)ViewBag.ViewOptions).InfoViewPartial = UsuarioController.GetInfo(user);
                }
                else
                {
                    ViewBag.HasLocalPassword = false;
                }
                ViewBag.ReturnUrl = Url.Action("Manage");

                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Manage


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins, Others")]
        public ActionResult Manage(LocalPasswordModel model)
        {
            ViewBag.ViewOptions = new ViewOptions
            {
                IsPrincipal = true,
                IsFixed = true,
            };

            if (User.Identity is UserIdentity)
            {
                var user = CServices.ServiceUser.Find((User.Identity as UserIdentity).UserId);
                if (user != null)
                {
                    ViewBag.HasLocalPassword = true;
                    //((ViewOptions)ViewBag.ViewOptions).InfoViewPartial = UsuarioController.GetInfo(user);
                    if (!ModelState.IsValid)
                        ModelState.AddModelError("", "ErrorInvalidInput");
                    else
                    {
                        // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                        try
                        {
                            ServiceUser.ChangePass(user, model.OldPassword, model.NewPassword);
                            return RedirectToAction("Manage", new {Message = ManageMessageId.ChangePasswordSuccess});

                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("", e.Message);
                        }
                    }
                }
                else
                {
                    ViewBag.HasLocalPassword = false;
                }
                ViewBag.ReturnUrl = Url.Action("Manage");
                // If we got this far, something failed, redisplay form
            }
            return View(model);
        }


        //
        // GET: /Account/ChangeEmail

        [Authorize(Roles = "Admins, Others")]
        public ActionResult ChangeEmail(ManageMessageId? message)
        {
            if ((Request.IsAuthenticated) && (User.Identity is UserIdentity))
            {
                ViewBag.StatusMessage = message == ManageMessageId.ChangeEmailSuccess ? "Your email has been changed.": "";
                ViewBag.ReturnUrl = Url.Action("ChangeEmail");
                ViewBag.ViewOptions = new ViewOptions
                {
                    IsPrincipal = true,
                    IsFixed = true,
                };
                var user = CServices.ServiceUser.Find(UserContext.CurrentUserId);
                if (user != null)
                {
                    ViewBag.HasLocalPassword = true;
                    //((ViewOptions)ViewBag.ViewOptions).InfoViewPartial = UsuarioController.GetInfo(user);
                }
                else
                {
                    ViewBag.HasLocalPassword = false;
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Email

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins, Others")]
        public ActionResult ChangeEmail(ChangeEmailModel model)
        {
            if ((Request.IsAuthenticated) && (User.Identity is UserIdentity))
            {
                ViewBag.ReturnUrl = Url.Action("ChangeEmail");
                ViewBag.ViewOptions = new ViewOptions
                {
                    IsPrincipal = true,
                    IsFixed = true,
                };

                var user = CServices.ServiceUser.Find(UserContext.CurrentUserId);
                if (user != null)
                {
                    ViewBag.HasLocalPassword = true;

                    ((ViewOptions) ViewBag.ViewOptions).InfoViewPartial = UsuarioController.GetInfo(user);
                    if (!ModelState.IsValid) ModelState.AddModelError("", "ErrorInvalidInput");
                    else
                    {
                        // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                        try
                        {
                            ServiceUser.ChangeEmail(user, model.Email, model.NewEmail);
                            return RedirectToAction("ChangeEmail", new { Message = ManageMessageId.ChangeEmailSuccess });

                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("", e.Message);
                        }
                    }
                }
                else
                {
                    ViewBag.HasLocalPassword = false;
                }
                // If we got this far, something failed, redisplay form

                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/StoredData

        [Authorize(Roles = "Admins, Others")]
        public ActionResult StoredData(ManageMessageId? message)
        {
            if ((Request.IsAuthenticated) && (User.Identity is UserIdentity))
            {
                ViewBag.StatusMessage = message == ManageMessageId.ChangeEmailSuccess ? "Your personal data has been changed." : "";
                ViewBag.ReturnUrl = Url.Action("StoredData");
                var user = CServices.ServiceUser.Find(UserContext.CurrentUserId);

                ViewBag.ViewOptions = new ViewOptions
                {
                    IsPrincipal = true,
                    IsFixed = true,
                    //InfoViewPartial = UsuarioController.GetInfo(user),
                };
                ViewBag.HasLocalPassword = user != null;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/StoredData

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins, Others")]
        public ActionResult StoredData(StoredDataModel model)
        {
            if ((Request.IsAuthenticated) && (User.Identity is UserIdentity))
            {
                ViewBag.ReturnUrl = Url.Action("StoredData");
                ViewBag.ViewOptions = new ViewOptions
                {
                    IsPrincipal = true,
                    IsFixed = true,
                };
                var user = CServices.ServiceUser.Find(UserContext.CurrentUserId);
                if (user != null)
                {
                    ViewBag.HasLocalPassword = true;
                    ((ViewOptions)ViewBag.ViewOptions).InfoViewPartial = UsuarioController.GetInfo(user);
                    if (!ModelState.IsValid) ModelState.AddModelError("", "ErrorInvalidInput");
                    else
                    {
                        // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                        try
                        {
                            //ServiceUser.ChangePersonalData(user, model.Name, model.LastName, model.IdPais, model.Phone);
                            return RedirectToAction("StoredData", new { Message = ManageMessageId.ChangeEmailSuccess });

                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("", e.Message);
                        }
                    }
                }
                else
                {
                    ViewBag.HasLocalPassword = false;
                }
                // If we got this far, something failed, redisplay form
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }


        #region Helpers
        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("FrameWork", "Home");
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            ChangeEmailSuccess,
            ChangeDataSuccess,

        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                //OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        /*private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }*/
        #endregion
    }
}
