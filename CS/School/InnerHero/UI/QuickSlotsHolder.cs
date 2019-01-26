using System.Collections.Generic;
using UnityEngine;

public class QuickSlotsHolder : MonoBehaviour
{
  public static QuickSlotsHolder Instance;
  public HeroEquipment HeroEquipment;
  public List<UIQuickSlotUse> slotUse;
  private UIQuickSlotUse[] slots;

  public QuickSlotsHolder()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    QuickSlotsHolder.Instance = this;
    if (!Object.op_Inequality((Object) PlayerSingleton.Instance, (Object) null))
      return;
    this.slots = (UIQuickSlotUse[]) ((Component) this).GetComponentsInChildren<UIQuickSlotUse>();
    this.SetSlots();
  }

  public void SetSlots()
  {
    for (int index = 0; index < this.slots.Length; ++index)
    {
      this.slotUse.Add(this.slots[index]);
      if (index < this.HeroEquipment.Consumables.Length && Object.op_Inequality((Object) this.HeroEquipment.Consumables[index].icon, (Object) null))
      {
        GameItem_Consumable_OnDungeon consumableOnDungeon = new GameItem_Consumable_OnDungeon();
        consumableOnDungeon.itemName = this.HeroEquipment.Consumables[index].itemName;
        consumableOnDungeon.icon = this.HeroEquipment.Consumables[index].icon;
        consumableOnDungeon.scripObjRef = this.HeroEquipment.Consumables[index].scripObjRef;
        consumableOnDungeon.indexInInventory = this.HeroEquipment.Consumables[index].indexInInventory;
        this.slots[index].item = consumableOnDungeon;
        ConsumableItemObject scripObjRef = (ConsumableItemObject) this.HeroEquipment.Consumables[index].scripObjRef;
        GameObject gameObject = (GameObject) Object.Instantiate((Object) scripObjRef.effectRef.itemEffect, PlayerSingleton.Instance.getPlayer().get_transform().get_position(), Quaternion.get_identity());
        gameObject.get_transform().SetParent(PlayerSingleton.Instance.getPlayer().get_transform());
        this.slots[index].spawnedItem = gameObject;
        this.slots[index].scriptObjRef = this.HeroEquipment.Consumables[index].scripObjRef;
        if (scripObjRef.effectRef.durationType)
        {
          this.slots[index].durationType = true;
          this.slots[index].duration = scripObjRef.effectRef.duration;
        }
        else
        {
          this.slots[index].durationType = false;
          this.slots[index].cooldown = scripObjRef.effectRef.cooldownTime;
        }
      }
    }
  }

  private void Update()
  {
  }

  public GameObject GetSlot(int index)
  {
    return ((Component) this.slots[index]).get_gameObject();
  }
}
