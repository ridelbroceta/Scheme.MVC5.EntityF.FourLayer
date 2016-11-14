using Apl.Entities.Domain;

namespace Apl.Data.Artifacts
{
    public abstract class AbstractDataService
    {

        private readonly IDataServiceFactory _serviceFactory;

        protected AbstractDataService(IDataServiceFactory serviceFactory)
        {
          _serviceFactory = serviceFactory;
        }

        /*protected IDataServiceFactory ServiceFactory
        {
          get { return _serviceFactory; }
        }*/

        protected ModelContainer MyDbContext
        {
          get { return _serviceFactory.MyDbContext; }
        }
    }

}
