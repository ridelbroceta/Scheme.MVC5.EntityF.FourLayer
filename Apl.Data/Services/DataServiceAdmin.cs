using Apl.Data.Artifacts;
using Apl.Entities.Domain;

namespace Apl.Data.Services
{
    public class DataServiceAdmin : DataServiceUser
    {

        #region private
        #endregion

        #region public
        public DataServiceAdmin(IDataServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }

        #region select

        #endregion

        #region set

        public void Add(admin user)
        {
            MyDbContext.users.Add(user);
            MyDbContext.SaveChanges();
        }

        #endregion

        #endregion
    }
}
