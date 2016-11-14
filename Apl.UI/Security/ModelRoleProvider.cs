using System;
using System.Web.Security;
using Apl.BusinessLayer.MainServices;

namespace Apl.UI.Security
{
    public class ModelRoleProvider: RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            using (var servicios = new FrameworkServiceFactory())
            {
                var user = servicios.ServiceUser.Find(username);
                return servicios.ServiceUser.RolesToStringArray(user);
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var servicios = new FrameworkServiceFactory())
            {
                var user = servicios.ServiceUser.Find(username);
                return servicios.ServiceUser.IsUserInRole(user, roleName);
            }
        }

        public override string[] GetAllRoles()
        {
            using (var servicios = new FrameworkServiceFactory())
            {
                return servicios.ServiceRole.RolesToStringArray();
            }
        }
        
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}