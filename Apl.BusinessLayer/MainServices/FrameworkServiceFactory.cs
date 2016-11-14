using System;
using Apl.Data.MainServices;
using Apl.BusinessLayer.Artifacts;
using Apl.BusinessLayer.Services;

namespace Apl.BusinessLayer.MainServices
{
  public class FrameworkServiceFactory : IServiceFactory, IDisposable
  {

      private readonly FrameworkDataServiceFactory _myDataServiceFactory;

      private ServiceOUser _serviceOUser;
      private ServiceUser _serviceUser;
      private ServiceRole _serviceRole;


      public FrameworkServiceFactory()
      {
          _myDataServiceFactory = new FrameworkDataServiceFactory();
      }

      public FrameworkDataServiceFactory MyDataServiceFactory {
          get { return _myDataServiceFactory ; }
      }


      public ServiceOUser ServiceOUser
      {
          get { return _serviceOUser ?? (_serviceOUser = new ServiceOUser(this)); }
      }

      public ServiceUser ServiceUser
      {
          get { return _serviceUser ?? (_serviceUser = new ServiceUser(this)); }
      }

      public ServiceRole ServiceRole
      {
          get { return _serviceRole ?? (_serviceRole = new ServiceRole(this)); }
      }


      public void Dispose()
      {
          if (_myDataServiceFactory != null)  _myDataServiceFactory.Dispose();
      }
  }

}
