using System.Collections.Generic;
using TowerDefence.Infrastructure.Services;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using TowerDefence.StaticData;
using TowerDefence.UI;
using UnityEngine;

namespace TowerDefence.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateTower(GameObject _at);
        GameObject CreateHud();
        GameObject CreateWaveSpawner();
        List<ISavedProgressReader> progressReaders { get; }
        List<ISavedProgress> progressWriters { get; }
        void Cleanup();
        GameObject CreateEnemy(EnemyTypeId _enemyTypeId);
        GameObject CreateLoot();
        GameObject CreateEnemySpawnPoints(GameObject _at);
        GameObject CreateCurrency();
    }
}