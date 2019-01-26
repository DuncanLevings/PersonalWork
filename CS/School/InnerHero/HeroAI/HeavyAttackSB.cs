using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class HeavyAttackSB : ActiveSkillbitBase
{
  public int damage;
  public AnimatorOverrideController overrideController;
  private bool lookAtTarget;

  private void OnEnable()
  {
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.anim = (Animator) this.playerObj.GetComponent<Animator>();
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
    if (((EnemyAI) this.target.GetComponentInChildren<EnemyAI>()).health <= 0)
    {
      this.lookAtTarget = false;
      this.anim.SetBool("attack", false);
      ((playerBase) this.playerObj.GetComponent<player>()).enableOnlyDefaultSB();
      this.consumeEventNoDestroy();
      this.StartCoroutine(this.Wait(1.2f));
    }
    else
    {
      this.anim.SetBool("attack", true);
      AnimatorStateInfo animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
      if (!((AnimatorStateInfo) ref animatorStateInfo).IsName("attack") || ((isSwordHitbox) this.playerObj.GetComponentInChildren<isSwordHitbox>()).baseDamage == this.damage)
        return;
      ((isSwordHitbox) this.playerObj.GetComponentInChildren<isSwordHitbox>()).baseDamage = this.damage;
      ((isSwordHitbox) this.playerObj.GetComponentInChildren<isSwordHitbox>()).damage = this.damage;
    }
  }

  private void FaceTarget()
  {
    Vector3 vector3 = Vector3.RotateTowards(this.playerObj.get_transform().get_forward(), Vector3.op_Subtraction(this.target.get_transform().get_position(), this.playerObj.get_transform().get_position()), 360f * Time.get_deltaTime(), 0.0f);
    Debug.DrawRay(((Component) this).get_transform().get_position(), vector3, Color.get_red());
    this.playerObj.get_transform().set_rotation(Quaternion.LookRotation(vector3));
  }

  [DebuggerHidden]
  private IEnumerator Wait(float waitTime)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new HeavyAttackSB.\u003CWait\u003Ec__Iterator16()
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
    if (Object.op_Equality((Object) this.target, (Object) null))
    {
      this.anim.SetBool("attack", false);
      this.getEventTarget();
    }
    else
    {
      if (Object.op_Inequality((Object) this.anim.get_runtimeAnimatorController(), (Object) this.overrideController))
        this.anim.set_runtimeAnimatorController((RuntimeAnimatorController) this.overrideController);
      if (this.lookAtTarget)
        this.FaceTarget();
    }
    if (!Object.op_Inequality((Object) ((playerBase) this.playerObj.GetComponent<player>()).target, (Object) this.target))
      return;
    this.lookAtTarget = false;
  }

  public override void UseSkillbit()
  {
  }
}
