using Apl.Data.MainServices;

namespace Apl.BusinessLayer.Artifacts
{
  public interface IServiceFactory
  {
    FrameworkDataServiceFactory MyDataServiceFactory { get; }
  }
}
