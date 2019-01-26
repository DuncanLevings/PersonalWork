using UnityEngine;
using UnityEngine.UI;

public class UIEquipSlot_Skillbit : UIEquipSlot
{
  public UIEquipSlot_Skillbit.TypeSB typeSB;
  public UIItemImage img;

  private void Start()
  {
    this.display = (GameItemDisplay) ((Component) this).GetComponent<GameItemDisplay>();
    ((UISlotInteractable) this).Start();
    this.img = (UIItemImage) ((Component) this).GetComponentInChildren<UIItemImage>();
    if (this.typeSB == UIEquipSlot_Skillbit.TypeSB.Active)
    {
      if (HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject()) == -1 || !Object.op_Inequality((Object) this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].skillbit, (Object) null))
        return;
      ((Behaviour) ((Component) this).GetComponentInChildren<UIItemImage>()).set_enabled(true);
      ((Image) ((Component) this).GetComponentInChildren<UIItemImage>()).set_sprite(this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].icon);
      this.display.gameItem.slotReference = (UIItemStatic_Bag) HeroBag.Instance.invSlotsSkillbit[this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].indexInInventory].GetComponent<UIItemStatic_Bag>();
      if (Object.op_Equality((Object) this.display.gameItem.slotReference.displayInfo, (Object) null))
        this.display.gameItem.slotReference.Start();
      this.display.gameItem.slotReference.displayInfo.gameItem.equipReference = (UIEquipSlot) this;
      this.display.gameItem = this.display.gameItem.slotReference.displayInfo.gameItem;
      this.display.gameItem.icon = this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].icon;
      this.display.gameItem.itemName = this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].itemName;
      this.display.gameItem.indexInInventory = this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].indexInInventory;
      this.display.gameItem.scripObjRef = HeroBag.Instance.hIv.InventorySlots_Skillbits[this.display.gameItem.indexInInventory].scripObjRef;
      this.display.gameItem.Equipped = true;
    }
    else
    {
      if (this.typeSB != UIEquipSlot_Skillbit.TypeSB.Passive || HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject()) == -1 || !Object.op_Inequality((Object) this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].skillbit, (Object) null))
        return;
      this.img = (UIItemImage) ((Component) this).GetComponentInChildren<UIItemImage>();
      ((Behaviour) ((Component) this).GetComponentInChildren<UIItemImage>()).set_enabled(true);
      ((Image) ((Component) this).GetComponentInChildren<UIItemImage>()).set_sprite(this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].icon);
      this.display.gameItem.slotReference = (UIItemStatic_Bag) HeroBag.Instance.invSlotsSkillbit[this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].indexInInventory].GetComponent<UIItemStatic_Bag>();
      if (Object.op_Equality((Object) this.display.gameItem.slotReference.displayInfo, (Object) null))
        this.display.gameItem.slotReference.Start();
      this.display.gameItem.slotReference.displayInfo.gameItem.equipReference = (UIEquipSlot) this;
      this.display.gameItem = this.display.gameItem.slotReference.displayInfo.gameItem;
      this.display.gameItem.icon = this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].icon;
      this.display.gameItem.itemName = this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].itemName;
      this.display.gameItem.indexInInventory = this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].indexInInventory;
      this.display.gameItem.scripObjRef = HeroBag.Instance.hIv.InventorySlots_Skillbits[this.display.gameItem.indexInInventory].scripObjRef;
      this.display.gameItem.Equipped = true;
    }
  }

  private void Update()
  {
  }

  public override void PlaceItem(GameObject prefab = null, UIItemStatic caller = null)
  {
    GameItemDisplay component = (GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>();
    if ((object) ((object) component.gameItem.scripObjRef).GetType() != (object) typeof (SkillbitObject))
      return;
    base.PlaceItem(prefab, caller);
    SkillbitObject scripObjRef = (SkillbitObject) ((GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>()).gameItem.scripObjRef;
    GameItem gameItem = new GameItem();
    gameItem.skillbit = scripObjRef.SkillBit;
    gameItem.icon = scripObjRef.img;
    if (scripObjRef.active)
    {
      int index = HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject());
      this.EquipmentStruc.EquipableSkillBits_Active[index] = gameItem;
      this.EquipmentStruc.EquipableSkillBits_Active[index].Equipped = true;
      this.EquipmentStruc.EquipableSkillBits_Active[index].scripObjRef = caller.GetIvObj();
      this.EquipmentStruc.EquipableSkillBits_Active[index].itemName = ((GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>()).gameItem.itemName;
      this.EquipmentStruc.EquipableSkillBits_Active[index].indexInInventory = ((GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>()).gameItem.indexInInventory;
    }
    else if (scripObjRef.passive)
    {
      int index = HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject());
      this.EquipmentStruc.EquipableSkillBits_Passive[index] = gameItem;
      this.EquipmentStruc.EquipableSkillBits_Passive[index].Equipped = true;
      this.EquipmentStruc.EquipableSkillBits_Passive[index].scripObjRef = caller.GetIvObj();
      this.EquipmentStruc.EquipableSkillBits_Passive[index].itemName = ((GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>()).gameItem.itemName;
      this.EquipmentStruc.EquipableSkillBits_Passive[index].indexInInventory = ((GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>()).gameItem.indexInInventory;
    }
    UIItemStatic_Bag uiItemStaticBag = (UIItemStatic_Bag) caller;
    if (Object.op_Equality((Object) component.gameItem.getBag(), (Object) null))
      uiItemStaticBag.setGameItemReference();
    component.gameItem.getBag().verifyEquipped();
  }

  public override void Unequip()
  {
    base.Unequip();
    if (this.typeSB == UIEquipSlot_Skillbit.TypeSB.Active)
    {
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].itemName = (string) null;
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].icon = (Sprite) null;
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].value = 0;
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].amount = 0;
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].scripObjRef = (InventoryObject) null;
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].Equipped = false;
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].skillbit = (GameObject) null;
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())].indexInInventory = 0;
      this.EquipmentStruc.EquipableSkillBits_Active[HeroBag.Instance.skillbitSlots_active.IndexOf(((Component) this).get_gameObject())] = (GameItem) null;
    }
    else
    {
      if (this.typeSB != UIEquipSlot_Skillbit.TypeSB.Passive)
        return;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].itemName = (string) null;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].icon = (Sprite) null;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].value = 0;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].amount = 0;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].scripObjRef = (InventoryObject) null;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].Equipped = false;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].skillbit = (GameObject) null;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())].indexInInventory = 0;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())] = (GameItem) null;
      this.EquipmentStruc.EquipableSkillBits_Passive[HeroBag.Instance.skillbitSlots_passive.IndexOf(((Component) this).get_gameObject())] = (GameItem) null;
    }
  }

  public enum TypeSB
  {
    Active,
    Passive,
  }
}
