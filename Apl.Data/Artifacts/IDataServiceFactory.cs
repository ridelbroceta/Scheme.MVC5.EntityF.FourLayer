using Apl.Entities.Domain;

namespace Apl.Data.Artifacts
{
  public interface IDataServiceFactory
  {
      ModelContainer MyDbContext { get; }
  }
}
