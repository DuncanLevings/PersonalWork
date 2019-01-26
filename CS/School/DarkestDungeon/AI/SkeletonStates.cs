using BehaviourMachine;
using UnityEngine;

public class SkeletonStates : MonoBehaviour
{
  public SkeletonStates()
  {
    base.\u002Ector();
  }

  public abstract class SkeletonActionNode : ActionNode
  {
    protected SkeletonAI skele;

    protected SkeletonActionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.skele = (SkeletonAI) this.get_self().GetComponent<SkeletonAI>();
    }
  }

  [NodeInfo(category = "Skeleton/Actions/")]
  public class ChasePlayer : SkeletonStates.SkeletonActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.skele, (Object) null))
        return (Status) 3;
      this.skele.ChasePlayer();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Skeleton/Actions/")]
  public class AttackPlayer : SkeletonStates.SkeletonActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.skele, (Object) null))
        return (Status) 3;
      this.skele.AttackPlayer();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Skeleton/Actions/")]
  public class SkeletonIdle : SkeletonStates.SkeletonActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.skele, (Object) null))
        return (Status) 3;
      this.skele.SkeleIdle();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Skeleton/Actions/")]
  public class SkeletonDie : SkeletonStates.SkeletonActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.skele, (Object) null))
        return (Status) 3;
      this.skele.Die();
      return (Status) 1;
    }
  }

  public abstract class SkeletonConditionNode : ConditionNode
  {
    protected SkeletonAI skele;

    protected SkeletonConditionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.skele = (SkeletonAI) ((ActionNode) this).get_self().GetComponent<SkeletonAI>();
    }
  }

  [NodeInfo(category = "Skeleton/Conditions/")]
  public class CloseToPlayer : SkeletonStates.SkeletonConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.skele, (Object) null))
        return (Status) 3;
      if (this.skele.InRangeOfPlayer(1000f))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Skeleton/Conditions/")]
  public class AttackRange : SkeletonStates.SkeletonConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.skele, (Object) null))
        return (Status) 3;
      if (this.skele.InRangeOfPlayer(100f))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Skeleton/Conditions/")]
  public class isSkeletonDead : SkeletonStates.SkeletonConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.skele, (Object) null))
        return (Status) 3;
      if ((double) this.skele.health <= 0.0)
        return (Status) 1;
      return (Status) 2;
    }
  }
}
