using System.Data.Entity;
using System.Linq;
using Apl.Data.Artifacts;
using Apl.Entities.Domain;


namespace Apl.Data.Services
{
    public class DataServiceUser: AbstractDataService
    {

        #region protected

        #endregion

        #region public

        public DataServiceUser(IDataServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }

        #region select

        public IQueryable<user> Gets()
        {
            return MyDbContext.users;
        }



        #endregion

        #region set
        public void Update(user user)
        {
            MyDbContext.Entry(user).State = EntityState.Modified;
            MyDbContext.SaveChanges();
        }

        public void Delete(user user)
        {
            MyDbContext.users.Remove(user);
            MyDbContext.SaveChanges();
        }

        #endregion
    #endregion
    }
}

