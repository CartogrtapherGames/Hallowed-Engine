using System.Diagnostics;
using Hallowed.Objects.Haley;
using Microsoft.Xna.Framework;

namespace Hallowed.Objects;

public class HaleyIdleState : HaleyBaseState
{
  public HaleyIdleState(HaleyStates key, ObjectHaley context) : base(key, context)
  {
    NextState = key;
  }

  public override void EnterState()
  {
    base.EnterState();
    Context.Sprite.Play("idle");
  }

  public override void UpdateState(GameTime delta)
  {
    if (Context.Input.IsPressed(PlayerInput.Left) || Context.Input.IsPressed(PlayerInput.Right))
    {
      Debug.WriteLine("it has been pressed!");
      NextState = HaleyStates.Walk;
      Debug.WriteLine(NextState.ToString());
    }
  }


  public bool IsAnyMovementInputPressed()
  {
    if (Context.Input.IsPressed(PlayerInput.Left))
    {
      return true;
    }

    if (Context.Input.IsPressed(PlayerInput.Right))
    {
      return true;
    }

    return false;
  }
}