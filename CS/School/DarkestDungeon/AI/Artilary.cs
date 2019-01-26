using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class Artilary : MonoBehaviour
{
  public float lifeTime;
  private Animator anim;
  public float speed;
  public float rotSpeed;
  private bool go;
  private GameObject target;
  private float StartPointX;
  private float StartPointY;
  private float ControlPointX;
  private float ControlPointY;
  private float EndPointX;
  private float EndPointY;
  public float CurveX;
  public float CurveY;
  private float BezierTime;

  public Artilary()
  {
    base.\u002Ector();
  }

  private void calcChaseCurve()
  {
    this.StartPointX = (float) ((Component) this).get_transform().get_position().x;
    this.ControlPointX = (float) (this.target.get_transform().get_position().x - ((Component) this).get_transform().get_position().x);
    this.ControlPointY = (float) this.target.get_transform().get_position().y + (float) Random.Range(500, 1000);
    this.EndPointX = (double) this.StartPointX >= (double) this.ControlPointX ? (float) this.target.get_transform().get_position().x - (float) Random.Range(-250, 250) : (float) this.target.get_transform().get_position().x + (float) Random.Range(-250, 250);
    this.EndPointY = (float) this.target.get_transform().get_position().y;
  }

  private void Update()
  {
    if (!this.go)
      return;
    ((Component) this).get_transform().Rotate(0.0f, 0.0f, this.rotSpeed * (float) ((Component) this).get_transform().get_rotation().z, (Space) 1);
    if ((double) this.ControlPointX == 0.0)
      this.calcChaseCurve();
    this.BezierTime += Time.get_deltaTime() / this.speed;
    this.CurveX = (float) ((1.0 - (double) this.BezierTime) * (1.0 - (double) this.BezierTime) * (double) this.StartPointX + 2.0 * (double) this.BezierTime * (1.0 - (double) this.BezierTime) * (double) this.ControlPointX + (double) this.BezierTime * (double) this.BezierTime * (double) this.EndPointX);
    this.CurveY = (float) ((1.0 - (double) this.BezierTime) * (1.0 - (double) this.BezierTime) * (double) this.StartPointY + 2.0 * (double) this.BezierTime * (1.0 - (double) this.BezierTime) * (double) this.ControlPointY + (double) this.BezierTime * (double) this.BezierTime * (double) this.EndPointY);
    ((Component) this).get_transform().set_position(new Vector3(this.CurveX, this.CurveY, 0.0f));
  }

  private void OnEnable()
  {
    this.target = ((BossAI) GameObject.Find("Boss").GetComponent<BossAI>()).target;
    this.StartPointY = (float) ((Component) this).get_transform().get_position().y;
    this.go = true;
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.StartCoroutine(this.animate());
  }

  [DebuggerHidden]
  private IEnumerator animate()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new Artilary.\u003Canimate\u003Ec__Iterator6()
    {
      \u003C\u003Ef__this = this
    };
  }

  public void kill()
  {
    this.go = false;
    ((Component) this).get_transform().set_rotation(Quaternion.get_identity());
    if (Object.op_Inequality((Object) this.anim, (Object) null))
      this.anim.SetBool("hit", true);
    PhotonView.Get((Component) this).RPC("networkDestoryObject", PhotonTargets.AllBufferedViaServer);
  }

  [PunRPC]
  private void networkDestoryObject()
  {
    Object.Destroy((Object) ((Component) this).get_gameObject(), 0.5f);
  }

  private void OnTriggerEnter2D(Collider2D coll)
  {
    if (!(((Component) coll).get_gameObject().get_tag() == "Player") && !(((Component) coll).get_gameObject().get_tag() == "Floor"))
      return;
    this.kill();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (!(collision.get_gameObject().get_tag() == "Player") && !(collision.get_gameObject().get_tag() == "Floor"))
      return;
    this.kill();
  }
}
