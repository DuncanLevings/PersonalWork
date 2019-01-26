using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
  public GameObject coinPrefab;
  public GameObject[] ItemsToDrop;
  public float respawnTime;
  public float health;
  public float movespeed;
  private bool droppedItems;
  public GameObject target;
  private Animator anim;

  public SkeletonAI()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
  }

  private void moveLeft()
  {
    Transform transform = ((Component) this).get_transform();
    transform.set_position(Vector3.op_Subtraction(transform.get_position(), new Vector3(this.movespeed, 0.0f, 0.0f)));
    this.anim.SetBool("run", true);
  }

  private void moveRight()
  {
    Transform transform = ((Component) this).get_transform();
    transform.set_position(Vector3.op_Addition(transform.get_position(), new Vector3(this.movespeed, 0.0f, 0.0f)));
    this.anim.SetBool("run", true);
  }

  public void ChasePlayer()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null))
      return;
    if (((Component) this).get_transform().get_position().x < this.target.get_transform().get_position().x)
    {
      this.moveRight();
      ((Component) this).get_transform().set_rotation(new Quaternion((float) ((Component) this).get_transform().get_rotation().x, 0.0f, (float) ((Component) this).get_transform().get_rotation().z, (float) ((Component) this).get_transform().get_rotation().w));
    }
    else
    {
      this.moveLeft();
      ((Component) this).get_transform().set_rotation(new Quaternion((float) ((Component) this).get_transform().get_rotation().x, -180f, (float) ((Component) this).get_transform().get_rotation().z, (float) ((Component) this).get_transform().get_rotation().w));
    }
  }

  public bool InRangeOfPlayer(float value)
  {
    return Object.op_Inequality((Object) this.target, (Object) null) && (double) Vector3.Distance(this.target.get_transform().get_position(), ((Component) this).get_transform().get_position()) < (double) value;
  }

  public void AttackPlayer()
  {
    this.anim.SetBool("attack", true);
    this.anim.SetBool("run", false);
  }

  public void SkeleIdle()
  {
    if (!Object.op_Inequality((Object) this.anim, (Object) null))
      return;
    this.anim.SetBool("run", false);
  }

  private void Update()
  {
    AnimatorStateInfo animatorStateInfo1 = this.anim.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo1).IsName("SkeleHit"))
      this.anim.SetBool("hit", false);
    AnimatorStateInfo animatorStateInfo2 = this.anim.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo2).IsName("SkeletonAttack"))
      this.anim.SetBool("attack", false);
    if (!PhotonNetwork.inRoom)
      return;
    this.findClosestPlayer();
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
    if (gameObjectsWithTag.Length >= 1)
      return;
    this.target = (GameObject) null;
  }

  private void OnTriggerEnter2D(Collider2D coll)
  {
    if (!(((Component) coll).get_gameObject().get_tag() == "Hitbox"))
      return;
    --this.health;
    if (Object.op_Inequality((Object) this.anim, (Object) null))
      this.anim.SetBool("hit", true);
    if (!Object.op_Inequality((Object) this.target, (Object) null))
      return;
    if (((Component) this).get_transform().get_position().x < this.target.get_transform().get_position().x)
    {
      ((Rigidbody2D) ((Component) this).get_gameObject().GetComponent<Rigidbody2D>()).AddForce(new Vector2(-300f, 150f), (ForceMode2D) 1);
    }
    else
    {
      if (((Component) this).get_transform().get_position().x <= this.target.get_transform().get_position().x)
        return;
      ((Rigidbody2D) ((Component) this).get_gameObject().GetComponent<Rigidbody2D>()).AddForce(new Vector2(300f, 150f), (ForceMode2D) 1);
    }
  }

  private void OnTriggerExit2D(Collider2D coll)
  {
  }

  public void Die()
  {
    this.anim.SetBool("dead", true);
    ((Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>()).set_isKinematic(true);
    ((Behaviour) ((Component) this).GetComponent<BoxCollider2D>()).set_enabled(false);
    this.Invoke("DestroySelf", this.respawnTime);
  }

  private void DestroySelf()
  {
    if (!this.droppedItems && PhotonView.Get((Component) this).isMine)
    {
      this.droppedItems = true;
      Debug.Log((object) "DROPPED COIN");
      PhotonNetwork.Instantiate(((Object) this.coinPrefab).get_name(), new Vector3(Random.Range((float) (((Component) this).get_transform().get_position().x - 50.0), (float) (((Component) this).get_transform().get_position().x + 50.0)), (float) (((Component) this).get_transform().get_position().y + 100.0), 0.0f), Quaternion.get_identity(), 0);
      PhotonNetwork.Instantiate(((Object) this.ItemsToDrop[Random.Range(0, this.ItemsToDrop.Length)]).get_name(), new Vector3(Random.Range((float) (((Component) this).get_transform().get_position().x - 50.0), (float) (((Component) this).get_transform().get_position().x + 50.0)), (float) (((Component) this).get_transform().get_position().y + 100.0), 0.0f), Quaternion.get_identity(), 0);
    }
    PhotonView.Get((Component) this).RPC("networkDestoryObject", PhotonTargets.AllBufferedViaServer);
  }

  [PunRPC]
  private void networkDestoryObject()
  {
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }
}
