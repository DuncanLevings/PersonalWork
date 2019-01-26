using UnityEngine;

public class QuickSlot : MonoBehaviour
{
  public GameObject enemy;
  public GameObject slot;
  public GameObject nextEvent;
  private bool triggered;
  public HeroInventorySO inventory;
  public HeroEquipment heroEquipment;
  public ConsumableItemObject potion;

  public QuickSlot()
  {
    base.\u002Ector();
  }

  private void Start()
  {
  }

  private void Update()
  {
    if (this.triggered || ((EnemyAI) this.enemy.GetComponent<EnemyAI>()).health > 0)
      return;
    this.explain();
    this.triggered = true;
  }

  private void explain()
  {
    TutorialEventManagerLevel1.Tut1Instance.executeDialog = nameof (QuickSlot);
    TimeScale.Instance.scale = 0.0f;
  }

  public void FinishedExplain()
  {
    this.inventory.InventorySlots = new GameItem[1];
    this.inventory.InventorySlots[0] = new GameItem()
    {
      icon = this.potion.img,
      itemName = this.potion.name,
      scripObjRef = (InventoryObject) this.potion,
      amount = 1,
      Equipped = true
    };
    this.heroEquipment.Consumables[0].icon = this.potion.img;
    this.heroEquipment.Consumables[0].scripObjRef = (InventoryObject) this.potion;
    this.heroEquipment.Consumables[0].amount = 1;
    this.heroEquipment.Consumables[0].indexInInventory = 0;
    this.heroEquipment.Consumables[0].Equipped = true;
    ((UIQuickSlotUse) this.slot.GetComponent<UIQuickSlotUse>()).item = new GameItem_Consumable_OnDungeon();
    ((UIQuickSlotUse) this.slot.GetComponent<UIQuickSlotUse>()).item.icon = this.potion.img;
    ((UIQuickSlotUse) this.slot.GetComponent<UIQuickSlotUse>()).item.scripObjRef = (InventoryObject) this.potion;
    ((UIQuickSlotUse) this.slot.GetComponent<UIQuickSlotUse>()).item.indexInInventory = 0;
    PauseInteractionSystem.Instance.SetInteractionEvent(this.slot, PauseInteractionSystem.InteractionType.Custom, 0.0f, false);
    ((Behaviour) this.slot.GetComponent<UIQuickSlotUse>()).set_enabled(true);
    ((UIQuickSlotUse) this.slot.GetComponent<UIQuickSlotUse>()).AppearItem();
    ((Behaviour) this.nextEvent.GetComponent<UsedPotion>()).set_enabled(true);
  }
}
