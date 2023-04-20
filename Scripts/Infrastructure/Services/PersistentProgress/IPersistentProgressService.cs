using TowerDefence.Data;

namespace TowerDefence.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress progress { get; set; }
    }
}