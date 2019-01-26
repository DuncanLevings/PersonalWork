using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
  [HideInInspector]
  public bool canOpenChest;
  [HideInInspector]
  public bool canAttackEnemy;
  public bool ManualMode;
  private static PlayerSingleton _instance;

  public PlayerSingleton()
  {
    base.\u002Ector();
  }

  public static PlayerSingleton Instance
  {
    get
    {
      if (Object.op_Equality((Object) PlayerSingleton._instance, (Object) null))
        PlayerSingleton._instance = (PlayerSingleton) Object.FindObjectOfType<PlayerSingleton>();
      return PlayerSingleton._instance;
    }
  }

  private void Awake()
  {
    if (Object.op_Equality((Object) PlayerSingleton._instance, (Object) null))
    {
      PlayerSingleton._instance = this;
    }
    else
    {
      if (!Object.op_Inequality((Object) this, (Object) PlayerSingleton._instance))
        return;
      Object.Destroy((Object) ((Component) this).get_gameObject());
    }
  }

  public GameObject getPlayer()
  {
    return ((Component) PlayerSingleton._instance).get_gameObject();
  }

  public void CheckSkillbits()
  {
    SearchForObjectSB searchType1 = this.getSearchType(SearchForObjectSB.SearchType.enemy);
    AttackEnemySB componentInChildren1 = (AttackEnemySB) ((Component) this).GetComponentInChildren<AttackEnemySB>();
    if (Object.op_Inequality((Object) searchType1, (Object) null) && Object.op_Inequality((Object) componentInChildren1, (Object) null) && (searchType1.Active && componentInChildren1.Active))
      this.canAttackEnemy = true;
    SearchForObjectSB searchType2 = this.getSearchType(SearchForObjectSB.SearchType.chest);
    OpenChestSB componentInChildren2 = (OpenChestSB) ((Component) this).GetComponentInChildren<OpenChestSB>();
    if (!Object.op_Inequality((Object) searchType2, (Object) null) || !Object.op_Inequality((Object) componentInChildren2, (Object) null) || (!searchType2.Active || !componentInChildren2.Active))
      return;
    this.canOpenChest = true;
  }

  private SearchForObjectSB getSearchType(SearchForObjectSB.SearchType type)
  {
    foreach (SearchForObjectSB componentsInChild in (SearchForObjectSB[]) ((Component) this).GetComponentsInChildren<SearchForObjectSB>())
    {
      if (componentsInChild.type == type)
        return componentsInChild;
    }
    return (SearchForObjectSB) null;
  }
}
