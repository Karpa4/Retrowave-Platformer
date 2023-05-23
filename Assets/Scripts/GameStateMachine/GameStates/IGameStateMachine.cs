﻿using Features.GameStates.States.Interfaces;
using Features.Services;

namespace Features.GameStates
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        TState GetState<TState>() where TState : class, IExitableState;
        void Register<TState>(TState state) where TState : class, IExitableState;
    }
}