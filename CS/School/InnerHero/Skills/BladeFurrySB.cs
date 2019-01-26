using UnityEngine;

[RequireComponent(typeof (SlotSkillbitData))]
public class BladeFurrySB : ActiveSkillbitBase
{
  [Tooltip("% of damage from base damage to do with this attack")]
  public float DamageMultiplier;
  [HideInInspector]
  public int damage;
  public float spinSpeed;
  public GameObject[] daggers;
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
  }

  public override void UseSkillbit()
  {
    if (this.castedSpell)
      return;
    this.castedSpell = true;
    this.buffOn = true;
    this.damage = (int) Mathf.Clamp((float) ((isSwordHitbox) this.playerObj.GetComponentInChildren<isSwordHitbox>()).damage * this.DamageMultiplier, 1f, float.PositiveInfinity);
    this.timer = this.Duration;
    for (int index = 0; index < this.daggers.Length; ++index)
      this.daggers[index].SetActive(true);
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
      ((Component) this).get_transform().Rotate(Vector3.op_Multiply(Vector3.get_up(), this.spinSpeed * Time.get_deltaTime()));
      if ((double) this.timer <= 0.0)
      {
        this.timer = this.Duration;
        this.buffOn = false;
        for (int index = 0; index < this.daggers.Length; ++index)
          this.daggers[index].SetActive(false);
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
