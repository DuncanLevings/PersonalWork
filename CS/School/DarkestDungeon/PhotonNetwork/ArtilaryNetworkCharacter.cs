using Photon;
using UnityEngine;

public class ArtilaryNetworkCharacter : MonoBehaviour
{
  private Vector3 correctPos;
  private Quaternion correctRot;
  private Animator anim;

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
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
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      stream.SendNext((object) this.anim.GetBool("hit"));
    }
    else
    {
      this.correctPos = (Vector3) stream.ReceiveNext();
      this.correctRot = (Quaternion) stream.ReceiveNext();
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      this.anim.SetBool("hit", (bool) stream.ReceiveNext());
    }
  }
}
