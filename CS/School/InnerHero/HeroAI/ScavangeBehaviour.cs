using BehaviourMachine;
using UnityEngine;

public class ScavangeBehaviour : MonoBehaviour
{
  public ScavangeBehaviour()
  {
    base.\u002Ector();
  }

  public abstract class ScavangeActionNode : ActionNode
  {
    protected ScavangeSB scav;

    public override void Awake()
    {
      this.scav = (ScavangeSB) this.self.GetComponent<ScavangeSB>();
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/ScavangeSkillbit/Actions/")]
  public class MoveToTarget : ScavangeBehaviour.ScavangeActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.scav, (Object) null))
        return Status.Error;
      this.scav.moveToTarget();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/ScavangeSkillbit/Actions/")]
  public class Scavange : ScavangeBehaviour.ScavangeActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.scav, (Object) null))
        return Status.Error;
      this.scav.ScavangeCrate();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/ScavangeSkillbit/Actions/")]
  public class ConsumeCrate : ScavangeBehaviour.ScavangeActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.scav, (Object) null))
        return Status.Error;
      this.scav.ConsumeTarget();
      return Status.Success;
    }
  }

  public abstract class ScavangeConditionNode : ConditionNode
  {
    protected ScavangeSB scav;
    protected GameObject player;

    public override void Awake()
    {
      this.scav = (ScavangeSB) this.self.GetComponent<ScavangeSB>();
      this.player = PlayerSingleton.Instance.getPlayer();
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/ScavangeSkillbit/Conditions/")]
  public class hasTarget : ScavangeBehaviour.ScavangeConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.scav, (Object) null))
        return Status.Error;
      return Object.op_Inequality((Object) this.scav.target, (Object) null) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/ScavangeSkillbit/Conditions/")]
  public class OutOfRange : ScavangeBehaviour.ScavangeConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.scav, (Object) null))
        return Status.Error;
      return this.scav.CheckTargetRange(this.range) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/ScavangeSkillbit/Conditions/")]
  public class ScavangeRange : ScavangeBehaviour.ScavangeConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.scav, (Object) null))
        return Status.Error;
      return this.scav.CheckOpenRange(this.range) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/ScavangeSkillbit/Conditions/")]
  public class CrateEmpty : ScavangeBehaviour.ScavangeConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.scav, (Object) null))
        return Status.Error;
      return this.scav.goldAmount <= 0 ? Status.Success : Status.Failure;
    }
  }
}
