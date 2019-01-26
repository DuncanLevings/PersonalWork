using UnityEngine;

public class CriticalPassiveSB : PassiveSkillbitBase
{
  [Range(0.0f, 1f)]
  [Tooltip("lower value = higher chance ie. 0.35 = 75% chance")]
  public float procChance;
  public float CriticalDamageMultiplier;
  private float criticalDamage;
  private GameObject enemyTarget;
  private GameObject player;

  private void Start()
  {
    this.player = PlayerSingleton.Instance.getPlayer();
  }

  public override bool passiveSBChanceBool()
  {
    if ((double) Random.get_value() < (double) this.procChance)
      return false;
    this.criticalDamage = (float) ((isSwordHitbox) this.player.GetComponentInChildren<isSwordHitbox>()).damage * this.CriticalDamageMultiplier;
    this.enemyTarget = ((playerBase) this.player.GetComponent<global::player>()).target;
    if (Object.op_Inequality((Object) this.enemyTarget, (Object) null))
    {
      DamageFeedback.Instance.SpawnDamage(this.enemyTarget.get_transform().get_position(), Color.get_yellow(), (int) this.criticalDamage);
      if (Object.op_Implicit((Object) this.enemyTarget.GetComponentInChildren<EnemyAI>()))
      {
        M0 componentInChildren = this.enemyTarget.GetComponentInChildren<EnemyAI>();
        ((EnemyAI) componentInChildren).health = ((EnemyAI) componentInChildren).health - (int) this.criticalDamage;
      }
      ((Animator) this.enemyTarget.GetComponent<Animator>()).SetTrigger("hit");
      CameraShake.Instance.PlayShake(0.3f, 0.5f, false);
    }
    return true;
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
}
