using Photon;
using UnityEngine;

public class NetworkCharacter : MonoBehaviour
{
  private Vector3 correctPlayerPos;
  private Quaternion correctPlayerRot;
  private Animator anim;
  private int lives;
  private int health;
  private Rigidbody2D rb;

  private void Start()
  {
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.rb = (Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>();
    this.lives = ((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).lives;
    this.health = ((UpdateUIAllClients) ((Component) this).GetComponent<UpdateUIAllClients>()).health;
  }

  private void Update()
  {
    if (!this.photonView.isMine)
    {
      if (Vector3.op_Inequality(this.correctPlayerPos, Vector3.get_zero()))
      {
        ((Component) this).get_transform().set_position(Vector3.Lerp(((Component) this).get_transform().get_position(), this.correctPlayerPos, Time.get_deltaTime() * 20f));
        ((Component) this).get_transform().set_rotation(this.correctPlayerRot);
      }
      ((UpdateUIAllClients) ((Component) this).GetComponent<UpdateUIAllClients>()).health = this.health;
    }
    else
      this.health = ((UpdateUIAllClients) ((Component) this).GetComponent<UpdateUIAllClients>()).health;
  }

  private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (stream.isWriting)
    {
      stream.SendNext((object) ((Component) this).get_transform().get_position());
      stream.SendNext((object) ((Component) this).get_transform().get_rotation());
      stream.SendNext((object) this.lives);
      stream.SendNext((object) this.health);
      if (Object.op_Inequality((Object) this.rb, (Object) null))
        stream.SendNext((object) this.rb.get_velocity());
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      stream.SendNext((object) this.anim.GetBool("run"));
      stream.SendNext((object) this.anim.GetBool("jump"));
      stream.SendNext((object) this.anim.GetBool("jumpAtk"));
    }
    else
    {
      this.correctPlayerPos = (Vector3) stream.ReceiveNext();
      this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
      this.lives = (int) stream.ReceiveNext();
      this.health = (int) stream.ReceiveNext();
      if (Object.op_Inequality((Object) this.rb, (Object) null))
        this.rb.set_velocity((Vector2) stream.ReceiveNext());
      if (!Object.op_Inequality((Object) this.anim, (Object) null))
        return;
      this.anim.SetBool("run", (bool) stream.ReceiveNext());
      this.anim.SetBool("jump", (bool) stream.ReceiveNext());
      this.anim.SetBool("jumpAtk", (bool) stream.ReceiveNext());
    }
  }
}
