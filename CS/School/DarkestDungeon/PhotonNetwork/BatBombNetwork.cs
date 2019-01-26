using Photon;
using UnityEngine;

public class BatBombNetwork : MonoBehaviour
{
  private Vector3 correctBombPos;
  private Animator anim;
  private Rigidbody2D rb;

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.rb = (Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (this.photonView.isMine || !Vector3.op_Inequality(this.correctBombPos, Vector3.get_zero()))
      return;
    ((Component) this).get_transform().set_position(Vector3.Lerp(((Component) this).get_transform().get_position(), this.correctBombPos, Time.get_deltaTime() * 20f));
  }

  private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) ((Component) this).get_transform().get_position());
      if (Object.op_Inequality((Object) this.rb, (Object) null))
        stream.SendNext((object) this.rb.get_velocity());
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      stream.SendNext((object) this.anim.GetBool("explode"));
    }
    else
    {
      this.correctBombPos = (Vector3) stream.ReceiveNext();
      if (Object.op_Inequality((Object) this.rb, (Object) null))
        this.rb.set_velocity((Vector2) stream.ReceiveNext());
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      this.anim.SetBool("explode", (bool) stream.ReceiveNext());
    }
  }
}
