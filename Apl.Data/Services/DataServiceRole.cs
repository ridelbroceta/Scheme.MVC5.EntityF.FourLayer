using System.Linq;
using Apl.Data.Artifacts;
using Apl.Entities.Domain;

namespace Apl.Data.Services
{
    public class DataServiceRole : AbstractDataService
    {
        #region private
        #endregion
        #region protected


        #endregion

        #region public

        public DataServiceRole(IDataServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }

        #region get

        public IQueryable<role> Gets()
        {
            return MyDbContext.roles;
        }

        public role Find(int id)
        {
            var repository = MyDbContext.roles.SingleOrDefault(p => (p.Id == id));
            return repository;
        }


        #endregion

        #region set


        #endregion

        #endregion

    }
}
