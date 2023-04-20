using TowerDefence.Data;
using TowerDefence.Infrastructure.Factory;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace TowerDefence.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private string ProgressKey = "Progress";

        private readonly IPersistentProgressService progressService;
        private readonly IGameFactory gameFactory;

        public SaveLoadService(IPersistentProgressService _progressService,  IGameFactory _gameFactory)
        {
            progressService = _progressService;
            gameFactory = _gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in gameFactory.progressWriters)
            {
                progressWriter.UpdateProgress(progressService.progress);
            }

            PlayerPrefs.SetString(ProgressKey, progressService.progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}