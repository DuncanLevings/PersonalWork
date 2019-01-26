using UnityEngine;

public class AllStatIncreasePassiveSB : PassiveSkillbitBase
{
  public float SpeedMulitplier;
  public float AttackSpeedMulitplier;
  public float HealthMulitplier;
  public float DefenseMulitplier;
  private Animator anim;

  private void Start()
  {
    if (!this.Active)
      return;
    float num1 = ((AIPath) PlayerSingleton.Instance.getPlayer().GetComponent<PlayerPathingAI>()).speed * this.SpeedMulitplier;
    M0 component = PlayerSingleton.Instance.getPlayer().GetComponent<PlayerPathingAI>();
    ((AIPath) component).speed = ((AIPath) component).speed + num1;
    this.anim = (Animator) PlayerSingleton.Instance.getPlayer().GetComponent<Animator>();
    float num2 = TimeScale.Instance.attackSpeedScale * this.AttackSpeedMulitplier;
    TimeScale.Instance.attackSpeedScale += num2;
    CombatSystem.instance.defaultAttackSpeed = TimeScale.Instance.attackSpeedScale;
    this.anim.SetFloat("AttackSpeedScale", TimeScale.Instance.attackSpeedScale);
    float num3 = PlayerValues.Instance.MaxHealth * this.HealthMulitplier;
    PlayerValues.Instance.MaxHealth += num3;
    PlayerValues.Instance.CurrentHealth = PlayerValues.Instance.MaxHealth;
  }

  private void Update()
  {
  }

  public override void GetEventTarget()
  {
  }

  public override void passiveSBChance()
  {
  }

  public override bool passiveSBChanceBool()
  {
    return false;
  }
}
