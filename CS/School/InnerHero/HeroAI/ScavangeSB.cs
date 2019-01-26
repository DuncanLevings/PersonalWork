using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ScavangeSB : ActiveSkillbitBase
{
  [HideInInspector]
  public int goldAmount;
  private Color powerBarColor;
  private Sprite powerBarIcon;
  private bool powerBarActive;

  private void OnEnable()
  {
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.anim = (Animator) this.playerObj.GetComponent<Animator>();
    this.powerBarColor = ((ActivateSelf) ((Component) this).GetComponent<ActivateSelf>()).BtnColor;
    this.powerBarIcon = ((ActivateSelf) ((Component) this).GetComponent<ActivateSelf>()).PowerBarIcon;
    this.goldAmount = 0;
  }

  public override void getEventTarget()
  {
    if (!Object.op_Inequality((Object) this.playerObj, (Object) null) || Timeline.instance.TimeLineRoomEmptyCheck() || ((IsAEntityType) Timeline.instance.FirstEvent().GetComponent<IsAEntityType>()).type != IsAEntityType.Type.crate)
      return;
    this.target = Timeline.instance.FirstEvent();
  }

  public bool CheckTargetRange(float range)
  {
    return Object.op_Inequality((Object) this.target, (Object) null) && Object.op_Inequality((Object) this.playerObj, (Object) null) && (double) Vector3.Distance(this.target.get_transform().get_position(), this.playerObj.get_transform().get_position()) >= (double) range;
  }

  public void moveToTarget()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null) || !Object.op_Inequality((Object) this.playerObj, (Object) null))
      return;
    if (this.powerBarActive)
      PowerBarInstance.Instance.EnablePowerBar(this.target, this.target, false, this.powerBarIcon, Color.get_white());
    this.powerBarActive = false;
    this.anim.SetBool("scavange", false);
    this.StartCoroutine(this.Wait(1f));
  }

  [DebuggerHidden]
  private IEnumerator Wait(float waitTime)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new ScavangeSB.\u003CWait\u003Ec__Iterator19()
    {
      waitTime = waitTime,
      \u003C\u0024\u003EwaitTime = waitTime,
      \u003C\u003Ef__this = this
    };
  }

  public bool CheckOpenRange(float range)
  {
    return Object.op_Inequality((Object) this.target, (Object) null) && Object.op_Inequality((Object) this.playerObj, (Object) null) && (double) Vector3.Distance(this.target.get_transform().get_position(), this.playerObj.get_transform().get_position()) <= (double) range;
  }

  public void ScavangeCrate()
  {
    ((AIPath) this.playerObj.GetComponent<PlayerPathingAI>()).endReachedDistance = 0.8f;
    this.anim.SetBool("scavange", true);
    if (this.powerBarActive)
      return;
    PowerBarInstance.Instance.EnablePowerBar(this.target, this.target, true, this.powerBarIcon, this.powerBarColor);
    ((Behaviour) this.target.GetComponent<PowerBarMultiTapCrate>()).set_enabled(true);
    this.powerBarActive = !this.powerBarActive;
  }

  public void ConsumeTarget()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null) || !Object.op_Inequality((Object) this.playerObj, (Object) null))
      return;
    this.powerBarActive = !this.powerBarActive;
    this.anim.SetBool("scavange", false);
    PowerBarInstance.Instance.EnablePowerBar(this.target, this.target, false, this.powerBarIcon, Color.get_white());
    ((Behaviour) this.target.GetComponent<PowerBarMultiTapCrate>()).set_enabled(false);
    this.consumeEvent();
    ((AIPath) this.playerObj.GetComponent<PlayerPathingAI>()).endReachedDistance = 0.2f;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.target, (Object) null))
    {
      this.getEventTarget();
      this.powerBarActive = false;
      if (this.anim.GetBool("scavange"))
        this.anim.SetBool("scavange", false);
    }
    else
      this.goldAmount = ((CrateData) this.target.GetComponent<CrateData>()).LootAmount;
    if (!Object.op_Equality((Object) this.playerObj, (Object) null))
      return;
    this.playerObj = PlayerSingleton.Instance.getPlayer();
  }

  public override void UseSkillbit()
  {
  }
}
