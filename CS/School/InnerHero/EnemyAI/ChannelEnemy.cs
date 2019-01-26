using UnityEngine;

public class ChannelEnemy : MonoBehaviour
{
  public float AttackSpeedBuffMultiplier;
  public float DamageBuffMultiplier;
  public int healAmount;
  public float TimeBetweenHeals;
  public int health;
  private float distanceToHover;
  private GameObject boss;
  private Animator anim;
  private float healthTimer;

  public ChannelEnemy()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.distanceToHover = Random.Range(1f, 1.5f);
    this.boss = ((Component) ((Component) ((Component) this).get_transform().get_root()).GetComponentInChildren<BossAI>()).get_gameObject();
    this.anim = (Animator) ((Component) this.boss.get_transform().get_parent()).GetComponent<Animator>();
    this.BuffTargetStats();
  }

  private void BuffTargetStats()
  {
    this.anim.SetFloat("AttackSpeed", this.anim.GetFloat("AttackSpeed") * this.AttackSpeedBuffMultiplier);
  }

  private void HealChannelTarget()
  {
    this.healthTimer += Time.get_deltaTime();
    if ((double) this.healthTimer <= (double) this.TimeBetweenHeals)
      return;
    ((EnemyAI) this.boss.GetComponent<EnemyAI>()).health = ((EnemyAI) this.boss.GetComponent<EnemyAI>()).health + this.healAmount;
    this.healthTimer = 0.0f;
  }

  private void Update()
  {
    if (Object.op_Inequality((Object) this.boss, (Object) null))
    {
      this.HealChannelTarget();
    }
    else
    {
      Object.Destroy((Object) ((Component) this).get_gameObject());
      Object.Destroy((Object) ((InitializeSwipeBox) ((Component) this).GetComponent<InitializeSwipeBox>()).go);
    }
    ((Component) this).get_transform().set_position(new Vector3((float) ((Component) this).get_transform().get_position().x, Mathf.PingPong(Time.get_time(), this.distanceToHover) + 1f, (float) ((Component) this).get_transform().get_position().z));
    if (this.health > 0)
      return;
    Object.Destroy((Object) ((Component) this).get_gameObject());
    Object.Destroy((Object) ((InitializeSwipeBox) ((Component) this).GetComponent<InitializeSwipeBox>()).go);
  }

  private void OnTriggerEnter(Collider col)
  {
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<isSwordHitbox>()))
    {
      int damage = ((isSwordHitbox) ((Component) col).GetComponent<isSwordHitbox>()).damage;
      ((isSwordHitbox) ((Component) col).GetComponent<isSwordHitbox>()).damage = ((isSwordHitbox) ((Component) col).GetComponent<isSwordHitbox>()).baseDamage;
      DamageFeedback.Instance.SpawnDamage(((Component) this).get_transform().get_position(), Color.get_white(), damage);
      this.health -= damage;
    }
    if (!Object.op_Implicit((Object) ((Component) col).GetComponent<Fireball>()))
      return;
    int damage1 = ((Fireball) ((Component) col).GetComponent<Fireball>()).damage;
    DamageFeedback.Instance.SpawnDamage(((Component) this).get_transform().get_position(), Color.get_white(), damage1);
    this.health -= damage1;
  }
}
