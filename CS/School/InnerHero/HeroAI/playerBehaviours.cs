using BehaviourMachine;
using UnityEngine;

public class playerBehaviours : MonoBehaviour
{
  public playerBehaviours()
  {
    base.\u002Ector();
  }

  public abstract class PlayerActionNode : ActionNode
  {
    protected player pl;

    public override void Awake()
    {
      this.pl = (player) this.self.GetComponent<player>();
    }
  }

  [NodeInfo(category = "Player/Actions/")]
  public class UpdateActiveSkillbits : playerBehaviours.PlayerActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      this.pl.UpdateActiveSkillbitTrees();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Player/Actions/")]
  public class UpdatePassiveSkillbits : playerBehaviours.PlayerActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      this.pl.UpdatePassiveSkillbitTrees();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Player/Actions/")]
  public class MoveToEnd : playerBehaviours.PlayerActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      this.pl.MoveToEnd();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Player/Actions/")]
  public class MoveToRoomCenter : playerBehaviours.PlayerActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      this.pl.MoveToRoomCenter();
      return Status.Success;
    }
  }

  public abstract class PlayerConditionNode : ConditionNode
  {
    protected player pl;

    public override void Awake()
    {
      this.pl = (player) this.self.GetComponent<player>();
    }
  }

  [NodeInfo(category = "Player/Conditions/")]
  public class HasActiveSkillbits : playerBehaviours.PlayerConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      return this.pl.ActiveSkillBitTrees != null ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Player/Conditions/")]
  public class HasPassiveSkillbits : playerBehaviours.PlayerConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      return this.pl.PassiveSkillBitTrees != null ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Player/Conditions/")]
  public class HasNoTarget : playerBehaviours.PlayerConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      return Object.op_Equality((Object) this.pl.target, (Object) null) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Player/Conditions/")]
  public class ManualModeOff : playerBehaviours.PlayerConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      return !PlayerSingleton.Instance.ManualMode ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Player/Conditions/")]
  public class HasExploreRoomTarget : playerBehaviours.PlayerConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      return this.pl.checkExploreCenter() ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Player/Conditions/")]
  public class NoExploreRoomTarget : playerBehaviours.PlayerConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pl, (Object) null))
        return Status.Error;
      return !this.pl.checkExploreCenter() ? Status.Success : Status.Failure;
    }
  }
}
