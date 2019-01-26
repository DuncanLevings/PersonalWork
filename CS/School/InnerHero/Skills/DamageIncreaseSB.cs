using UnityEngine;

[RequireComponent(typeof (SlotSkillbitData))]
public class DamageIncreaseSB : ActiveSkillbitBase
{
  public float DamageMultiplier;
  public float Duration;
  private float timer;
  private float cooldown;
  private bool castedSpell;
  private bool buffOn;
  private float amount;

  private void Start()
  {
    this.castedSpell = false;
    this.cooldown = this.UseCooldown;
    this.timer = this.Duration;
    ((SlotSkillbitData) ((Component) this).GetComponent<SlotSkillbitData>()).cooldown = this.UseCooldown;
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.anim = (Animator) this.playerObj.GetComponent<Animator>();
  }

  public override void UseSkillbit()
  {
    if (this.castedSpell)
      return;
    this.castedSpell = true;
    this.buffOn = true;
    this.amount = (float) ((isSwordHitbox) this.playerObj.GetComponentInChildren<isSwordHitbox>()).baseDamage * this.DamageMultiplier;
    M0 componentInChildren = this.playerObj.GetComponentInChildren<isSwordHitbox>();
    ((isSwordHitbox) componentInChildren).baseDamage = ((isSwordHitbox) componentInChildren).baseDamage + (int) this.amount;
    ((isSwordHitbox) this.playerObj.GetComponentInChildren<isSwordHitbox>()).damage = ((isSwordHitbox) this.playerObj.GetComponentInChildren<isSwordHitbox>()).baseDamage;
  }

  private void Update()
  {
    ((SlotSkillbitData) ((Component) this).GetComponent<SlotSkillbitData>()).cooldown = this.UseCooldown;
    ((SlotSkillbitData) ((Component) this).GetComponent<SlotSkillbitData>()).castedSpell = this.castedSpell;
    if (!this.castedSpell)
      return;
    if (this.buffOn)
    {
      this.timer -= Time.get_deltaTime();
      if ((double) this.timer <= 0.0)
      {
        M0 componentInChildren = this.playerObj.GetComponentInChildren<isSwordHitbox>();
        ((isSwordHitbox) componentInChildren).baseDamage = ((isSwordHitbox) componentInChildren).baseDamage - (int) this.amount;
        this.timer = this.Duration;
        this.buffOn = false;
      }
    }
    this.UseCooldown -= Time.get_deltaTime();
    if ((double) this.UseCooldown > 0.0)
      return;
    this.castedSpell = false;
    this.UseCooldown = this.cooldown;
  }

  public override void getEventTarget()
  {
  }
}
