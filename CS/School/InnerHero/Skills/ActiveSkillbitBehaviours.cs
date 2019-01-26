using BehaviourMachine;
using UnityEngine;

public class ActiveSkillbitBehaviours : MonoBehaviour
{
  public ActiveSkillbitBehaviours()
  {
    base.\u002Ector();
  }

  public abstract class ActiveSkillbitActionNode : ActionNode
  {
    protected ActiveSkillbitBase pas;

    public override void Awake()
    {
      this.pas = (ActiveSkillbitBase) this.self.GetComponent<ActiveSkillbitBase>();
    }
  }

  public abstract class ActiveSkillbitConditionNode : ConditionNode
  {
    protected ActiveSkillbitBase pas;

    public override void Awake()
    {
      this.pas = (ActiveSkillbitBase) this.self.GetComponent<ActiveSkillbitBase>();
    }
  }
}
