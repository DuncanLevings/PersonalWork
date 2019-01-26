using BehaviourMachine;
using UnityEngine;

public class BatStates : MonoBehaviour
{
  public BatStates()
  {
    base.\u002Ector();
  }

  public abstract class BatActionNode : ActionNode
  {
    protected BatAI bat;

    protected BatActionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.bat = (BatAI) this.get_self().GetComponent<BatAI>();
    }
  }

  [NodeInfo(category = "Bat/Actions/")]
  public class ChasePlayer : BatStates.BatActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bat, (Object) null))
        return (Status) 3;
      this.bat.ChasePlayer();
      this.bat.DropBomb();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Bat/Actions/")]
  public class IdleState : BatStates.BatActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bat, (Object) null))
        return (Status) 3;
      this.bat.BatIdle();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Bat/Actions/")]
  public class BatDie : BatStates.BatActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bat, (Object) null))
        return (Status) 3;
      this.bat.Die();
      return (Status) 1;
    }
  }

  public abstract class BatConditionNode : ConditionNode
  {
    protected BatAI bat;

    protected BatConditionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.bat = (BatAI) ((ActionNode) this).get_self().GetComponent<BatAI>();
    }
  }

  [NodeInfo(category = "Bat/Conditions/")]
  public class CloseToPlayer : BatStates.BatConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bat, (Object) null))
        return (Status) 3;
      if (this.bat.InRangeOfPlayer())
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Bat/Conditions/")]
  public class isBatDead : BatStates.BatConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bat, (Object) null))
        return (Status) 3;
      if ((double) this.bat.health <= 0.0)
        return (Status) 1;
      return (Status) 2;
    }
  }
}
