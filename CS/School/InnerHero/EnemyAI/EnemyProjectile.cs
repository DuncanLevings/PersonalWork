using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
  public float speed;
  public float TimeToLive;
  public int damage;
  private GameObject player;
  private Vector3 dir;
  private bool triggered;

  public EnemyProjectile()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.player = PlayerSingleton.Instance.getPlayer();
    this.calcDir();
  }

  private void calcDir()
  {
    if (!Object.op_Inequality((Object) this.player, (Object) null))
      return;
    this.dir = Vector3.op_Subtraction(this.player.get_transform().get_position(), ((Component) this).get_transform().get_position());
    ((Vector3) ref this.dir).Normalize();
  }

  private void Update()
  {
    ((Component) this).get_transform().Translate(Vector3.op_Multiply(this.dir, Time.get_deltaTime() * this.speed), (Space) 0);
    this.TimeToLive -= Time.get_deltaTime();
    if ((double) this.TimeToLive > 0.0)
      return;
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }
}
