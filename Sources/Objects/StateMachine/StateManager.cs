using System;
using System.Collections.Generic;

namespace Hallowed.Objects.StateMachine;

// TODO : for now every 
public abstract class StateManager<T> where T : Enum
{
  protected Dictionary<T, BaseState<T>> States;
  protected BaseState<T> CurrentState;

  protected bool IsTransitioningState = false;

  public void Initialize()
  {
    CurrentState.EnterState();
  }

  public void Update()
  {
    var nextStateKey = CurrentState.GetNextState();
    if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
    {
      CurrentState.UpdateState();
    }
    else if (!IsTransitioningState)
    {
      TransitionToState(nextStateKey);
    }
  }

  public void TransitionToState(T stateKey)
  {
    CurrentState.ExitState();
    CurrentState = States[stateKey];
    CurrentState.EnterState();
  }

  // TODO : actually have implementations for colliders...
  public void OnTriggerEnter()
  {
  }

  public void OnTriggerStay()
  {
  }

  public void OnTriggerExit()
  {
  }
}