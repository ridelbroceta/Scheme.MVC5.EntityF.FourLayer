using System.Linq;
using Apl.Data.Services;
using Apl.Entities.Domain;
using Apl.BusinessLayer.Artifacts;
using Apl.BusinessLayer.MainServices;

namespace Apl.BusinessLayer.Services
{

    public class ServiceOUserException : AbstractServiceApplicationException
    {
        public ServiceOUserException(string message) : base("OUser: " + message) { }
    }


    public class ServiceOUser : ServiceUser
    {
        #region private

        private DataServiceWorker GetOwnDataService
        {
            get { return (DataServiceWorker)OwnDataService; }
        }

        private new IQueryable<ouser> List()
        {
            return base.List().OfType<ouser>();
        }

        #endregion
        #region protected


        #endregion

        #region public

        public ServiceOUser(FrameworkServiceFactory serviceFactory)
            : base(serviceFactory)
        {
            OwnDataService = new DataServiceWorker(MyDataServiceFactory);
        }

        
        #region get

        public new ouser Find(int id)
        {
            return (base.Find(id) as ouser);
        }

        #endregion

        #region set

        public static void Create(string name, string lastname, string pass, string email, string phone)
        {
        }


        #endregion

        #endregion
    }
}
