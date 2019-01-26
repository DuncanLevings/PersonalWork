using UnityEngine;

public class CriticalPotion : ItemUseBase
{
  public GameObject skillbit;
  [Range(0.0f, 1f)]
  public float critChanceMultiplier;
  private GameObject go;
  public float duration;
  private float timer;
  private bool on;
  private bool exists;

  private void Start()
  {
    if (Object.op_Implicit((Object) Object.FindObjectOfType<CriticalPassiveSB>()))
    {
      this.go = ((Component) Object.FindObjectOfType<CriticalPassiveSB>()).get_gameObject();
      this.exists = true;
    }
    else
    {
      this.go = (GameObject) Object.Instantiate<GameObject>((M0) this.skillbit);
      this.go.get_transform().SetParent(PlayerSingleton.Instance.getPlayer().get_transform());
      M0 component = this.go.GetComponent<CriticalPassiveSB>();
      ((CriticalPassiveSB) component).procChance = ((CriticalPassiveSB) component).procChance - this.critChanceMultiplier;
      foreach (EnemyAI enemyAi in (EnemyAI[]) Object.FindObjectsOfType<EnemyAI>())
        enemyAi.SetHitProcs();
      this.go.SetActive(false);
    }
  }

  public override void UseItem()
  {
    this.on = true;
    if (this.exists)
    {
      M0 component = this.go.GetComponent<CriticalPassiveSB>();
      ((CriticalPassiveSB) component).procChance = ((CriticalPassiveSB) component).procChance - this.critChanceMultiplier;
    }
    else
      this.go.SetActive(true);
    this.timer = this.duration;
  }

  private void Update()
  {
    if (!this.on)
      return;
    this.timer -= Time.get_deltaTime();
    if ((double) this.timer > 0.0)
      return;
    if (this.exists)
    {
      M0 component = this.go.GetComponent<CriticalPassiveSB>();
      ((CriticalPassiveSB) component).procChance = ((CriticalPassiveSB) component).procChance + this.critChanceMultiplier;
    }
    else
      this.go.SetActive(false);
    this.timer = this.duration;
    this.on = false;
  }
}
