using System;
using TowerDefence.Data;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using TowerDefence.Infrastructure.Services.SaveLoad;

namespace TowerDefence.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IPersistentProgressService persistentProgressService;
        private readonly ISaveLoadService saveLoadService;

        public LoadProgressState(GameStateMachine _gameStateMachine, IPersistentProgressService _persistentProgressService, ISaveLoadService _saveLoadService)
        {
            gameStateMachine = _gameStateMachine;
            persistentProgressService = _persistentProgressService;
            saveLoadService = _saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            gameStateMachine.Enter<LoadLevelState, string>("GameScene");
            saveLoadService.SaveProgress();
        }

        public void Exit()
        {

        }

        private void LoadProgressOrInitNew()
        {
            persistentProgressService.progress = saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(_damageDistance: new Vector3Data(1, 1, 1), _bulletSpeed: 2.5f, _damage: 1f);

            return progress;
        }
    }
}