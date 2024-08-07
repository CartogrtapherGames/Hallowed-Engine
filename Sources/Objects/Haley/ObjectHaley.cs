﻿using Hallowed.Core;
using Hallowed.Data;
using Microsoft.Xna.Framework;

namespace Hallowed.Objects.Haley;

public enum PlayerInput
{
  Space,
  Left,
  Right
}

public class ObjectHaley : CharacterBase
{
  public float MovementSpeed = 300f;

  private readonly InputMap<PlayerInput> _inputMap;
  private readonly HaleyStateManager _stateMachine;

  public ObjectHaley(HaleyDataModel data, InputMap<PlayerInput> inputMap) : base(data)
  {
    Id = "Haley";
    _inputMap = inputMap;
    _stateMachine = new HaleyStateManager(this);
    _stateMachine.Initialize();
  }

  public void Move(float x, float y)
  {
    X = x;
    Y = y;
  }

  public override void Update(GameTime delta)
  {
    Sprite.Update(delta);
    _stateMachine.Update(delta);
  }

  public InputMap<PlayerInput> Input => _inputMap;

  // in this case we ensure that we can have an way to access haley stateManager 
  public HaleyStateManager StateMachine => _stateMachine;

  protected override Vector2 Origin { get; set; }
}