using TowerDefence.Infrastructure.AssetManagement;
using TowerDefence.Infrastructure.Factory;
using TowerDefence.Infrastructure.Services;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using TowerDefence.Infrastructure.Services.SaveLoad;
using TowerDefence.StaticData;

namespace TowerDefence.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string NAME_SCENE = "Initial";
        private readonly GameStateMachine gameStateMachine;
        private readonly SceneLoader sceneLoader;
        private readonly AllServices allServices;

        public BootstrapState(GameStateMachine _gameStateMachine, SceneLoader _sceneLoader, AllServices _allServices)
        {
            gameStateMachine = _gameStateMachine;
            sceneLoader = _sceneLoader;
            allServices = _allServices;

            RegisterServices();
        }

        public void Enter()
        {
            sceneLoader.Load(NAME_SCENE, _onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel() => 
            gameStateMachine.Enter<LoadProgressState>();

         
        private void RegisterServices()
        {
            RegisterStaticData();
            allServices.RegisterSingle<IAssetProvider>(new AssetProvider());
            allServices.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            allServices.RegisterSingle<IGameFactory>(new GameFactory(allServices.Single<IAssetProvider>(), allServices.Single<IStaticDataService>(), allServices.Single<IPersistentProgressService>()));
            allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService(allServices.Single<IPersistentProgressService>(), allServices.Single<IGameFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadEnemies();
            staticData.LoadTower();
            staticData.LoadDamageDistanceImprovement();
            staticData.LoadDamageImprovement();
            staticData.LoadSpeedImprovement();
            allServices.RegisterSingle<IStaticDataService>(staticData);
        }
    }
}
