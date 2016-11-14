using System.Linq;
using Apl.Data.Services;
using Apl.Entities.Domain;
using Apl.BusinessLayer.Artifacts;
using Apl.BusinessLayer.Domain;
using Apl.BusinessLayer.MainServices;

namespace Apl.BusinessLayer.Services
{
    public class ServiceRole : AbstractService
    {
        #region private
        private DataServiceRole GetOwnDataService
        {
            get { return (DataServiceRole)OwnDataService; }
        }

        #endregion
        #region protected


        #endregion

        #region public

        public ServiceRole(FrameworkServiceFactory serviceFactory)
            : base(serviceFactory)
        {
            OwnDataService = new DataServiceRole(MyDataServiceFactory);
        }


        #region get
        public IQueryable<role> List()
        {
            return GetOwnDataService.Gets();
        }

        public role Find(int id)
        {
            var repository = List().SingleOrDefault(p => (p.Id == id));
            return repository;
        }

        public string[] RolesToStringArray()
        {
            var roles = List();
            return roles != null ? roles.Select(rol => rol.Desc).ToArray() : new string[] { };
        }

        #endregion

        #region set


        #endregion

        #endregion

    }
}
