using UnityEngine;

public class BladeSpinProcPassiveSB : PassiveSkillbitBase
{
  [Tooltip("lower value = higher chance ie. 0.35 = 75% chance")]
  [Range(0.0f, 1f)]
  public float procChance;
  [Tooltip("% of damage from base damage to do with this attack")]
  public float DamageMultiplier;
  public float spinSpeed;
  public float duration;
  public GameObject[] daggers;
  private float timer;
  [HideInInspector]
  public int damage;
  private GameObject player;
  private bool spinOn;

  private void Start()
  {
    this.player = PlayerSingleton.Instance.getPlayer();
    this.timer = this.duration;
  }

  public override void passiveSBChance()
  {
    if ((double) Random.get_value() < (double) this.procChance)
      return;
    this.damage = (int) Mathf.Clamp((float) ((isSwordHitbox) this.player.GetComponentInChildren<isSwordHitbox>()).damage * this.DamageMultiplier, 1f, float.PositiveInfinity);
    this.spinOn = true;
    this.timer = this.duration;
    for (int index = 0; index < this.daggers.Length; ++index)
      this.daggers[index].SetActive(true);
  }

  private void Update()
  {
    if (!this.Active || !this.spinOn)
      return;
    this.timer -= Time.get_deltaTime();
    ((Component) this).get_transform().Rotate(Vector3.op_Multiply(Vector3.get_up(), this.spinSpeed * Time.get_deltaTime()));
    if ((double) this.timer > 0.0)
      return;
    this.timer = this.duration;
    this.spinOn = false;
    for (int index = 0; index < this.daggers.Length; ++index)
      this.daggers[index].SetActive(false);
  }

  public override void GetEventTarget()
  {
  }

  public override bool passiveSBChanceBool()
  {
    return false;
  }
}
