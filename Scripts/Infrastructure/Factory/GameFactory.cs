using System;
using System.Collections.Generic;
using TowerDefence.Infrastructure.AssetManagement;
using TowerDefence.Infrastructure.Services;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using TowerDefence.StaticData;
using UnityEngine;
using TowerDefence.Enemy;
using TowerDefence.Tower;
using TowerDefence.WaveOfEnemies;
using TowerDefence.LootEnemy;
using TowerDefence.UI;
using TowerDefence.Bullet;

namespace TowerDefence.Infrastructure.Factory
{
    public partial class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assets;
        private readonly IStaticDataService staticData;
        private IPersistentProgressService persistentProgressService;

        private GameObject towerGameObject { get;  set; }
        private GameObject waveSpawner { get; set; }
        private GameObject hud { get; set; }

        private List<Transform> enemySpawnPoints = new List<Transform>();

        public List<ISavedProgressReader> progressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> progressWriters { get; } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider _assets, IStaticDataService _staticData, IPersistentProgressService _persistentProgressService)
        {
            assets = _assets;
            staticData = _staticData;
            persistentProgressService = _persistentProgressService;
        }

        public GameObject CreateTower(GameObject _at)
        {
            TowerStaticData towerStaticData = staticData.GetTowerStaticData();
            towerGameObject = InstantiateRegistered(AssetPath.TOWER_PATH, _at.transform.position);

            var towerHealth = towerGameObject.GetComponent<TowerHealth>();
            towerHealth.Current = towerStaticData.hp;
            towerHealth.Max = towerStaticData.hp;

            return towerGameObject;
        }

        public GameObject CreateWaveSpawner()
        {
            waveSpawner = InstantiateRegistered(AssetPath.WAVE_SPAWNER_PATH);
            waveSpawner.GetComponent<WaveSpawner>().Construct(enemySpawnPoints, hud.GetComponentInChildren<WaveCounter>());         
            return waveSpawner;
        }

        public GameObject CreateEnemy(EnemyTypeId _enemyTypeId)
        {
            EnemyStaticData enemyStaticData = staticData.SearchEnemyStaticData(_enemyTypeId);
            GameObject enemy = GameObject.Instantiate(enemyStaticData.prefab);

            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.Current = enemyStaticData.hp;
            enemyHealth.Max = enemyStaticData.hp;

            var enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.Construct(towerGameObject.transform);
            enemyMovement.DistanceToTarget = enemyStaticData.stopDistance;
            enemyMovement.MinSpeed = enemyStaticData.minSpeed;
            enemyMovement.MaxSpeed = enemyStaticData.maxSpeed;

            var enemyAttack = enemy.GetComponent<EnemyAttack>();
            enemyAttack.Construct(towerGameObject);
            enemyAttack.Damage = enemyStaticData.damage;

            var enemyDeath = enemy.GetComponent<EnemyDeath>();
            enemyDeath.Construct(towerGameObject.GetComponent<TowerAttack>(), waveSpawner.GetComponent<WaveSpawner>());

            var lootSpawner = enemy.GetComponent<LootSpawner>();
            lootSpawner.SetLoot(enemyStaticData.lootMoney);

            lootSpawner.Construct(this);

            return enemy;
        }

        public GameObject CreateLoot()
        {
            GameObject loot = InstantiateRegistered(AssetPath.LOOT_PATH);
            loot.GetComponent<LootPiece>().Construct(persistentProgressService.progress.moneyData);
            return loot;
        }

        public GameObject CreateEnemySpawnPoints(GameObject _at)
        {
            GameObject enemySpawnPointParent = InstantiateRegistered(AssetPath.ENEMY_SPAWN_POINTS_PATH, _at.transform.position);

            for (int i = 0; i < enemySpawnPointParent.transform.childCount; i++)
            {
                enemySpawnPoints.Add(enemySpawnPointParent.transform.GetChild(i).transform);
            }

            return enemySpawnPointParent;
        }

        public GameObject CreateHud()
        {
            hud = InstantiateRegistered(AssetPath.GAME_CANVAS_PATH);

            ImprovementStaticData speedData = staticData.GetSpeedData();
            ImprovementStaticData damageData = staticData.GetDamageData();
            ImprovementStaticData damageDistanceData = staticData.GetDamageDistanceData();


            hud.GetComponentInChildren<Improvements>().Construct(persistentProgressService.progress.moneyData,
                                                                 towerGameObject.GetComponentInChildren<BulletMovement>(),
                                                                 towerGameObject.GetComponentInChildren<BulletDamage>(),
                                                                 towerGameObject.GetComponentInChildren<AttackZone>(),
                                                                 speedData, damageData, damageDistanceData);

            hud.GetComponentInChildren<ActorUI>().Construct(towerGameObject.GetComponent<TowerHealth>());
            hud.GetComponentInChildren<LootCounter>().Construct(persistentProgressService.progress.moneyData);
            return hud;
        }

        public GameObject CreateCurrency() => 
            InstantiateRegistered(AssetPath.CURRENCY_PATH);

        public void Cleanup()
        {
            progressReaders.Clear();
            progressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string _prefabPath, Vector2 _position)
        {
            GameObject gameObject = assets.Instantiate(_prefabPath, _position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string _prefabPath)
        {
            GameObject gameObject = assets.Instantiate(_prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader _progressReader)
        {
            if (_progressReader is ISavedProgress _progressWriter)
            {
                progressWriters.Add(_progressWriter);
            }

            progressReaders.Add(_progressReader);
        }
    }
}
