using UnityEngine;

public class BossAI : MonoBehaviour
{
  [HideInInspector]
  public BossBehaviourBase[] selectedBehaviour;
  private float health;
  private float maxHealth;
  private float healthPercentage;
  private EnemyAI enemyAI;

  public BossAI()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.enemyAI = (EnemyAI) ((Component) this).GetComponent<EnemyAI>();
  }

  private void selectThresholdBehaviours()
  {
    if (!Object.op_Inequality((Object) BossBehaviourSystem.instance, (Object) null) || BossBehaviourSystem.instance.behaviours.Count <= 0)
      return;
    for (int index1 = 0; index1 < 3; ++index1)
    {
      int index2 = Random.Range(0, BossBehaviourSystem.instance.behaviours.Count);
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) BossBehaviourSystem.instance.behaviours[index2]);
      gameObject.get_transform().set_localPosition(Vector3.get_zero());
      gameObject.get_transform().SetParent(((Component) this).get_transform(), false);
      this.selectedBehaviour[index1] = (BossBehaviourBase) gameObject.GetComponent<BossBehaviourBase>();
    }
  }

  private void Update()
  {
    this.maxHealth = (float) this.enemyAI.maxHealth;
    this.health = (float) this.enemyAI.health;
    this.healthPercentage = (float) ((double) this.health / (double) this.maxHealth * 100.0);
    if (Object.op_Equality((Object) this.selectedBehaviour[0], (Object) null))
    {
      this.selectThresholdBehaviours();
    }
    else
    {
      if ((double) this.healthPercentage <= 75.0 && !this.selectedBehaviour[0].activated)
        this.selectedBehaviour[0].behaviour();
      if ((double) this.healthPercentage <= 50.0 && !this.selectedBehaviour[1].activated)
        this.selectedBehaviour[1].behaviour();
      if ((double) this.healthPercentage > 30.0 || this.selectedBehaviour[2].activated)
        return;
      this.selectedBehaviour[2].behaviour();
    }
  }
}
