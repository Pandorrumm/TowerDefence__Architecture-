
using TowerDefence.Data;

namespace TowerDefence.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
       public PlayerProgress progress { get; set; }
    }
}
