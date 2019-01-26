using UnityEngine;

public class PlayerResources : MonoBehaviour
{
  public static PlayerResources Instance;
  public int Gold;
  public bool attackEnemy;
  public bool openDoor;
  public bool openChest;
  public bool searchEnemy;
  public bool searchDoor;
  public bool searchChest;

  public PlayerResources()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    PlayerResources.Instance = this;
    Object.DontDestroyOnLoad((Object) this);
  }

  private void OnLevelWasLoaded(int level)
  {
    if (level != 0)
      return;
    GameObject player = PlayerSingleton.Instance.getPlayer();
    ((ActiveSkillbitBase) player.GetComponentInChildren<AttackEnemySB>()).Active = this.attackEnemy;
    ((ActiveSkillbitBase) player.GetComponentInChildren<OpenDoorSB>()).Active = this.openDoor;
    ((ActiveSkillbitBase) player.GetComponentInChildren<OpenChestSB>()).Active = this.openChest;
    foreach (SearchForObjectSB componentsInChild in (SearchForObjectSB[]) player.GetComponentsInChildren<SearchForObjectSB>())
    {
      if (((Object) ((Component) componentsInChild).get_gameObject()).get_name() == "SearchEnemyPassiveSB")
        componentsInChild.Active = this.searchEnemy;
      else if (((Object) ((Component) componentsInChild).get_gameObject()).get_name() == "SearchDoorPassiveSB")
        componentsInChild.Active = this.searchDoor;
      else if (((Object) ((Component) componentsInChild).get_gameObject()).get_name() == "SearchChestPassiveSB")
        componentsInChild.Active = this.searchChest;
    }
  }

  public void toggleAttackEnemy()
  {
    if (!this.attackEnemy)
      this.attackEnemy = true;
    else
      this.attackEnemy = false;
  }

  public void toggleOpenDoor()
  {
    if (!this.openDoor)
      this.openDoor = true;
    else
      this.openDoor = false;
  }

  public void toggleOpenChest()
  {
    if (!this.openChest)
      this.openChest = true;
    else
      this.openChest = false;
  }

  public void toggleSearchEnemy()
  {
    if (!this.searchEnemy)
      this.searchEnemy = true;
    else
      this.searchEnemy = false;
  }

  public void toggleSearchDoor()
  {
    if (!this.searchDoor)
      this.searchDoor = true;
    else
      this.searchDoor = false;
  }

  public void toggleSearchChest()
  {
    if (!this.searchChest)
      this.searchChest = true;
    else
      this.searchChest = false;
  }

  private void Update()
  {
  }
}
