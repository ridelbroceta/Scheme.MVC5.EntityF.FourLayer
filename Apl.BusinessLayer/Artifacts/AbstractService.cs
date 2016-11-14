using System;
using Apl.Data.Artifacts;
using Apl.Data.MainServices;
using Apl.BusinessLayer.MainServices;

namespace Apl.BusinessLayer.Artifacts
{

    public class AbstractServiceApplicationException : ApplicationException
    {
        public AbstractServiceApplicationException(string message): base(message) {}
    }

    public abstract class AbstractService
    {
        private readonly FrameworkServiceFactory _serviceFactory;
        protected AbstractDataService OwnDataService;

        protected AbstractService(FrameworkServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        protected FrameworkServiceFactory ServiceFactory
        {
            get { return _serviceFactory; }
        }

        protected FrameworkDataServiceFactory MyDataServiceFactory
        {
            get { return _serviceFactory.MyDataServiceFactory; }
        }
    }

}
