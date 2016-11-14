using System;
using Apl.Data.Artifacts;
using Apl.Entities.Domain;

namespace Apl.Data.MainServices
{
  public class FrameworkDataServiceFactory : IDataServiceFactory, IDisposable
  {
    private ModelContainer _myDbContext;


    public ModelContainer MyDbContext
    {
        get { return _myDbContext ?? (_myDbContext = new ModelContainer()); }
    }

    public void Dispose()
    {
        if (_myDbContext != null) _myDbContext.Dispose();
    }

  }

}
