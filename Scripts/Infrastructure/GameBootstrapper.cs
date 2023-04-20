using UnityEngine;
using TowerDefence.Logic;
using TowerDefence.Infrastructure.States;

namespace TowerDefence.Infrastructure
{
    public partial class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain loadingCurtainPrefab;

        private Game game;

        private void Awake()
        {
            game = new Game(this, Instantiate(loadingCurtainPrefab));

            game.gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}