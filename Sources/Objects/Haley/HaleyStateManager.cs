using System.Diagnostics;
using Hallowed.Core.Objects;
using Hallowed.Objects.StateMachine;

namespace Hallowed.Objects.Haley;

public class HaleyStateManager : StateManager<HaleyStates>
{
  public HaleyStateManager(ObjectHaley context)
  {
    AddState(HaleyStates.Idle, new HaleyIdleState(HaleyStates.Idle, context));
    AddState(HaleyStates.Walk, new HaleyWalkingState(HaleyStates.Walk, context));
    CurrentState = States[HaleyStates.Idle];
  }
}