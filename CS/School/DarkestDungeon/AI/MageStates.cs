using BehaviourMachine;
using UnityEngine;

public class MageStates : MonoBehaviour
{
  public MageStates()
  {
    base.\u002Ector();
  }

  public abstract class MageActionNode : ActionNode
  {
    protected MageAI mage;

    protected MageActionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.mage = (MageAI) this.get_self().GetComponent<MageAI>();
    }
  }

  [NodeInfo(category = "Mage/Actions/")]
  public class KeepDistance : MageStates.MageActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      this.mage.KeepDistanceFromPlayer();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Mage/Actions/")]
  public class AttackPlayer : MageStates.MageActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      this.mage.AttackPlayer();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Mage/Actions/")]
  public class Idle : MageStates.MageActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      this.mage.Idle();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Mage/Actions/")]
  public class Die : MageStates.MageActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      this.mage.Die();
      return (Status) 1;
    }
  }

  public abstract class MageConditionNode : ConditionNode
  {
    protected MageAI mage;

    protected MageConditionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.mage = (MageAI) ((ActionNode) this).get_self().GetComponent<MageAI>();
    }
  }

  [NodeInfo(category = "Mage/Conditions/")]
  public class InRangeOfPlayer : MageStates.MageConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      if (this.mage.InRangeOfPlayer(1200f))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Mage/Conditions/")]
  public class CloseToPlayer : MageStates.MageConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      if (this.mage.InRangeOfPlayer(600f))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Mage/Conditions/")]
  public class AttackRange : MageStates.MageConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      if (this.mage.InRangeOfPlayer(1000f))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Mage/Conditions/")]
  public class AttackCooldown : MageStates.MageConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      if ((double) this.mage.cooldown > 3.0)
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Mage/Conditions/")]
  public class isDead : MageStates.MageConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.mage, (Object) null))
        return (Status) 3;
      if ((double) this.mage.health <= 0.0)
        return (Status) 1;
      return (Status) 2;
    }
  }
}
