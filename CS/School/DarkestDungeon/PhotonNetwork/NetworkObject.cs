using Photon;
using UnityEngine;

public class NetworkObject : MonoBehaviour
{
  private Vector3 correctObjectPos;
  private Quaternion correctObjectRot;
  private Rigidbody2D rb;

  private void Start()
  {
    this.rb = (Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (this.photonView.isMine || !Vector3.op_Inequality(this.correctObjectPos, Vector3.get_zero()))
      return;
    ((Component) this).get_transform().set_position(Vector3.Lerp(((Component) this).get_transform().get_position(), this.correctObjectPos, Time.get_deltaTime() * 20f));
    ((Component) this).get_transform().set_rotation(this.correctObjectRot);
  }

  private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) ((Component) this).get_transform().get_position());
      stream.SendNext((object) ((Component) this).get_transform().get_rotation());
      if (!Object.op_Inequality((Object) this.rb, (Object) null))
        return;
      stream.SendNext((object) this.rb.get_velocity());
    }
    else
    {
      this.correctObjectPos = (Vector3) stream.ReceiveNext();
      this.correctObjectRot = (Quaternion) stream.ReceiveNext();
      if (!Object.op_Inequality((Object) this.rb, (Object) null))
        return;
      this.rb.set_velocity((Vector2) stream.ReceiveNext());
    }
  }
}
