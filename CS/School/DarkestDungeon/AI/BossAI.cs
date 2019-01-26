using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
  public float respawnTime;
  public float health;
  public float maxHealth;
  public float movespeed;
  public GameObject target;
  private Animator anim;
  private float cooldown;
  private BossLobbedGun blg;
  private GrenadeLauncher gl;
  private Vector2 dir;
  private BossGun brg;
  public bool secondStage;
  [HideInInspector]
  public float grnadeCD;
  [HideInInspector]
  public float artilaryCD;
  [HideInInspector]
  public float railgunCD;
  public GameObject bow;
  private bool spawnedBow;
  public AnimatorOverrideController SecondBoss;
  private BossEnterHealthUpdate bossHealthCanvas;
  private Slider phase1Bar;
  private Slider phase2Bar;

  public BossAI()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.brg = Object.FindObjectOfType(typeof (BossGun)) as BossGun;
    this.blg = Object.FindObjectOfType(typeof (BossLobbedGun)) as BossLobbedGun;
    this.gl = Object.FindObjectOfType(typeof (GrenadeLauncher)) as GrenadeLauncher;
    this.dir = new Vector2(1f, 1f);
    this.secondStage = false;
    this.maxHealth = this.health;
    this.bossHealthCanvas = (BossEnterHealthUpdate) GameObject.Find("BossHealthCanvas").GetComponent<BossEnterHealthUpdate>();
    this.phase1Bar = this.bossHealthCanvas.bar1;
    this.phase2Bar = this.bossHealthCanvas.bar2;
  }

  public bool playerWithinRadius(float radius)
  {
    if (Object.op_Inequality((Object) this.target, (Object) null))
    {
      foreach (Component component in Physics2D.OverlapCircleAll(Vector2.op_Implicit(((Component) this).get_transform().get_position()), radius))
      {
        if (component.get_tag() == "Player")
          return true;
      }
    }
    return false;
  }

  public bool canCharge()
  {
    if (Object.op_Inequality((Object) this.target, (Object) null))
    {
      Collider2D collider2D = Physics2D.OverlapCircle(Vector2.op_Implicit(((Component) this).get_transform().get_position()), 300f);
      if (((Component) collider2D).get_tag() == "Player" || ((Component) collider2D).get_tag() == "LinkBot")
      {
        ((Rigidbody2D) ((Component) collider2D).GetComponent<Rigidbody2D>()).AddForce(new Vector2(-500f, 200f), (ForceMode2D) 1);
        return true;
      }
    }
    return false;
  }

  public void Charge()
  {
    PhotonView.Get((Component) this).RPC("SetTrigger", PhotonTargets.All, (object) "charge");
    Transform transform = ((Component) this).get_transform();
    transform.set_position(Vector3.op_Subtraction(transform.get_position(), new Vector3(this.movespeed * 3f, 0.0f)));
  }

  public void LobbedAttack()
  {
    if ((double) this.cooldown <= 1.0)
      return;
    this.blg.fire();
    this.artilaryCD = 0.0f;
    this.cooldown = 0.0f;
  }

  public void RailGunAttack()
  {
    if ((double) this.cooldown <= 1.0)
      return;
    this.brg.fire();
    this.railgunCD = 0.0f;
    this.cooldown = 0.0f;
  }

  public void Grenade()
  {
    if ((double) this.cooldown <= 1.0)
      return;
    this.gl.fire();
    this.grnadeCD = 0.0f;
    this.cooldown = 0.0f;
  }

  public void Idle()
  {
    if (!Object.op_Inequality((Object) this.anim, (Object) null))
      return;
    if (((Component) this).get_transform().get_position().x < 300.0)
      this.dir = new Vector2(1f, 0.0f);
    if (((Component) this).get_transform().get_position().x > 800.0)
      this.dir = new Vector2(-1f, 0.0f);
    Transform transform = ((Component) this).get_transform();
    transform.set_position(Vector3.op_Addition(transform.get_position(), new Vector3((float) this.dir.x * this.movespeed, 0.0f)));
  }

  public void Transform()
  {
    if (!Object.op_Inequality((Object) this.anim, (Object) null))
      return;
    PhotonView.Get((Component) this).RPC("SetTrigger", PhotonTargets.All, (object) "transform");
    this.secondStage = true;
    this.anim.set_runtimeAnimatorController((RuntimeAnimatorController) this.SecondBoss);
  }

  private void Update()
  {
    if (!PhotonNetwork.inRoom)
      return;
    this.findClosestPlayer();
    this.artilaryCD += Time.get_deltaTime();
    this.railgunCD += Time.get_deltaTime();
    this.cooldown += Time.get_deltaTime();
    this.grnadeCD += Time.get_deltaTime();
    if (!this.bossHealthCanvas.updateHealth)
      return;
    if ((double) this.health > 9.9)
    {
      this.phase1Bar.set_value(this.health - 10f);
    }
    else
    {
      this.phase1Bar.set_value(0.0f);
      this.phase2Bar.set_value(this.health);
    }
  }

  private void findClosestPlayer()
  {
    GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("Player");
    float num = float.PositiveInfinity;
    Vector3 position = ((Component) this).get_transform().get_position();
    foreach (GameObject gameObject in gameObjectsWithTag)
    {
      Vector3 vector3 = Vector3.op_Subtraction(gameObject.get_transform().get_position(), position);
      float sqrMagnitude = ((Vector3) ref vector3).get_sqrMagnitude();
      if ((double) sqrMagnitude < (double) num)
      {
        this.target = gameObject;
        num = sqrMagnitude;
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D coll)
  {
    if (!(((Component) coll).get_gameObject().get_tag() == "Hitbox"))
      return;
    --this.health;
  }

  public void Die()
  {
    PhotonView.Get((Component) this).RPC("SetTrigger", PhotonTargets.All, (object) "die");
    ((Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>()).set_isKinematic(true);
    ((Behaviour) ((Component) this).GetComponent<BoxCollider2D>()).set_enabled(false);
    if (this.spawnedBow)
      return;
    PhotonNetwork.Instantiate(((Object) this.bow).get_name(), Vector2.op_Implicit(new Vector2((float) (((Component) this).get_transform().get_position().x - 500.0), (float) ((Component) this).get_transform().get_position().y)), ((Component) this).get_transform().get_rotation(), 0);
    this.spawnedBow = true;
  }

  [PunRPC]
  private void SetTrigger(string name)
  {
    if (!Object.op_Inequality((Object) this.anim, (Object) null))
      return;
    this.anim.SetTrigger(name);
  }
}
