using System.Collections.Generic;
using UnityEngine;

public class SpawnMinion : BossBehaviourBase
{
  private List<GameObject> enemyToSpawn = new List<GameObject>();
  public int SpawnAmount;
  public List<GameObject> enemyTypes;
  private Vector3[] spawnPoints;

  private void Start()
  {
    this.anim = (Animator) ((Component) ((Component) ((Component) this).get_transform().get_parent()).get_transform().get_parent()).GetComponent<Animator>();
    this.selectEnemys();
    this.spawnPoints = new Vector3[this.enemyToSpawn.Count];
  }

  private void selectEnemys()
  {
    for (int index = 0; index < this.SpawnAmount; ++index)
      this.enemyToSpawn.Add(this.enemyTypes[Random.Range(0, this.enemyTypes.Count)]);
  }

  public override void behaviour()
  {
    this.activated = true;
    CameraShake.Instance.PlayShake(0.5f, 0.75f, false);
    this.anim.SetTrigger("roar");
    for (int index = 0; index < this.enemyToSpawn.Count; ++index)
    {
      Vector3 spawnSpots = this.FindSpawnSpots();
      if (Vector3.op_Inequality(spawnSpots, Vector3.get_zero()))
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.enemyToSpawn[index]);
        gameObject.get_transform().set_position(spawnSpots);
        ((Component) gameObject.GetComponentInChildren<EnemyType>()).get_gameObject().get_transform().set_localPosition(Vector3.get_zero());
        gameObject.get_transform().SetParent(((Component) this).get_transform().get_root(), true);
      }
    }
  }

  private Vector3 FindSpawnSpots()
  {
    Vector3 zero = Vector3.get_zero();
    GameObject player = PlayerSingleton.Instance.getPlayer();
    Bounds bounds = ((Collider) ((Component) ((Component) this).get_transform().get_root()).GetComponent<BoxCollider>()).get_bounds();
    while (Vector3.op_Equality(zero, Vector3.get_zero()))
    {
      float num = Random.Range(0.0f, 6.283185f);
      zero.x = (__Null) (player.get_transform().get_position().x + 3.0 * (double) Mathf.Cos(num));
      zero.z = (__Null) (player.get_transform().get_position().z + 3.0 * (double) Mathf.Sin(num));
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
