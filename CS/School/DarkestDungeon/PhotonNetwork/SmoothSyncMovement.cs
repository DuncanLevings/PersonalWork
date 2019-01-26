using Photon;
using UnityEngine;

public class SmoothSyncMovement : MonoBehaviour
{
  public float SmoothingDelay = 5f;
  private Vector3 correctPlayerPos = Vector3.get_zero();
  private Quaternion correctPlayerRot = Quaternion.get_identity();

  public void Awake()
  {
    if (!Object.op_Equality((Object) this.photonView, (Object) null) && !Object.op_Inequality((Object) this.photonView.observed, (Object) this))
      return;
    Debug.LogWarning((object) (this.ToString() + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used."));
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) ((Component) this).get_transform().get_position());
      stream.SendNext((object) ((Component) this).get_transform().get_rotation());
    }
    else
    {
      this.correctPlayerPos = (Vector3) stream.ReceiveNext();
      this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
    }
  }

  public void Update()
  {
    if (this.photonView.isMine)
      return;
    ((Component) this).get_transform().set_position(Vector3.Lerp(((Component) this).get_transform().get_position(), this.correctPlayerPos, Time.get_deltaTime() * this.SmoothingDelay));
    ((Component) this).get_transform().set_rotation(Quaternion.Lerp(((Component) this).get_transform().get_rotation(), this.correctPlayerRot, Time.get_deltaTime() * this.SmoothingDelay));
  }
}
