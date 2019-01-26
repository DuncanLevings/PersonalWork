using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSystem : MonoBehaviour
{
  public static CombatSystem instance;
  public float staminaCost;
  private Vector3 fingerStart;
  private Vector3 fingerEnd;
  public float tolerance;
  [HideInInspector]
  public List<GameObject> enemys;
  public Text comboText;
  private Animator textAnim;
  [HideInInspector]
  public int comboCount;
  private bool comboHit;
  public float comboResetTime;
  private float comboTime;
  public int comboMultiplerInterval;
  private GameObject activeSkillbit;
  [HideInInspector]
  public float defaultAttackSpeed;
  public float recoverTime;
  private float origRecoverTime;
  private bool mustRecover;
  private slashEffect slashPanel;
  private player playerScript;
  private isSwordHitbox hitbox;
  public GameObject FeedbackIcon;

  public CombatSystem()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    CombatSystem.instance = this;
    this.origRecoverTime = this.recoverTime;
    if (Object.op_Equality((Object) this.slashPanel, (Object) null))
      this.slashPanel = (slashEffect) Object.FindObjectOfType<slashEffect>();
    if (Object.op_Implicit((Object) this.slashPanel))
      ((Component) this.slashPanel).get_gameObject().SetActive(false);
    if (Object.op_Inequality((Object) PlayerSingleton.Instance, (Object) null))
    {
      this.playerScript = (player) PlayerSingleton.Instance.getPlayer().GetComponent<player>();
      if (Object.op_Implicit((Object) Object.FindObjectOfType<AttackEnemySB>()))
        this.activeSkillbit = ((Component) Object.FindObjectOfType<AttackEnemySB>()).get_gameObject();
      this.hitbox = (isSwordHitbox) ((Component) this.playerScript).GetComponentInChildren<isSwordHitbox>();
    }
    this.textAnim = (Animator) ((Component) this.comboText).GetComponent<Animator>();
    this.tolerance = (float) Camera.get_main().get_pixelHeight() * 0.2f;
    Debug.Log((object) "aa");
    this.defaultAttackSpeed = TimeScale.Instance.attackSpeedScale;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.activeSkillbit, (Object) null) && Object.op_Implicit((Object) Object.FindObjectOfType<AttackEnemySB>()))
      this.activeSkillbit = ((Component) Object.FindObjectOfType<AttackEnemySB>()).get_gameObject();
    if (Input.GetMouseButtonDown(0))
    {
      this.fingerStart = Input.get_mousePosition();
      this.fingerEnd = Input.get_mousePosition();
    }
    if (!this.mustRecover && Input.GetMouseButton(0))
    {
      this.fingerEnd = Input.get_mousePosition();
      if ((double) Mathf.Abs((float) (this.fingerEnd.x - this.fingerStart.x)) > (double) this.tolerance || (double) Mathf.Abs((float) (this.fingerEnd.y - this.fingerStart.y)) > (double) this.tolerance)
      {
        if (this.enemys.Count > 0)
        {
          this.setTargetEnemy();
          this.Slash();
        }
        this.fingerStart = this.fingerEnd;
      }
    }
    if (Input.GetMouseButtonUp(0))
    {
      this.fingerStart = Vector3.get_zero();
      this.fingerEnd = Vector3.get_zero();
    }
    if (Object.op_Inequality((Object) this.comboText, (Object) null))
      this.comboText.set_text("COMBO: " + (object) this.comboCount);
    if (this.comboHit)
    {
      if ((double) this.comboTime == 0.0)
        this.comboTime = Time.get_time();
      else if ((double) Time.get_time() - (double) this.comboTime >= (double) this.comboResetTime)
      {
        this.comboCount = 0;
        this.comboTime = 0.0f;
        this.comboHit = false;
        if (Object.op_Implicit((Object) this.slashPanel))
          ((Component) this.slashPanel).get_gameObject().SetActive(false);
      }
    }
    if (this.comboCount > 1)
    {
      this.setMultiplerHero(this.comboCount);
    }
    else
    {
      TimeScale.Instance.attackSpeedScale = this.defaultAttackSpeed;
      if (Object.op_Inequality((Object) this.playerScript, (Object) null))
        this.hitbox.damage = this.hitbox.baseDamage;
    }
    if (Object.op_Inequality((Object) PlayerValues.Instance, (Object) null) && (double) PlayerValues.Instance.CurrentActionPoints <= 0.0)
    {
      this.comboCount = 0;
      this.comboTime = 0.0f;
      this.comboHit = false;
      this.mustRecover = true;
    }
    if (!this.mustRecover)
      return;
    if (Object.op_Inequality((Object) this.FeedbackIcon, (Object) null))
      IconFeedbackSystem.instance.InitializeIcon(this.FeedbackIcon, 3f);
    this.recoverTime -= Time.get_deltaTime();
    if ((double) this.recoverTime > 0.0)
      return;
    this.mustRecover = false;
    this.recoverTime = this.origRecoverTime;
  }

  private void Slash()
  {
    ((AudioSource) ((Component) this).GetComponent<AudioSource>()).Play();
    PlayerValues.Instance.CurrentActionPoints -= this.staminaCost;
    ++this.comboCount;
    this.textAnim.SetTrigger("combo");
    for (int index = 0; index < this.enemys.Count; ++index)
    {
      if (Object.op_Inequality((Object) this.slashPanel, (Object) null) && Object.op_Inequality((Object) this.enemys[index], (Object) null))
      {
        ((Component) this.slashPanel).get_gameObject().SetActive(false);
        this.slashPanel.FlashEffect(this.enemys[index].get_transform().get_position());
      }
      this.comboHit = true;
      this.comboTime = 0.0f;
    }
    this.enemys.Clear();
  }

  private void setMultiplerHero(int multiple)
  {
    if (multiple == 0)
      return;
    float num = Mathf.Clamp((float) ((double) this.defaultAttackSpeed * (double) multiple * 0.25), 0.0f, 5f);
    TimeScale.Instance.attackSpeedScale = this.defaultAttackSpeed + num;
  }

  private void setTargetEnemy()
  {
    for (int index = 0; index < this.enemys.Count; ++index)
    {
      if (Object.op_Equality((Object) this.enemys[index], (Object) null))
        this.enemys.RemoveAt(index);
    }
    if (this.enemys.Count <= 0 || !Object.op_Inequality((Object) this.playerScript.target, (Object) this.enemys[0]))
      return;
    ((ActivateSelf) this.activeSkillbit.GetComponent<ActivateSelf>()).ActivateTree(this.enemys[0]);
  }
}
