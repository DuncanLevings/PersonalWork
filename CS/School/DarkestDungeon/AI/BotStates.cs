using BehaviourMachine;
using UnityEngine;

public class BotStates : MonoBehaviour
{
  public BotStates()
  {
    base.\u002Ector();
  }

  public abstract class BotActionNode : ActionNode
  {
    protected BotAI bot;

    protected BotActionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.bot = (BotAI) this.get_self().GetComponent<BotAI>();
    }
  }

  [NodeInfo(category = "Bot/Actions/")]
  public class Idle : BotStates.BotActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      this.bot.Idle();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Bot/Actions/")]
  public class FollowPlayer : BotStates.BotActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      this.bot.FollowTarget(this.bot.player);
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Bot/Actions/")]
  public class AttackEnemy : BotStates.BotActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      this.bot.AttackTarget();
      return (Status) 1;
    }
  }

  [NodeInfo(category = "Bot/Actions/")]
  public class ChaseEnemy : BotStates.BotActionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      this.bot.FollowTarget(this.bot.enemyTarget);
      return (Status) 1;
    }
  }

  public abstract class BotConditionNode : ConditionNode
  {
    protected BotAI bot;

    protected BotConditionNode()
    {
      base.\u002Ector();
    }

    public virtual void Awake()
    {
      this.bot = (BotAI) ((ActionNode) this).get_self().GetComponent<BotAI>();
    }
  }

  [NodeInfo(category = "Bot/Conditions/")]
  public class InFollowRadius : BotStates.BotConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      if (this.bot.WithinFollowRadius())
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Bot/Conditions/")]
  public class NotInFollowRadius : BotStates.BotConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      if (!this.bot.WithinFollowRadius())
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Bot/Conditions/")]
  public class EnemyInSeekRadius : BotStates.BotConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      if (this.bot.FindEnemy(this.bot.seekRadius))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Bot/Conditions/")]
  public class BossInSeekRadius : BotStates.BotConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      if (this.bot.FindBoss(this.bot.seekRadius))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Bot/Conditions/")]
  public class EnemyNotInDestroyRadius : BotStates.BotConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      if (!this.bot.FindEnemy(this.bot.destroyRadius))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Bot/Conditions/")]
  public class OutsideDestroyRadius : BotStates.BotConditionNode
  {
    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      if (!this.bot.outsideDestroyRadius())
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Bot/Conditions/")]
  public class InAttackRange : BotStates.BotConditionNode
  {
    public float range;

    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      if (this.bot.InRangeOfTarget(this.range))
        return (Status) 1;
      return (Status) 2;
    }
  }

  [NodeInfo(category = "Bot/Conditions/")]
  public class NotInAttackRange : BotStates.BotConditionNode
  {
    public float range;

    public virtual Status Update()
    {
      if (Object.op_Equality((Object) this.bot, (Object) null))
        return (Status) 3;
      if (!this.bot.InRangeOfTarget(this.range))
        return (Status) 1;
      return (Status) 2;
    }
  }
}
