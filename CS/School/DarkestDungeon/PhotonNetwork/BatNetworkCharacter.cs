using Photon;
using UnityEngine;

public class BatNetworkCharacter : MonoBehaviour
{
  private Vector3 correctBatPos;
  private Animator anim;
  private BatAI batAI;

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.batAI = (BatAI) ((Component) this).GetComponent<BatAI>();
  }

  private void Update()
  {
    if (this.photonView.isMine || !Vector3.op_Inequality(this.correctBatPos, Vector3.get_zero()))
      return;
    ((Component) this).get_transform().set_position(Vector3.Lerp(((Component) this).get_transform().get_position(), this.correctBatPos, 0.1f));
  }

  private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) ((Component) this).get_transform().get_position());
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      stream.SendNext((object) this.anim.GetBool("dead"));
    }
    else
    {
      this.correctBatPos = (Vector3) stream.ReceiveNext();
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      this.anim.SetBool("dead", (bool) stream.ReceiveNext());
    }
  }
}
