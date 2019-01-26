using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
  public GameObject projectile;

  public ArrowSpawn()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    PhotonNetwork.Instantiate(((Object) this.projectile).get_name(), Vector2.op_Implicit(new Vector2((float) ((Component) this).get_transform().get_position().x, (float) ((Component) this).get_transform().get_position().y)), ((Component) this).get_transform().get_rotation(), 0);
  }
}
