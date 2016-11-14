using Apl.Data.Artifacts;
using Apl.Entities.Domain;

namespace Apl.Data.Services
{
    public class DataServiceWorker : DataServiceUser
    {
        #region public

        public DataServiceWorker(IDataServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }

        #region select

        #endregion

        #region set

        public void Add(ouser user)
        {
            MyDbContext.users.Add(user);
            MyDbContext.SaveChanges();
        }

        #endregion

        #endregion

    }
}
