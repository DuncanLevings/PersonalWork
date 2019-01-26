using UnityEngine;

public class EnemyAI : MonoBehaviour
{
  public float SearchRadius;
  private GameObject playerObj;
  public GameObject Target;
  public GameObject pathingTarget;
  public int health;
  private EnemyPathingAI enemyPath;
  private InitializeSwipeBox swipeBox;
  private EnemyHitboxes hitbox;
  [HideInInspector]
  public bool recentFlee;
  [HideInInspector]
  public int maxHealth;
  private GameObject enemyRoom;
  private Animator anim;
  private Vector3 spawnPosition;
  private Vector3 fleePos;
  private bool MustReturnHome;
  private bool giveLoot;
  private bool dead;
  private bool lookAtTarget;
  private isaEnemyHitProc[] hitProcs;

  public EnemyAI()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.anim = (Animator) ((Component) ((Component) this).get_transform().get_parent()).GetComponent<Animator>();
    this.spawnPosition = this.pathingTarget.get_transform().get_position();
    this.enemyRoom = ((Component) ((Component) ((Component) ((Component) this).get_transform().get_parent()).get_transform().get_parent()).get_transform().get_parent()).get_gameObject();
    this.maxHealth = this.health;
    this.enemyPath = (EnemyPathingAI) ((Component) ((Component) this).get_transform().get_parent()).GetComponent<EnemyPathingAI>();
    this.swipeBox = (InitializeSwipeBox) ((Component) ((Component) this).get_transform().get_parent()).GetComponent<InitializeSwipeBox>();
    this.hitbox = (EnemyHitboxes) ((Component) ((Component) this).get_transform().get_parent()).GetComponent<EnemyHitboxes>();
    this.SetHitProcs();
  }

  public void SetHitProcs()
  {
    this.hitProcs = (isaEnemyHitProc[]) this.playerObj.GetComponentsInChildren<isaEnemyHitProc>();
  }

  private void OnTriggerEnter(Collider col)
  {
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<isSwordHitbox>()))
    {
      for (int index = 0; index < this.hitProcs.Length; ++index)
      {
        if (((Component) this.hitProcs[index]).get_gameObject().get_activeSelf())
        {
          ((PassiveSkillbitBase) ((Component) this.hitProcs[index]).GetComponent<PassiveSkillbitBase>()).passiveSBChance();
          if (((PassiveSkillbitBase) ((Component) this.hitProcs[index]).GetComponent<PassiveSkillbitBase>()).passiveSBChanceBool())
            return;
        }
      }
      int damage = ((isSwordHitbox) ((Component) col).GetComponent<isSwordHitbox>()).damage;
      ((isSwordHitbox) ((Component) col).GetComponent<isSwordHitbox>()).damage = ((isSwordHitbox) ((Component) col).GetComponent<isSwordHitbox>()).baseDamage;
      DamageFeedback.Instance.SpawnDamage(((Component) this).get_transform().get_position(), Color.get_white(), damage);
      this.health -= damage;
      this.anim.SetTrigger("hit");
    }
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<isLightNovaHitbox>()))
      this.showDamage(((LightNovaPassiveSB) ((Component) ((Component) col).get_transform().get_parent()).GetComponent<LightNovaPassiveSB>()).damage);
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<isBladeFlurryHitbox>()))
      this.showDamage(((BladeSpinProcPassiveSB) ((Component) ((Component) col).get_transform().get_parent()).GetComponent<BladeSpinProcPassiveSB>()).damage);
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<isActiveBladeFlurryHitbox>()))
      this.showDamage(((BladeFurrySB) ((Component) ((Component) col).get_transform().get_parent()).GetComponent<BladeFurrySB>()).damage);
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<Fireball>()))
      this.showDamage(((Fireball) ((Component) col).GetComponent<Fireball>()).damage);
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<Explosion>()))
      this.showDamage(((Explosion) ((Component) col).GetComponent<Explosion>()).damage);
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<Consume>()))
    {
      int damage = ((Consume) ((Component) col).GetComponent<Consume>()).damage;
      this.showDamage(damage);
      DamageFeedback.Instance.SpawnPlayerHit(PlayerSingleton.Instance.getPlayer().get_transform().get_position(), Color.get_green(), "+" + damage.ToString());
      PlayerValues.Instance.CurrentHealth += (float) damage;
    }
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<FireNova>()))
    {
      this.hitbox.DisableEnableHitbox();
      this.anim.SetTrigger("stunned");
      int damage = ((FireNova) ((Component) col).GetComponent<FireNova>()).damage;
      DamageFeedback.Instance.SpawnDamage(((Component) this).get_transform().get_position(), Color.get_white(), damage);
      this.health -= damage;
    }
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<SlowNova>()))
    {
      this.hitbox.DisableEnableHitbox();
      this.enemyPath.EffectBySpell = true;
      this.enemyPath.UniqueTimeScale = ((SlowNova) ((Component) col).GetComponent<SlowNova>()).SlowAmount;
      this.enemyPath.spellEffectDuration = ((SlowNova) ((Component) col).GetComponent<SlowNova>()).SlowDuration;
    }
    if (!Object.op_Implicit((Object) ((Component) col).GetComponent<IceMine>()))
      return;
    this.hitbox.DisableEnableHitbox();
    this.enemyPath.EffectBySpell = true;
    this.enemyPath.UniqueTimeScale = ((IceMine) ((Component) col).GetComponent<IceMine>()).SlowAmount;
    this.enemyPath.spellEffectDuration = ((IceMine) ((Component) col).GetComponent<IceMine>()).SlowDuration;
  }

  private void showDamage(int damage)
  {
    DamageFeedback.Instance.SpawnDamage(((Component) this).get_transform().get_position(), Color.get_white(), damage);
    this.health -= damage;
    this.anim.SetTrigger("hit");
  }

  private void LookForPlayer(float radius)
  {
    if (!Object.op_Equality((Object) ((playerBase) this.playerObj.GetComponent<player>()).CurrentRoom, (Object) this.enemyRoom))
      return;
    this.Target = this.playerObj;
    ((Behaviour) this.swipeBox).set_enabled(true);
  }

  public void LookForTarget(GameObject target)
  {
    if (!Object.op_Equality((Object) target, (Object) this.enemyRoom))
      return;
    this.Target = target;
  }

  public void ReturnToHome()
  {
    this.pathingTarget.get_transform().set_position(this.spawnPosition);
    this.anim.SetBool("attack", false);
    this.enemyPath.canMove = true;
    this.Target = this.pathingTarget;
  }

  public bool CheckHome()
  {
    if (!Object.op_Equality((Object) this.Target, (Object) this.playerObj) || !Object.op_Inequality((Object) ((playerBase) this.Target.GetComponent<player>()).CurrentRoom, (Object) this.enemyRoom))
      return false;
    this.MustReturnHome = true;
    return true;
  }

  public bool CheckTargetRange(float range)
  {
    return Object.op_Inequality((Object) this.Target, (Object) null) && (double) Vector3.Distance(this.Target.get_transform().get_position(), ((Component) this).get_transform().get_position()) <= (double) range;
  }

  public bool LineOfSightCheck(Transform target)
  {
    return false;
  }

  public void ChaseTarget()
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null) || !Object.op_Equality((Object) this.Target, (Object) this.playerObj) || !Object.op_Equality((Object) ((playerBase) this.Target.GetComponent<player>()).CurrentRoom, (Object) this.enemyRoom))
      return;
    this.pathingTarget.get_transform().set_position(this.Target.get_transform().get_position());
    this.anim.SetBool("attack", false);
    AnimatorStateInfo animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo).IsName("Movement"))
    {
      ((Behaviour) this.enemyPath).set_enabled(true);
      this.enemyPath.canMove = true;
    }
    if (!Object.op_Inequality((Object) this.swipeBox.go, (Object) null))
      return;
    this.swipeBox.go.SetActive(true);
  }

  public void FleeTarget()
  {
    if (!Object.op_Equality((Object) this.Target, (Object) this.playerObj))
      return;
    if (!((Behaviour) this.enemyPath).get_enabled())
      ((Behaviour) this.enemyPath).set_enabled(true);
    if (!this.recentFlee)
      this.fleePos = this.findFurthestPoint();
    if (Vector3.op_Inequality(this.fleePos, Vector3.get_zero()))
    {
      this.recentFlee = true;
      this.pathingTarget.get_transform().set_position(this.fleePos);
      Debug.DrawLine(((Component) this).get_transform().get_position(), this.fleePos, Color.get_green());
    }
    if (!this.anim.GetBool("attack") || !this.AttackAnimCheck())
      return;
    this.anim.SetBool("attack", false);
    this.lookAtTarget = false;
    this.enemyPath.canMove = true;
  }

  private bool AttackAnimCheck()
  {
    AnimatorStateInfo animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
    return (double) ((AnimatorStateInfo) ref animatorStateInfo).get_normalizedTime() % 1.0 > 0.899999976158142;
  }

  private Vector3 findFurthestPoint()
  {
    Vector3 vector3_1 = Vector3.get_zero();
    Bounds bounds = ((Collider) this.enemyRoom.GetComponent<BoxCollider>()).get_bounds();
    Vector3 vector3_2;
    vector3_2.x = (__Null) (double) Random.Range((float) ((Bounds) ref bounds).get_min().x, (float) ((Bounds) ref bounds).get_max().x);
    vector3_2.z = (__Null) (double) Random.Range((float) ((Bounds) ref bounds).get_min().z, (float) ((Bounds) ref bounds).get_max().z);
    vector3_2.y = (__Null) 1.0;
    RaycastHit raycastHit;
    if ((double) Vector3.Distance(vector3_2, this.Target.get_transform().get_position()) > 5.0 && !Physics.Linecast(vector3_2, ((Component) this).get_transform().get_position(), ref raycastHit, 1024))
      vector3_1 = vector3_2;
    return vector3_1;
  }

  public void attackTarget()
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null) || !Object.op_Equality((Object) this.Target, (Object) this.playerObj))
      return;
    this.lookAtTarget = true;
    if (!((Behaviour) this.enemyPath).get_enabled())
      ((Behaviour) this.enemyPath).set_enabled(true);
    this.enemyPath.canMove = false;
    if (Object.op_Inequality((Object) this.swipeBox.go, (Object) null) && !this.swipeBox.go.get_activeSelf())
      this.swipeBox.go.SetActive(true);
    this.anim.SetBool("attack", true);
  }

  private void FaceTarget()
  {
    Vector3 vector3 = Vector3.RotateTowards(((Component) ((Component) this).get_transform().get_parent()).get_transform().get_forward(), Vector3.op_Subtraction(this.Target.get_transform().get_position(), ((Component) ((Component) this).get_transform().get_parent()).get_transform().get_position()), 360f * Time.get_deltaTime(), 0.0f);
    ((Component) ((Component) this).get_transform().get_parent()).get_transform().set_rotation(Quaternion.LookRotation(vector3));
    Debug.DrawRay(((Component) ((Component) this).get_transform().get_parent()).get_transform().get_position(), vector3, Color.get_red());
  }

  private void Update()
  {
    if (!this.MustReturnHome)
    {
      if (Object.op_Inequality((Object) this.Target, (Object) this.playerObj))
        this.LookForPlayer(this.SearchRadius);
      if (this.lookAtTarget)
      {
        AnimatorStateInfo animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
        if (((AnimatorStateInfo) ref animatorStateInfo).IsName("vampireAttack"))
        {
          if ((double) this.enemyPath.UniqueTimeScale > 0.0)
            this.FaceTarget();
        }
        else
          this.hitbox.DisableEnableHitbox();
      }
    }
    else
    {
      if (Object.op_Inequality((Object) this.swipeBox.go, (Object) null))
        this.swipeBox.go.SetActive(false);
      this.lookAtTarget = false;
      this.anim.SetBool("attack", false);
      if (this.CheckTargetRange(2f))
        this.MustReturnHome = false;
    }
    if (this.recentFlee && (double) Vector3.Distance(this.fleePos, ((Component) ((Component) this).get_transform().get_parent()).get_transform().get_position()) < 2.0)
    {
      this.recentFlee = false;
      this.attackTarget();
    }
    if (this.dead || this.health > 0)
      return;
    this.dead = true;
    this.lookAtTarget = false;
    ((Component) ((Component) this).get_transform().get_parent()).get_gameObject().set_layer(0);
    Object.Destroy((Object) this.swipeBox.go);
    this.enemyPath.canSearch = false;
    ((Behaviour) this.enemyPath).set_enabled(false);
    ((Collider) ((Component) ((Component) this).get_transform().get_parent()).GetComponent<CharacterController>()).set_enabled(false);
    ((Collider) ((Component) this).GetComponent<CapsuleCollider>()).set_enabled(false);
    if (this.giveLoot)
    {
      int currencyLoot = ((EnemyLoot) ((Component) this).GetComponent<EnemyLoot>()).currencyLoot;
      PlayerValues.Instance.AddGold(currencyLoot);
      DamageFeedback.Instance.SpawnCoin(((Component) this).get_transform().get_position(), currencyLoot);
    }
    Object.Destroy((Object) ((Component) ((Component) ((Component) this).get_transform().get_parent()).get_transform().get_parent()).get_gameObject(), 2f);
    this.giveLoot = false;
    this.anim.SetTrigger("death");
    ((AudioSource) ((Component) this).GetComponent<AudioSource>()).Play();
  }
}
