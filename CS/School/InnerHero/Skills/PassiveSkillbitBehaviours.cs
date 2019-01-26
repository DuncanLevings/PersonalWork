using BehaviourMachine;
using UnityEngine;

public class PassiveSkillbitBehaviours : MonoBehaviour
{
  public PassiveSkillbitBehaviours()
  {
    base.\u002Ector();
  }

  public abstract class PassiveSkillbitActionNode : ActionNode
  {
    protected PassiveSkillbitBase pas;

    public override void Awake()
    {
      this.pas = (PassiveSkillbitBase) this.self.GetComponent<PassiveSkillbitBase>();
    }
  }

  [NodeInfo(category = "Skillbits/PassiveSkillbit/Actions/")]
  public class PopulateEvents : PassiveSkillbitBehaviours.PassiveSkillbitActionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pas, (Object) null))
        return Status.Error;
      this.pas.GetEventTarget();
      return Status.Success;
    }
  }

  public abstract class PassiveSkillbitConditionNode : ConditionNode
  {
    protected PassiveSkillbitBase pas;

    public override void Awake()
    {
      this.pas = (PassiveSkillbitBase) this.self.GetComponent<PassiveSkillbitBase>();
    }
  }

  [NodeInfo(category = "Skillbits/PassiveSkillbit/Conditions/")]
  public class isEventListEmpty : PassiveSkillbitBehaviours.PassiveSkillbitConditionNode
  {
    public override Status Update()
    {
      if (Object.op_Equality((Object) this.pas, (Object) null))
        return Status.Error;
      return Object.op_Equality((Object) this.pas.target, (Object) null) ? Status.Success : Status.Failure;
    }
  }
}
