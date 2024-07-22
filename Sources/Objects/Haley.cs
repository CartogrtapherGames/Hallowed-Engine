using System.Diagnostics;
using Hallowed.Core;
using Hallowed.Data;
using Microsoft.Xna.Framework;

namespace Hallowed.Objects;

public enum PlayerInput
{
  Space,
  Left,
  Right
}

public class Haley : CharacterBase
{
  public float MovementSpeed = 300f;

  private InputMap<PlayerInput> _inputMap;
  private bool idling = true;

  public Haley(HaleyDataModel data, InputMap<PlayerInput> inputMap) : base(data)
  {
    _inputMap = inputMap;
  }

  public void Move(float x, float y)
  {
    X = x;
    Y = y;
  }

  public override void Update(GameTime delta)
  {
    Sprite.Update(delta);
    UpdateDirection();

    if (idling) Sprite.Play("idle");


    UpdateMovement(delta);
  }

  private void UpdateDirection()
  {
    if (Direction == Direction.Left)
    {
      Sprite.Flip(false);
    }
    else if (Direction == Direction.Right)
    {
      Sprite.Flip(true);
    }
  }

  private void UpdateMovement(GameTime delta)
  {
    float elapsed = (float)delta.ElapsedGameTime.TotalSeconds;
    if (_inputMap.IsPressed(PlayerInput.Left))
    {
      Sprite.X -= MovementSpeed * elapsed;
      Direction = Direction.Left;
      idling = false;
      Sprite.Play("walk");
    }

    if (_inputMap.IsPressed(PlayerInput.Right))
    {
      Sprite.X += MovementSpeed * elapsed;
      Direction = Direction.Right;
      idling = false;
      Sprite.Play("walk");
    }

    if (_inputMap.IsTriggered(PlayerInput.Space))
    {
      Sprite.Play("walk");
      idling = false;
    }
  }

  protected override Vector2 Origin { get; set; }
}