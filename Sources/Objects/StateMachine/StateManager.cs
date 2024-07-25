using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Hallowed.Objects.StateMachine;

/// <summary>
/// The abstract class that manage the Object States. 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StateManager<T> where T : Enum
{
  protected readonly Dictionary<T, BaseState<T>> States = new();
  protected BaseState<T> CurrentState;

  protected bool IsTransitioningState = false;


  public void Initialize()
  {
    CurrentState.EnterState();
  }

  protected void AddState(T key, BaseState<T> stateObj)
  {
    States.Add(key, stateObj);
  }

  public void Update(GameTime delta)
  {
    var nextStateKey = CurrentState.GetNextState();
    if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
    {
      CurrentState.UpdateState(delta);
    }
    else if (!IsTransitioningState)
    {
      TransitionToState(nextStateKey);
    }
  }

  public void TransitionToState(T stateKey)
  {
    IsTransitioningState = true;
    CurrentState.ExitState();
    CurrentState = States[stateKey];
    CurrentState.EnterState();
    IsTransitioningState = false;
    Debug.WriteLine(" it is been called?");
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