using System;
using TowerDefence.Infrastructure.Factory;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using TowerDefence.Logic;
using TowerDefence.UI;
using UnityEngine;

namespace TowerDefence.Infrastructure.States
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        private const string TOWER_INITIAL_POINT_TAG = "TowerInitialPoint";
        private const string  PLACE_INITIAL_SPAWN_POINTS_TAG = "ContainerEnemySpawnPoints";

        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly LoadingCurtain loadingCurtain;
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService persistentProgressService;

        public LoadLevelState(GameStateMachine _gameStateMachine, SceneLoader _sceneLoader, LoadingCurtain _loadingCurtain, IGameFactory _gameFactory, IPersistentProgressService _persistentProgressService)
        {
            gameStateMachine = _gameStateMachine;
            sceneLoader = _sceneLoader;
            loadingCurtain = _loadingCurtain;
            gameFactory = _gameFactory;
            persistentProgressService = _persistentProgressService;
        }

        public void Enter(string _sceneName)
        {
            loadingCurtain.Show();
            gameFactory.Cleanup();
            sceneLoader.Load(_sceneName, OnLoaded);
        }

        public void Exit()
        {
            loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();

            InformProgressReaders();

            gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in gameFactory.progressReaders)
            {
                progressReader.LoadProgress(persistentProgressService.progress);
            }
        }

        private void InitGameWorld()
        {
            gameFactory.CreateTower(GameObject.FindWithTag(TOWER_INITIAL_POINT_TAG));
            gameFactory.CreateEnemySpawnPoints(GameObject.FindWithTag(PLACE_INITIAL_SPAWN_POINTS_TAG));
            gameFactory.CreateCurrency();
            gameFactory.CreateHud();

            gameFactory.CreateWaveSpawner();
        }
    }
}