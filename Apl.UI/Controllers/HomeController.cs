using System;
using System.Web;
using System.Web.Mvc;
using Apl.BusinessLayer.Domain;
using Apl.BusinessLayer.MainServices;
using Apl.UI.Artifacts;
using Apl.UI.Models;
using Apl.UI.Security;

namespace Apl.UI.Controllers
{
     
    public class HomeController : MyBaseController
    {

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Index(string returnUrl = null)
        {
            const string baseHome = "/Home/Index";
            const string baseHomeFrameWork = "/Home/Framework";
            if (string.IsNullOrEmpty(returnUrl) || (returnUrl.Equals(baseHome, StringComparison.OrdinalIgnoreCase))) returnUrl = baseHomeFrameWork;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid) ModelState.AddModelError("", "ErrorInvalidInput");
            else
            {
                try
                {
                    MyConstant.EnumLoginError autUser;
                    using (var servicios = new FrameworkServiceFactory())
                    {
                        var user = servicios.ServiceUser.Find(model.Email);
                        autUser = servicios.ServiceUser.Authenticate(user, model.Password);

                        if (autUser == MyConstant.EnumLoginError.NoError)
                        {
                            servicios.ServiceUser.LoginUser(user);
                            UserContext.Login(user, false);
                                return RedirectToAction("RedirectToLocal", "Account", new { returnUrl });
                        }
                    }

                    switch (autUser)
                    {
                        case MyConstant.EnumLoginError.UserLocked:
                            ModelState.AddModelError("", "ErrorUserLocked");
                            break;
                        case MyConstant.EnumLoginError.UserConnected:
                            ModelState.AddModelError("", "ErrorUserConnected");
                            break;
                        case MyConstant.EnumLoginError.NoUserRegistered:
                            ModelState.AddModelError("", "ErrorNoUserRegistered");
                            break;
                        case MyConstant.EnumLoginError.IncorrectPassword:
                            ModelState.AddModelError("", "ErrorIncorrectPassword");
                            break;
                        case MyConstant.EnumLoginError.PasswordOlder:
                            ModelState.AddModelError("", "ErrorPasswordOlder");
                            break;
                        case MyConstant.EnumLoginError.UserWait:
                            ModelState.AddModelError("", "ErrorUserWait");
                            break;
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            // If we got this far, something failed, redisplay form
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        public ActionResult ServerSide404(int id)
        {
            HttpContext.AddError(new HttpException(404, "Invalid Id - " + id));
            //return View();
            return null;
        }

       // [Authorize(Roles = "Admins, Others")]

        public ActionResult FrameWork()
        {
            if ((Request.IsAuthenticated) && (User.Identity is UserIdentity))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfigInc(string message = null)
        {
            ViewBag.Message = message;
            return View();
        }


        [HttpPost]
        public ActionResult File_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

    }
}
