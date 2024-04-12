using System;
using System.Collections.Generic;
using MyGame.General.StateMachine.Interfaces;

namespace MyGame.General.StateMachine
{
    public abstract class StateMachine
    {
        protected Dictionary<Type, IExitableState> StatesMap;
        private IExitableState _currentState;
        
        protected void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        protected void Enter<TState, TLoad>(TLoad load) where TState : class, ILoadingState<TLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(load);
        }

        protected virtual TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        protected TState GetState<TState>() where TState : class, IExitableState => StatesMap[typeof(TState)] as TState;
    }
}