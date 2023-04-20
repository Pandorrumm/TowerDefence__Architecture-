using System;
using System.Collections.Generic;
using TowerDefence.Enemy;
using TowerDefence.Infrastructure.Factory;
using TowerDefence.Infrastructure.Services;
using TowerDefence.StaticData;
using TowerDefence.UI;
using UnityEngine;

namespace TowerDefence.WaveOfEnemies
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveTeamData> waveTeamsDatas = new List<WaveTeamData>();

        private int numberEnemies;
        private int indexWave = 0;

        private List<Transform> spawnPoints = new List<Transform>();
        private WaveCounter waveCounter;

        private IGameFactory gameFactory;

        public void Construct(List<Transform> _spawnPoints, WaveCounter _waveCounter)
        {
            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                spawnPoints.Add(_spawnPoints[i]);
            }

            waveCounter = _waveCounter;
        }

        private void Awake()
        {
            gameFactory = AllServices.container.Single<IGameFactory>();
        }

        private void Start()
        {
            StartCreationWave();
        }

        private void StartCreationWave()
        {
            if (indexWave >= waveTeamsDatas.Count)
            {
                return;
            }

            waveCounter.UpdateWaveCounterText(indexWave + 1);

            CreateEnemies();
        }

        private void CreateEnemies()
        {
            for (int i = 0; i < waveTeamsDatas[indexWave].enemies.Count; i++)
            {
                GameObject enemy = gameFactory.CreateEnemy(waveTeamsDatas[indexWave].enemies[i]);


                numberEnemies++;

                enemy.transform.position = GetRandomSpawnPoints(spawnPoints).position;
            }      
        }

        private Transform GetRandomSpawnPoints(List<Transform> _spawnPoints)
        {
            int max = _spawnPoints.Count;
            int min = 0;
            int index = UnityEngine.Random.Range(min, max);

            return _spawnPoints[index];
        }

        public void RemoveEnemy()
        {
            numberEnemies--;

            if (numberEnemies <= 0)
            {
                indexWave++;

                StartCreationWave();
            }
        }
    }
}
