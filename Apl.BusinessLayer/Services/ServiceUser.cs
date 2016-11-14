using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Apl.Data.Services;
using Apl.Entities.Domain;
using Apl.BusinessLayer.Artifacts;
using Apl.BusinessLayer.Domain;
using Apl.BusinessLayer.MainServices;

namespace Apl.BusinessLayer.Services
{
    public class ServiceUserException : AbstractServiceApplicationException
    {
        public ServiceUserException(string message) : base("User: " + message) { }
    }


    public class ServiceUser : AbstractService
    {
        #region private

        private DataServiceUser GetOwnDataService
        {
            get { return (DataServiceUser) OwnDataService ; }
        }

        private void ChangePass(user user, string newPass)
        {
            bool result = (user != null) && (!string.IsNullOrEmpty(newPass));
            if (!result) throw new ServiceUserException("ChangePass");
            try
            {
                user.Pass = MyCryptography.EncryptPass(newPass);
                user.DateLastPassChange = DateTime.Now;
                user.CountAfterPassAttempt = 0;
                GetOwnDataService.Update(user);
            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("ChangePass: " + e.Message);
            }
        }

        #endregion

        #region protected

        public bool AnyExceptThis(string email, int id)
        {
            return List().Any(p => ((p.Id != id) && (p.Email.Equals(email, StringComparison.OrdinalIgnoreCase))));
        }


        protected bool Verificar(string name, string lastname, string pass, string email, string phone, int idPais)
        {
            var result = Find(email) == null;
            if (!result) throw new ServiceUserException("Email registred");
            result = (!string.IsNullOrEmpty(name)) &&
                        (!string.IsNullOrEmpty(lastname)) &&
                        (!string.IsNullOrEmpty(pass)) &&
                        (!string.IsNullOrEmpty(email)) &&
                        (!string.IsNullOrEmpty(phone));
            return result;
        }

        protected void Fill(user user, string name, string lastname, string pass, string email, string phone, int idPais, int idUserCreated)
        {
            user.Name = name;
            user.LastName = lastname;
            user.Pass = MyCryptography.EncryptPass(pass);
            user.Email = email.ToLower();
            user.Phone = phone;
            user.IsActive = true;
            user.IsLocked = false;
            if (idUserCreated > 0) user.UserCreated = idUserCreated;
            user.DateCreated = DateTime.Now;
        }


        #endregion 

        #region public

        public ServiceUser(FrameworkServiceFactory serviceFactory): base(serviceFactory)
        {
            OwnDataService = new DataServiceUser(MyDataServiceFactory);
        }

        #region get

        public int CountUser()
        {
            return List().Count();
        }

        public bool HasLocalAccount(int id)
        {
            var user = Find(id);
            return (user != null);
        }

        public string RolesToString(user user)
        {
            return (user.Roles != null) ? string.Join(",", user.Roles.Select(rol => rol.Desc)) : string.Empty;
        }

        public string[] RolesToStringArray(user user)
        {
            return (user != null) && (user.Roles != null) ? user.Roles.Select(rol => rol.Desc).ToArray() : new string[] { };
        }

