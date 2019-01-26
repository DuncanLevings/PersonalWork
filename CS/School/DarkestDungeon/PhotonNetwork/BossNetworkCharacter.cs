using Photon;
using UnityEngine;

public class BossNetworkCharacter : MonoBehaviour
{
  private Vector3 correctPos;
  private Quaternion correctRot;
  private Animator anim;
  private Rigidbody2D rb;
  private float health;

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.rb = (Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>();
    this.health = ((BossAI) ((Component) this).GetComponent<BossAI>()).health;
  }

  private void Update()
  {
    if (this.photonView.isMine || !Vector3.op_Inequality(this.correctPos, Vector3.get_zero()))
      return;
    ((Component) this).get_transform().set_position(Vector3.Lerp(((Component) this).get_transform().get_position(), this.correctPos, Time.get_deltaTime() * 20f));
    ((Component) this).get_transform().set_rotation(this.correctRot);
  }

  private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) ((Component) this).get_transform().get_position());
      stream.SendNext((object) ((Component) this).get_transform().get_rotation());
      stream.SendNext((object) this.health);
      if (Object.op_Inequality((Object) this.rb, (Object) null))
        stream.SendNext((object) this.rb.get_velocity());
      if (Object.op_Inequality((Object) this.anim, (Object) null))
        ;
    }
    else
    {
      this.correctPos = (Vector3) stream.ReceiveNext();
      this.correctRot = (Quaternion) stream.ReceiveNext();
      this.health = (float) stream.ReceiveNext();
      if (Object.op_Inequality((Object) this.rb, (Object) null))
        this.rb.set_velocity((Vector2) stream.ReceiveNext());
      if (Object.op_Inequality((Object) this.anim, (Object) null))
        ;
    }
  }
}
