using UnityEngine;

[RequireComponent(typeof (SlotSkillbitData))]
public class BerserkSB : ActiveSkillbitBase
{
  [Range(0.0f, 1f)]
  public float AttackSpeedMultiplier;
  private float amount;
  [Range(0.0f, 1f)]
  public float DefenseMultiplierLoss;
  public float Duration;
  private float timer;
  private float cooldown;
  private bool castedSpell;
  private bool buffOn;

  private void Start()
  {
    this.castedSpell = false;
    this.cooldown = this.UseCooldown;
    this.timer = this.Duration;
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.anim = (Animator) this.playerObj.GetComponent<Animator>();
    this.amount = TimeScale.Instance.attackSpeedScale * this.AttackSpeedMultiplier;
  }

  public override void UseSkillbit()
  {
    if (this.castedSpell)
      return;
    this.castedSpell = true;
    this.buffOn = true;
    CombatSystem.instance.defaultAttackSpeed += this.amount;
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
        CombatSystem.instance.defaultAttackSpeed -= this.amount;
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
