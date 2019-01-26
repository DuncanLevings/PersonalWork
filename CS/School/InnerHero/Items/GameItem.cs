using System;
using UnityEngine;

[Serializable]
public class GameItem : IGetAmount
{
  public string itemName;
  public Sprite icon;
  public int value;
  public int amount;
  public InventoryObject scripObjRef;
  public bool Equipped;
  public GameObject skillbit;
  public UIItemStatic_Bag slotReference;
  public UIEquipSlot equipReference;
  public int indexInInventory;
  public System.Type MyType;

  public GameItem()
  {
  }

  public GameItem(InventoryObject iv)
  {
    this.scripObjRef = iv;
    this.MyType = ((object) this.scripObjRef).GetType();
  }

  public int GetAmount()
  {
    return this.amount;
  }

  public int Add()
  {
    ++this.amount;
    return this.amount;
  }

  public int Subtract()
  {
    --this.amount;
    if (this.amount < 0)
      this.amount = 0;
    return this.amount;
  }

  public UIItemStatic_Bag getBag()
  {
    return this.slotReference;
  }

  public void setBag(UIItemStatic_Bag bag)
  {
    this.slotReference = bag;
  }

  public int GetPrice()
  {
    return this.value;
  }
}
