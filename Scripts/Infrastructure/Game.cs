using TowerDefence.Infrastructure.Services;
using TowerDefence.Infrastructure.States;
using TowerDefence.Logic;

namespace TowerDefence.Infrastructure
{
    public class Game
    {
        public GameStateMachine gameStateMachine;

        public Game(ICoroutineRunner _coroutineRunner, LoadingCurtain _loadingCurtain)
        {
            gameStateMachine = new GameStateMachine(new SceneLoader(_coroutineRunner), _loadingCurtain, AllServices.container);
        }
    }
}