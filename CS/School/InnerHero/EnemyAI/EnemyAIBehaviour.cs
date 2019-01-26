using BehaviourMachine;
using UnityEngine;

public class EnemyAIBehaviour : MonoBehaviour
{
  public EnemyAIBehaviour()
  {
    base.\u002Ector();
  }

  public abstract class EnemyAIActionNode : ActionNode
  {
    protected EnemyAI en;

    public override void Awake()
    {
      this.en = (EnemyAI) this.self.GetComponent<EnemyAI>();
    }
  }

  [NodeInfo(category = "Enemy/Actions/")]
  public class MoveToTarget : EnemyAIBehaviour.EnemyAIActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      this.en.ChaseTarget();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Enemy/Actions/")]
  public class AttackTarget : EnemyAIBehaviour.EnemyAIActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      this.en.attackTarget();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Enemy/Actions/")]
  public class ReturnHome : EnemyAIBehaviour.EnemyAIActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      this.en.ReturnToHome();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Enemy/Actions/")]
  public class FindNonPlayerTarget : EnemyAIBehaviour.EnemyAIActionNode
  {
    public GameObject target;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      this.en.LookForTarget(this.target);
      return Status.Success;
    }
  }

  [NodeInfo(category = "Enemy/Actions/")]
  public class FleeTarget : EnemyAIBehaviour.EnemyAIActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      this.en.FleeTarget();
      return Status.Success;
    }
  }

  public abstract class EnemyAIConditionNode : ConditionNode
  {
    protected EnemyAI en;

    public override void Awake()
    {
      this.en = (EnemyAI) this.self.GetComponent<EnemyAI>();
    }
  }

  [NodeInfo(category = "Enemy/Conditions/")]
  public class hasTarget : EnemyAIBehaviour.EnemyAIConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      return Object.op_Inequality((Object) this.en.Target, (Object) null) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Enemy/Conditions/")]
  public class InChaseRange : EnemyAIBehaviour.EnemyAIConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      return this.en.CheckTargetRange(this.range) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Enemy/Conditions/")]
  public class FarFromSpawn : EnemyAIBehaviour.EnemyAIConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      return this.en.CheckHome() ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Enemy/Conditions/")]
  public class InChaseRangeAndFoV : EnemyAIBehaviour.EnemyAIConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      return this.en.CheckTargetRange(this.range) && this.en.LineOfSightCheck(this.en.Target.get_transform()) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Enemy/Conditions/")]
  public class AttackRange : EnemyAIBehaviour.EnemyAIConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      return this.en.CheckTargetRange(this.range) && this.en.health > 0 ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Enemy/Conditions/")]
  public class FleeRange : EnemyAIBehaviour.EnemyAIConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.en, (Object) null))
        return Status.Error;
      return this.en.CheckTargetRange(this.range) && this.en.health > 0 ? Status.Success : Status.Failure;
    }
  }
}
