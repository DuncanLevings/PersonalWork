// Decompiled with JetBrains decompiler
// Type: AttackEnemyBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FF427592-9464-4031-9AF7-E8EEDF896718
// Assembly location: C:\Users\Duncan\Desktop\Assembly-CSharp.dll

using BehaviourMachine;
using UnityEngine;

public class AttackEnemyBehaviour : MonoBehaviour
{
  public AttackEnemyBehaviour()
  {
    base.\u002Ector();
  }

  public abstract class AttackEnemyActionNode : ActionNode
  {
    protected AttackEnemySB att;

    public override void Awake()
    {
      this.att = (AttackEnemySB) this.self.GetComponent<AttackEnemySB>();
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/AttackSkillbit/Actions/")]
  public class MoveToTarget : AttackEnemyBehaviour.AttackEnemyActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.att, (Object) null))
        return Status.Error;
      this.att.moveToTarget();
      return Status.Success;
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/AttackSkillbit/Actions/")]
  public class AttackTarget : AttackEnemyBehaviour.AttackEnemyActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.att, (Object) null))
        return Status.Error;
      this.att.attackTarget();
      return Status.Success;
    }
  }

  public abstract class AttackEnemyConditionNode : ConditionNode
  {
    protected AttackEnemySB att;
    protected GameObject player;

    public override void Awake()
    {
      this.att = (AttackEnemySB) this.self.GetComponent<AttackEnemySB>();
      this.player = PlayerSingleton.Instance.getPlayer();
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/AttackSkillbit/Conditions/")]
  public class hasTarget : AttackEnemyBehaviour.AttackEnemyConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.att, (Object) null))
        return Status.Error;
      return Object.op_Inequality((Object) this.att.target, (Object) null) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/AttackSkillbit/Conditions/")]
  public class OutOfRange : AttackEnemyBehaviour.AttackEnemyConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.att, (Object) null))
        return Status.Error;
      return this.att.CheckTargetRange(this.range) ? Status.Success : Status.Failure;
    }
  }

  [NodeInfo(category = "Skillbits/ActiveSkillbit/AttackSkillbit/Conditions/")]
  public class AttackRange : AttackEnemyBehaviour.AttackEnemyConditionNode
  {
    public float range;

    public override Status Update()
    {
      if (Object.op_Equality((Object) this.att, (Object) null))
        return Status.Error;
      return this.att.CheckAttackRange(this.range) ? Status.Success : Status.Failure;
    }
  }
}
