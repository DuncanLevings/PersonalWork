using BehaviourMachine;
using UnityEngine;

public class BossStates : MonoBehaviour
{
  public BossStates()
  {
    base.\u002Ector();
  }

  public abstract class BossActionNode : ActionNode
  {
    protected BossAI Boss;

    protected BossActionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.Boss = (BossAI) this.get_self().GetComponent<BossAI>();
    }
  }

  [NodeInfo(category = "Boss/Actions/")]
  public class Die : BossStates.BossActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      this.Boss.Die();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Boss/Actions/")]
  public class idle : BossStates.BossActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      this.Boss.Idle();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Boss/Actions/")]
  public class RailGunfire : BossStates.BossActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      this.Boss.RailGunAttack();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Boss/Actions/")]
  public class ArtilaryFire : BossStates.BossActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      this.Boss.LobbedAttack();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Boss/Actions/")]
  public class Charge : BossStates.BossActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      this.Boss.Charge();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Boss/Actions/")]
  public class Transform : BossStates.BossActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      this.Boss.Transform();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Boss/Actions/")]
  public class Grenade : BossStates.BossActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      this.Boss.Grenade();
      return (Status) 1;
    }
  }

  public abstract class BossConditionNode : ConditionNode
  {
    protected BossAI Boss;

    protected BossConditionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.Boss = (BossAI) ((ActionNode) this).get_self().GetComponent<BossAI>();
    }
  }

  [NodeInfo(category = "Boss/Conditions/")]
  public class isDead : BossStates.BossConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      if ((double) this.Boss.health <= 0.0)
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Boss/Conditions/")]
  public class healthTransform : BossStates.BossConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      if ((double) this.Boss.health <= (double) this.Boss.maxHealth / 2.0 && !this.Boss.secondStage)
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Boss/Conditions/")]
  public class canFireRail : BossStates.BossConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      if ((double) this.Boss.railgunCD > 3.0)
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Boss/Conditions/")]
  public class canArtilary : BossStates.BossConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      if ((double) this.Boss.artilaryCD > 4.5)
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Boss/Conditions/")]
  public class canGrenade : BossStates.BossConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      if ((double) this.Boss.grnadeCD > 5.8 && this.Boss.secondStage)
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Boss/Conditions/")]
  public class canCharge : BossStates.BossConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      if (this.Boss.canCharge())
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Boss/Conditions/")]
  public class PlayerClose : BossStates.BossConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.Boss, (Object) null))
        return (Status) 3;
      if (this.Boss.playerWithinRadius(1200f))
        return (Status) 1;
      return (Status) 2;
    }
  }
}
