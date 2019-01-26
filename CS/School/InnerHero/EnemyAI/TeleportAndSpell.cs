using UnityEngine;

public class TeleportAndSpell : BossBehaviourBase
{
  private GameObject boss;

  private void Start()
  {
    this.anim = (Animator) ((Component) ((Component) ((Component) this).get_transform().get_parent()).get_transform().get_parent()).GetComponent<Animator>();
    this.boss = ((Component) ((Component) ((Component) ((Component) this).get_transform().get_parent()).get_transform().get_parent()).get_transform().get_parent()).get_gameObject();
  }

  public override void behaviour()
  {
    this.activated = true;
    CameraShake.Instance.PlayShake(0.5f, 0.75f, false);
    Vector3 spawnSpots = this.FindSpawnSpots();
    if (!Vector3.op_Inequality(spawnSpots, Vector3.get_zero()))
      return;
    this.boss.get_transform().set_position(spawnSpots);
    ((Component) this.boss.GetComponentInChildren<EnemyType>()).get_gameObject().get_transform().set_localPosition(Vector3.get_zero());
    this.anim.SetTrigger("spell");
  }

  private Vector3 FindSpawnSpots()
  {
    Vector3 zero = Vector3.get_zero();
    GameObject player = PlayerSingleton.Instance.getPlayer();
    Bounds bounds = ((Collider) ((Component) ((Component) this).get_transform().get_root()).GetComponent<BoxCollider>()).get_bounds();
    while (Vector3.op_Equality(zero, Vector3.get_zero()))
    {
      float num = Random.Range(0.0f, 6.283185f);
      zero.x = (__Null) (player.get_transform().get_position().x + 5.0 * (double) Mathf.Cos(num));
      zero.z = (__Null) (player.get_transform().get_position().z + 5.0 * (double) Mathf.Sin(num));
      zero.y = (__Null) 0.5;
      if (((Bounds) ref bounds).Contains(zero))
      {
        if (Physics.OverlapSphere(((Component) this).get_transform().get_position(), 1f, 1024).Length == 0 && !Physics.Linecast(zero, ((Component) this).get_transform().get_position(), 1024))
          return zero;
      }
      else
        zero = Vector3.get_zero();
    }
    return zero;
  }

  private void Update()
  {
  }
}
