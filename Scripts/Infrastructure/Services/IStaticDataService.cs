using TowerDefence.StaticData;

namespace TowerDefence.Infrastructure.Services
{
    public interface IStaticDataService :IService
    {
        void LoadEnemies();
        EnemyStaticData SearchEnemyStaticData(EnemyTypeId _typeId);
        void LoadTower();
        TowerStaticData GetTowerStaticData();

        void LoadDamageDistanceImprovement();

        void LoadDamageImprovement();

        void LoadSpeedImprovement();

        ImprovementStaticData GetDamageDistanceData();
        ImprovementStaticData GetDamageData();
        ImprovementStaticData GetSpeedData();
    }
}