using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class AttackEnemySB : ActiveSkillbitBase
{
  public int damage;
  [HideInInspector]
  public RuntimeAnimatorController defaultController;
  private bool lookAtTarget;

  private void OnEnable()
  {
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.anim = (Animator) this.playerObj.GetComponent<Animator>();
    this.defaultController = this.anim.get_runtimeAnimatorController();
    this.searchSBList = (SearchForObjectSB[]) this.playerObj.GetComponentsInChildren<SearchForObjectSB>();
  }

  public override void getEventTarget()
  {
    if (!Object.op_Inequality((Object) this.playerObj, (Object) null))
      return;
    SearchForObjectSB eventType = this.getEventType(SearchForObjectSB.SearchType.enemy);
    if (Object.op_Inequality((Object) eventType, (Object) null) && Object.op_Inequality((Object) eventType.target, (Object) null))
    {
      this.target = eventType.target;
    }
    else
    {
      if (PlayerSingleton.Instance.ManualMode || Timeline.instance.TimeLineRoomEmptyCheck() || ((IsAEntityType) Timeline.instance.FirstEvent().GetComponent<IsAEntityType>()).type != IsAEntityType.Type.enemy)
        return;
      this.target = Timeline.instance.FirstEvent();
    }
  }

  public bool CheckTargetRange(float range)
  {
    return Object.op_Inequality((Object) this.target, (Object) null) && Object.op_Inequality((Object) this.playerObj, (Object) null) && (double) Vector3.Distance(this.target.get_transform().get_position(), this.playerObj.get_transform().get_position()) >= (double) range;
  }

  public void moveToTarget()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null) || !Object.op_Inequality((Object) this.playerObj, (Object) null))
      return;
    this.anim.SetBool("attack", false);
    ((player) this.playerObj.GetComponent<player>()).DisableEnableHitbox();
    if (this.anim.GetBool("push"))
      return;
    ((AIPath) this.playerObj.GetComponent<PlayerPathingAI>()).canMove = true;
  }

  public bool CheckAttackRange(float range)
  {
    return Object.op_Inequality((Object) this.target, (Object) null) && Object.op_Inequality((Object) this.playerObj, (Object) null) && (double) Vector3.Distance(this.target.get_transform().get_position(), this.playerObj.get_transform().get_position()) <= (double) range;
  }

  public void attackTarget()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null) || !Object.op_Inequality((Object) this.playerObj, (Object) null))
      return;
    this.lookAtTarget = true;
    ((AIPath) this.playerObj.GetComponent<PlayerPathingAI>()).canMove = false;
    if ((!Object.op_Implicit((Object) this.target.GetComponentInChildren<EnemyAI>()) ? ((ChannelEnemy) this.target.GetComponentInChildren<ChannelEnemy>()).health : ((EnemyAI) this.target.GetComponentInChildren<EnemyAI>()).health) <= 0)
    {
      this.anim.SetBool("attack", false);
      this.lookAtTarget = false;
      this.consumeEventNoDestroy();
      this.StartCoroutine(this.Wait(1.2f));
    }
    else
      this.anim.SetBool("attack", true);
  }

  private void FaceTarget()
  {
    Vector3 vector3;
    ((Vector3) ref vector3).\u002Ector((float) this.target.get_transform().get_position().x, (float) this.playerObj.get_transform().get_position().y, (float) this.target.get_transform().get_position().z);
    this.playerObj.get_transform().LookAt(vector3);
  }

  [DebuggerHidden]
  private IEnumerator Wait(float waitTime)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AttackEnemySB.\u003CWait\u003Ec__Iterator15()
    {
      waitTime = waitTime,
      \u003C\u0024\u003EwaitTime = waitTime,
      \u003C\u003Ef__this = this
    };
  }

  private void Update()
  {
    if (!this.Active)
      return;
    if (this.searchSBList.Length == 0)
      this.searchSBList = (SearchForObjectSB[]) this.playerObj.GetComponentsInChildren<SearchForObjectSB>();
    if (Object.op_Equality((Object) this.target, (Object) null))
    {
      this.anim.SetBool("attack", false);
      this.getEventTarget();
    }
    else
    {
      if (Object.op_Inequality((Object) this.anim.get_runtimeAnimatorController(), (Object) this.defaultController))
        this.anim.set_runtimeAnimatorController(this.defaultController);
      if (this.lookAtTarget)
        this.FaceTarget();
      this.anim.SetFloat("AttackSpeedScale", TimeScale.Instance.attackSpeedScale);
    }
    if (!Object.op_Inequality((Object) ((playerBase) this.playerObj.GetComponent<player>()).target, (Object) this.target))
      return;
    this.lookAtTarget = false;
  }

  public override void UseSkillbit()
  {
  }
}
