using Photon;
using UnityEngine;

public class SkeletonNetworkCharacter : MonoBehaviour
{
  private Vector3 correctSkeletonPos;
  private Quaternion correctSkeletonRot;
  private Animator anim;
  private Rigidbody2D rb;

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.rb = (Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (this.photonView.isMine || !Vector3.op_Inequality(this.correctSkeletonPos, Vector3.get_zero()))
      return;
    ((Component) this).get_transform().set_position(Vector3.Lerp(((Component) this).get_transform().get_position(), this.correctSkeletonPos, Time.get_deltaTime() * 20f));
    ((Component) this).get_transform().set_rotation(this.correctSkeletonRot);
  }

  private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) ((Component) this).get_transform().get_position());
      stream.SendNext((object) ((Component) this).get_transform().get_rotation());
      if (Object.op_Inequality((Object) this.rb, (Object) null))
        stream.SendNext((object) this.rb.get_velocity());
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      stream.SendNext((object) this.anim.GetBool("attack"));
      stream.SendNext((object) this.anim.GetBool("run"));
      stream.SendNext((object) this.anim.GetBool("dead"));
      stream.SendNext((object) this.anim.GetBool("hit"));
    }
    else
    {
      this.correctSkeletonPos = (Vector3) stream.ReceiveNext();
      this.correctSkeletonRot = (Quaternion) stream.ReceiveNext();
      if (Object.op_Inequality((Object) this.rb, (Object) null))
        this.rb.set_velocity((Vector2) stream.ReceiveNext());
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      this.anim.SetBool("attack", (bool) stream.ReceiveNext());
      this.anim.SetBool("run", (bool) stream.ReceiveNext());
      this.anim.SetBool("dead", (bool) stream.ReceiveNext());
      this.anim.SetBool("hit", (bool) stream.ReceiveNext());
    }
  }
}
