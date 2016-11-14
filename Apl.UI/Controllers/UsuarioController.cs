using System.Linq;
using Apl.UI.Artifacts;
using Apl.UI.Models;
using Apl.Entities.Domain;


namespace Apl.UI.Controllers
{
    public class UsuarioController : MyBaseController
    {

         public static InfoViewPartial GetInfo(user user)
         {
             var result = new InfoViewPartial
             {
                 Prefijo = ReflectionExtensions.MyGetPropertyDisplayName<user>(i => i.Id),
             };

             result.Body.Add(ReflectionExtensions.MyGetPropertyDisplayName<user>(i => i.Email), user.Email);
             result.Body.Add(ReflectionExtensions.MyGetPropertyDisplayName<user>(i => i.Name), user.Name);
             result.Body.Add(ReflectionExtensions.MyGetPropertyDisplayName<user>(i => i.LastName), user.LastName);
             result.Body.Add(ReflectionExtensions.MyGetPropertyDisplayName<user>(i => i.Roles), user.Roles.First().Desc);
             result.Body.Add(ReflectionExtensions.MyGetPropertyDisplayName<user>(i => i.IsLocked), user.IsLocked ? "Yes" : "NO");
             return result;
         }


         #region Json

         #endregion

         #region private
 
         #endregion

    }
}