using UnityEngine;

public class AttackSpeed2PassiveSB : PassiveSkillbitBase
{
  [Range(0.0f, 1f)]
  [Tooltip("lower value = higher chance ie. 0.35 = 75% chance")]
  public float procChance;
  [Tooltip("Incremental amount to add attack speed")]
  public float AttackSpeedAmount;
  public float MaxStackAmount;
  public float DurationToReset;
  private float timer;
  private float currentStackAmount;
  private bool recentProc;

  private void Start()
  {
    this.currentStackAmount = 0.0f;
    this.timer = this.DurationToReset;
  }

  public override void passiveSBChance()
  {
    if ((double) this.currentStackAmount >= (double) this.MaxStackAmount || (double) Random.get_value() < (double) this.procChance)
      return;
    float num = CombatSystem.instance.defaultAttackSpeed + this.AttackSpeedAmount;
    CombatSystem.instance.defaultAttackSpeed = num;
    ++this.currentStackAmount;
    this.recentProc = true;
    this.timer = this.DurationToReset;
  }

  private void Update()
  {
    if (!this.Active || !this.recentProc)
      return;
    this.timer -= Time.get_deltaTime();
    if ((double) this.timer > 0.0)
      return;
    CombatSystem.instance.defaultAttackSpeed -= this.currentStackAmount * this.AttackSpeedAmount;
    this.currentStackAmount = 0.0f;
    this.timer = this.DurationToReset;
    this.recentProc = false;
  }

  public override void GetEventTarget()
  {
  }

  public override bool passiveSBChanceBool()
  {
    return false;
  }
}
