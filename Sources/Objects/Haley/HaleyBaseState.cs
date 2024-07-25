using Hallowed.Core.Objects;
using Hallowed.Objects.Haley;
using Hallowed.Objects.StateMachine;
using Microsoft.Xna.Framework;

namespace Hallowed.Objects;

public enum HaleyStates
{
  Idle,
  Walk
}

// here we implement haley base state which is a superState of every sub state haley can have.
public abstract class HaleyBaseState : BaseState<HaleyStates>
{
  protected ObjectHaley Context { get; }

  protected HaleyStates NextState;

  protected HaleyBaseState(HaleyStates key, ObjectHaley context) : base(key)
  {
    Context = context;
  }

  public override void EnterState()
  {
    NextState = StateKey;
  }

  public override void ExitState()
  {
    // here we can play some specific noise etc or even play particles
  }

  public override void UpdateState(GameTime delta)
  {
  }

  public override HaleyStates GetNextState()
  {
    return NextState;
  }

  public override void OnTriggerEnter()
  {
    // here I need to implement an collission system for this.
  }

  public override void OnTriggerStay()
  {
  }

  public override void OnTriggerExit()
  {
  }
}