using Hallowed.Core;
using Microsoft.Xna.Framework;

namespace Hallowed.Objects.Haley;

public class HaleyWalkingState : HaleyBaseState
{
  private int _speed;

  public HaleyWalkingState(HaleyStates key, ObjectHaley context) : base(key, context)
  {
    _speed = (int)context.MovementSpeed;
    NextState = key;
  }


  public override void UpdateState(GameTime delta)
  {
    if (!IsMoving())
    {
      NextState = HaleyStates.Idle;
      return;
    }

    UpdateDirection();
    UpdateMovement(delta);
  }

  private void UpdateDirection()
  {
    if (Context.Direction == Direction.Left)
    {
      Context.Sprite.Flip(false);
    }
    else if (Context.Direction == Direction.Right)
    {
      Context.Sprite.Flip(true);
    }
  }

  private void UpdateMovement(GameTime delta)
  {
    var input = Context.Input;
    float elapsed = (float)delta.ElapsedGameTime.TotalSeconds;
    if (input.IsPressed(PlayerInput.Left))
    {
      Context.X -= _speed * elapsed;
      Context.Direction = Direction.Left;
      Context.Sprite.Play("walk");
      return;
    }

    if (input.IsPressed(PlayerInput.Right))
    {
      Context.X += _speed * elapsed;
      Context.Direction = Direction.Right;
      Context.Sprite.Play("walk");
    }
  }

  private bool IsMoving()
  {
    var ct = Context.Input;
    return ct.IsPressed(PlayerInput.Left) || ct.IsPressed(PlayerInput.Right);
  }
}