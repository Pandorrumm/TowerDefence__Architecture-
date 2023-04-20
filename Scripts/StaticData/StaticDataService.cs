using System.Collections.Generic;
using System.Linq;
using TowerDefence.Infrastructure.Services;
using UnityEngine;

namespace TowerDefence.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, EnemyStaticData> enemies;
        private TowerStaticData towerStaticData;
        private ImprovementStaticData damageDistanceStaticData;
        private ImprovementStaticData damageStaticData;
        private ImprovementStaticData speedStaticData;

        public void LoadEnemies()
        {
            enemies = Resources.LoadAll<EnemyStaticData>("StaticData/Enemies")
                .ToDictionary(x => x.enemyTypeId, x => x);
        }

        public void LoadTower() => 
            towerStaticData = Resources.Load<TowerStaticData>("StaticData/Tower/TowerData");

        public void LoadDamageDistanceImprovement() => 
            damageDistanceStaticData = Resources.Load<ImprovementStaticData>("StaticData/TowerImprovement/DamageDistanceImprovement");

        public void LoadDamageImprovement() => 
            damageStaticData = Resources.Load<ImprovementStaticData>("StaticData/TowerImprovement/DamageImprovement");

        public void LoadSpeedImprovement() => 
            speedStaticData = Resources.Load<ImprovementStaticData>("StaticData/TowerImprovement/SpeedImprovement");

        public EnemyStaticData SearchEnemyStaticData(EnemyTypeId _typeId)
        {
            if (enemies.TryGetValue(_typeId, out EnemyStaticData enemyStaticData))
            {
                return enemyStaticData;
            }

            return null;
        }

        public TowerStaticData GetTowerStaticData() =>
            towerStaticData;

        public ImprovementStaticData GetDamageDistanceData() =>
            damageDistanceStaticData;

        public ImprovementStaticData GetDamageData() =>
            damageStaticData;

        public ImprovementStaticData GetSpeedData() =>
            speedStaticData;
    }
}
