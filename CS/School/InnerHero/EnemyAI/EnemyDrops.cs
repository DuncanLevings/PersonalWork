using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
  public InventoryObject droppedObject;
  private HeroInventorySO inventory;
  public int inventorySlotIndex;
  public string ItemName;
  public Sprite Icon;
  private bool givenLoot;

  public EnemyDrops()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.inventory = ((player) PlayerSingleton.Instance.getPlayer().GetComponent<player>()).inventory;
  }

  private void Update()
  {
    if (((EnemyAI) ((Component) this).GetComponent<EnemyAI>()).health > 0 || this.givenLoot)
      return;
    this.inventory.InventorySlots[this.inventorySlotIndex].scripObjRef = this.droppedObject;
    this.inventory.InventorySlots[this.inventorySlotIndex].value = 100;
    this.inventory.InventorySlots[this.inventorySlotIndex].itemName = this.ItemName;
    this.inventory.InventorySlots[this.inventorySlotIndex].icon = this.Icon;
    this.givenLoot = true;
  }
}
