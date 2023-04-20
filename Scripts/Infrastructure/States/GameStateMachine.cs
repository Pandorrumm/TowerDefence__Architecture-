using System;
using System.Collections.Generic;
using TowerDefence.Infrastructure.Factory;
using TowerDefence.Infrastructure.Services;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using TowerDefence.Infrastructure.Services.SaveLoad;
using TowerDefence.Logic;

namespace TowerDefence.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> states;
        private IExitableState activeState;

        public GameStateMachine(SceneLoader _sceneLoader, LoadingCurtain _loadingCurtain, AllServices _allServices)
        {
            states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _sceneLoader, _allServices),
                [typeof(LoadLevelState)] = new LoadLevelState(this, _sceneLoader, _loadingCurtain, _allServices.Single<IGameFactory>(), _allServices.Single<IPersistentProgressService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, _allServices.Single<IPersistentProgressService>(), _allServices.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            activeState?.Exit();

            TState state = GetState<TState>();
            activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            states[typeof(TState)] as TState;
    }
}
