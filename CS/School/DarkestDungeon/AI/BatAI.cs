using UnityEngine;

public class BatAI : MonoBehaviour
{
  public GameObject coinPrefab;
  public GameObject[] ItemsToDrop;
  public float health;
  public float respawnTime;
  public float movespeed;
  public GameObject bombPrefab;
  public GameObject target;
  private bool droppedItems;
  private Animator anim;
  private bool overPlayer;
  private float StartPointX;
  private float StartPointY;
  private float ControlPointX;
  private float ControlPointY;
  private float EndPointX;
  private float EndPointY;
  public float CurveX;
  public float CurveY;
  private float BezierTime;

  public BatAI()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.StartPointY = (float) ((Component) this).get_transform().get_position().y;
  }

  public void BatIdle()
  {
    Vector3 position = ((Component) this).get_transform().get_position();
    ref Vector3 local = ref position;
    local.x = (__Null) (local.x + 2.0 * (double) Mathf.Sin(Time.get_time() * 1f));
    ((Component) this).get_transform().set_position(position);
    ((Component) this).get_transform().set_position(new Vector3((float) ((Component) this).get_transform().get_position().x, (float) ((Component) this).get_transform().get_position().y, 0.0f));
  }

  public bool InRangeOfPlayer()
  {
    return Object.op_Inequality((Object) this.target, (Object) null) && (double) Vector3.Distance(this.target.get_transform().get_position(), ((Component) this).get_transform().get_position()) < 1500.0;
  }

  private void calcChaseCurve()
  {
    this.StartPointX = (float) ((Component) this).get_transform().get_position().x;
    this.ControlPointX = (float) this.target.get_transform().get_position().x;
    this.ControlPointY = (float) this.target.get_transform().get_position().y + (float) Random.Range(50, 200);
    this.EndPointX = (double) this.StartPointX >= (double) this.ControlPointX ? this.ControlPointX - (float) Random.Range(200, 500) : this.ControlPointX + (float) Random.Range(200, 500);
    this.EndPointY = this.StartPointY;
  }

  public void ChasePlayer()
  {
    if (!Object.op_Inequality((Object) this.target, (Object) null))
      return;
    if ((double) this.ControlPointX == 0.0)
      this.calcChaseCurve();
    this.BezierTime += Time.get_deltaTime() / this.movespeed;
    if ((double) this.BezierTime >= 1.0)
    {
      this.BezierTime = 0.0f;
      this.calcChaseCurve();
    }
    this.CurveX = (float) ((1.0 - (double) this.BezierTime) * (1.0 - (double) this.BezierTime) * (double) this.StartPointX + 2.0 * (double) this.BezierTime * (1.0 - (double) this.BezierTime) * (double) this.ControlPointX + (double) this.BezierTime * (double) this.BezierTime * (double) this.EndPointX);
    this.CurveY = (float) ((1.0 - (double) this.BezierTime) * (1.0 - (double) this.BezierTime) * (double) this.StartPointY + 2.0 * (double) this.BezierTime * (1.0 - (double) this.BezierTime) * (double) this.ControlPointY + (double) this.BezierTime * (double) this.BezierTime * (double) this.EndPointY);
    ((Component) this).get_transform().set_position(new Vector3(this.CurveX, this.CurveY, 0.0f));
  }

  public void DropBomb()
  {
    if (((Component) this).get_transform().get_position().x < this.target.get_transform().get_position().x + 100.0 && ((Component) this).get_transform().get_position().x > this.target.get_transform().get_position().x - 100.0)
    {
      if (this.overPlayer)
        return;
      PhotonNetwork.Instantiate(((Object) this.bombPrefab).get_name(), ((Component) this).get_transform().get_position(), Quaternion.get_identity(), 0);
      this.overPlayer = true;
    }
    else
      this.overPlayer = false;
  }

  private void Update()
  {
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

  public void Die()
  {
    this.anim.SetBool("dead", true);
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

  private void OnTriggerEnter2D(Collider2D coll)
  {
    if (!(((Component) coll).get_gameObject().get_tag() == "Hitbox"))
      return;
    --this.health;
  }
}
