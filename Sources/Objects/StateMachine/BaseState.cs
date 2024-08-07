﻿using System;
using Hallowed.Core.Objects;
using Microsoft.Xna.Framework;

namespace Hallowed.Objects.StateMachine;

public abstract class BaseState<T> where T : Enum
{
  public T StateKey { get; private set; }

  public BaseState(T key)
  {
    StateKey = key;
  }

  public abstract void EnterState();
  public abstract void ExitState();
  public abstract void UpdateState(GameTime delta);
  public abstract T GetNextState();
  public abstract void OnTriggerEnter();
  public abstract void OnTriggerStay();
  public abstract void OnTriggerExit();
}