        public bool IsUserInRole(user user, string role)
        {
            return (user != null) && user.Roles != null && user.Roles.Any(r => r.Desc.Equals(role, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsUserInRole(user user, int idRole)
        {
            return (user != null) && user.Roles != null && user.Roles.Any(r => r.Id == idRole);
        }

        public MyConstant.EnumLoginError Authenticate(user user, string pass)
        {
            var result = MyConstant.EnumLoginError.NoError;
            if (user == null) return MyConstant.EnumLoginError.NoUserRegistered;
            if (user.IsConnected) result = MyConstant.EnumLoginError.NoError; //myConstant.enumLoginError.UserConnected;
            else if (user.IsLocked) result = MyConstant.EnumLoginError.UserLocked;
            if ((result == MyConstant.EnumLoginError.NoError) && (user.Pass != MyCryptography.EncryptPass(pass)))
                result = MyConstant.EnumLoginError.IncorrectPassword;
            return result;
        }

        public bool IsUserConnect(user user)
        {
            bool result = (user != null);
            if (result)
                result = user.IsConnected;
            return result;
        }

        public IQueryable<user> List()
        {
            return GetOwnDataService.Gets().Where(p => p.IsActive);
        }

        public bool Any(string email)
        {
            return List().Any(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public user Find(int id)
        {
            var repository = List().SingleOrDefault(p => (p.Id == id));
            return repository;
        }

        public user Find(string email)
        {
            var repository = List().SingleOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return repository;
        }

        public static user StaticFind(int id)
        {
            using (var servicios = new FrameworkServiceFactory())
            {
                var repository = servicios.ServiceUser.Find(id);
                return repository;
            }
        }


        #endregion

        #region set

        public static void ChangePass(user user, string oldPass, string newPass)
        {
            var result = (user != null) &&
                           (!string.IsNullOrEmpty(oldPass)) &&
                           (!string.IsNullOrEmpty(newPass)) &&
                           (user.Pass.Equals(MyCryptography.EncryptPass(oldPass), StringComparison.OrdinalIgnoreCase));
            if (!result) throw new ServiceUserException("ChangePass");
            OperChangePass(user, newPass);
        }

        private static void OperChangePass(user user, string newPass)
        {
            bool result = (user != null) && (!string.IsNullOrEmpty(newPass));
            if (!result) throw new ServiceUserException("ChangePass");
            try
            {
                using (var servicios = new FrameworkServiceFactory())
                {
                    user = servicios.ServiceUser.Find(user.Id);
                    user.Pass = MyCryptography.EncryptPass(newPass);
                    user.DateLastPassChange = DateTime.Now;
                    user.CountAfterPassAttempt = 0;
                    using (var transaction = new TransactionScope())
                    {
                        servicios.ServiceUser.GetOwnDataService.Update(user);
                        transaction.Complete();
                    }
                }
            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("ChangePass: " + e.Message);
            }
        }

        public static void ChangeEmail(user user, string oldEmail, string newEmail)
        {
            var result = (user != null) &&
                           (!string.IsNullOrEmpty(oldEmail)) &&
                           (!string.IsNullOrEmpty(newEmail)) &&
                           (user.Email.Equals(oldEmail, StringComparison.OrdinalIgnoreCase));
            if (!result) throw new ServiceUserException("ChangeEmail");
            using (var servicios = new FrameworkServiceFactory())
            {
                user = servicios.ServiceUser.Find(user.Id);
                if (servicios.ServiceUser.AnyExceptThis(newEmail, user.Id)) throw new ServiceUserException("Email registred");
                user.DateUpdated = DateTime.Now;
                user.UserUpdated = user.Id;
                user.Email = newEmail.ToLower();
                using (var transaction = new TransactionScope())
                {
                    servicios.ServiceUser.GetOwnDataService.Update(user);
                    transaction.Complete();
                }
            }
        }

        public static void ChangePersonalData(user user, string name, string lastName, int idPais, string phone)
        {
            var result = (user != null) &&
                           (!string.IsNullOrEmpty(name)) &&
                           (!string.IsNullOrEmpty(lastName)) &&
                           (!string.IsNullOrEmpty(phone)) &&
                           (idPais > 0);
            if (!result) throw new ServiceUserException("ChangePersonalData");
            using (var servicios = new FrameworkServiceFactory())
            {
                user = servicios.ServiceUser.Find(user.Id);
                user.DateUpdated = DateTime.Now;
                user.UserUpdated = user.Id;
                user.Name = name;
                user.LastName = lastName;
                user.Phone = phone;
                using (var transaction = new TransactionScope())
                {
                    servicios.ServiceUser.GetOwnDataService.Update(user);
                    transaction.Complete();
                }

            }
        }

        public static void ResetPass(user user, string newPass, int userResetPass)
        {
            var result = (user != null) && (!string.IsNullOrEmpty(newPass));
            if (!result) throw new ServiceUserException("ResetPass");
            using (var servicios = new FrameworkServiceFactory())
            {
                user = servicios.ServiceUser.Find(user.Id);
                user.UserResetPass = userResetPass;
                user.DateResetPass = DateTime.Now;
                using (var transaction = new TransactionScope())
                {
                    servicios.ServiceUser.ChangePass(user, newPass);
                    transaction.Complete();
                }
            }
        }

        public static void ChangeLocked(user user, int userLastLockout)
        {
            var result = (user != null);
            if (!result) throw new ServiceUserException("ChangeLocked");
            try
            {
                using (var servicios = new FrameworkServiceFactory())
                {
                    user = servicios.ServiceUser.Find(user.Id);
                    if (user.IsLocked)
                    {
                        user.IsLocked = false;
                        user.CountFailedPassAttempt = 0;
                    }
                    else user.IsLocked = true;
                    user.DateLastLockout = DateTime.Now;
                    user.UserLastLockout = userLastLockout;

                    using (var transaction = new TransactionScope())
                    {
                        servicios.ServiceUser.GetOwnDataService.Update(user);
                        transaction.Complete();
                    }
                }
            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("ChangeLocked: " + e.Message);
            }
        }

        public void ChangeLocked(user user, bool newValue, int userLastLockout)
        {
            bool result = (user != null);
            if (!result) throw new ServiceUserException("ChangeLocked");
            try
            {
                if (user.IsLocked && (user.IsLocked != newValue))
                {
                    user.CountFailedPassAttempt = 0;
                }
                user.IsLocked = newValue;
                user.DateLastLockout = DateTime.Now;
                user.UserLastLockout = userLastLockout;
                GetOwnDataService.Update(user);
            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("ChangeLocked: " + e.Message);
            }
        }

        public static void DesconnectedUser(int idCurrentUser)
        {
            try
            {
                using (var servicios = new FrameworkServiceFactory())
                {
                    var user = servicios.ServiceUser.Find(idCurrentUser);
                    if (user != null)
                    {
                        user.IsConnected = false;
                        using (var transaction = new TransactionScope())
                        {
                            servicios.ServiceUser.GetOwnDataService.Update(user);
                            transaction.Complete();
                        }
                    }
                }
            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("DesconnectedUser: " + e.Message);
            }
        }

        public void LoginUser(user user)
        {
            if (user == null) throw new ServiceUserException("LoginUser");
            try
            {
                user.IsConnected = true;
                user.DateLastLogin = DateTime.Now;
                using (var transaction = new TransactionScope())
                {
                    GetOwnDataService.Update(user);
                    transaction.Complete();
                }

            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("LoginUser: " + e.Message);
            }
        }

        public bool RemoveRole(user user, role role, int userLastLockout)
        {
            if (user == null) throw new ServiceUserException("RemoveRole");
            try
            {
                var result = user.Roles.Remove(role);
                if (result)
                {
                    if ((user.Roles == null) || (user.Roles.Count == 0))
                    {
                        user.IsLocked = true;
                        user.DateLastLockout = DateTime.Now;
                        user.UserLastLockout = userLastLockout;
                    }
                    GetOwnDataService.Update(user);
                }
                return result;
            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("RemoveRole: " + e.Message);
            }
        }

        public static void UpdateUserRoles(user user, ICollection<role> roles, int userLastLockout)
        {
            try
            {
                using (var servicios = new FrameworkServiceFactory())
                {
                    user = servicios.ServiceUser.Find(user.Id);

                    if ((roles != null) && (roles.Count > 0))
                    {
                        user.Roles.Clear();
                        foreach (var item in roles)
                        {
                            user.Roles.Add(servicios.ServiceRole.Find(item.Id));
                        }
                    }
                    else
                    {
                        user.IsLocked = true;
                        user.DateLastLockout = DateTime.Now;
                        user.UserLastLockout = userLastLockout;
                    }
                    using (var transaction = new TransactionScope())
                    {
                        servicios.ServiceUser.GetOwnDataService.Update(user);
                        transaction.Complete();
                    }
                }
            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("UpdateUserRoles: " + e.Message);
            }
        }

        public static void Delete(user user)
        {
            if (user == null) throw new ServiceUserException("Delete");

            try
            {
                using (var servicios = new FrameworkServiceFactory())
                {
                    var userId = user.Id;
                    user = servicios.ServiceUser.Find(userId);
                    using (var transaction = new TransactionScope())
                    {
                        user.Roles.Clear();
                        servicios.ServiceUser.GetOwnDataService.Delete(user);

                        transaction.Complete();
                    }
                }
            }
            catch (ApplicationException e)
            {
                throw new ServiceUserException("Delete: " + e.Message);
            }
        }

        #endregion

        #endregion


    }
}
