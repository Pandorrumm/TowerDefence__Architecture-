namespace TowerDefence.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private GameStateMachine gameStateMachine;

        public GameLoopState(GameStateMachine _gameStateMachine)
        {
            gameStateMachine = _gameStateMachine;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}