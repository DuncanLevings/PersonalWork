using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEquipSlot : UIItemPlaceable, IEventSystemHandler, IPointerUpHandler
{
  public HeroEquipment EquipmentStruc;
  public GameItemDisplay display;

  private void Update()
  {
  }

  public override void PlaceItem(GameObject prefab = null, UIItemStatic caller = null)
  {
    if (!Object.op_Inequality((Object) caller, (Object) null))
      return;
    if (Object.op_Equality((Object) this.display, (Object) null))
      this.display = (GameItemDisplay) ((Component) this).GetComponent<GameItemDisplay>();
    UIItemImage componentInChildren = (UIItemImage) ((Component) caller).GetComponentInChildren<UIItemImage>();
    GameItemDisplay component = (GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>();
    if (!Object.op_Inequality((Object) component.gameItem.equipReference, (Object) null))
      ;
    component.gameItem.Equipped = true;
    if (this.display.gameItem != null && Object.op_Inequality((Object) this.display.gameItem.equipReference, (Object) null))
      this.display.gameItem.equipReference.Unequip();
    ((Behaviour) ((Component) this).GetComponentInChildren<UIItemImage>()).set_enabled(true);
    ((Image) ((Component) this).GetComponentInChildren<UIItemImage>()).set_sprite(componentInChildren.get_sprite());
    this.display.gameItem = ((GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>()).gameItem;
    this.display.gameItem.setBag((UIItemStatic_Bag) caller);
    this.display.gameItem.getBag().displayInfo.gameItem.equipReference = this;
  }

  public virtual void Unequip()
  {
    ((Behaviour) ((Component) this).GetComponentInChildren<UIItemImage>()).set_enabled(false);
    ((Image) ((Component) this).GetComponentInChildren<UIItemImage>()).set_sprite((Sprite) null);
    if (!Object.op_Inequality((Object) this.display, (Object) null) || this.display.gameItem == null || !Object.op_Inequality((Object) this.display.gameItem.icon, (Object) null))
      return;
    UIItemStatic_Bag bag = (UIItemStatic_Bag) null;
    if ((object) ((object) this.display.gameItem.scripObjRef).GetType() == (object) typeof (ConsumableItemObject))
      bag = (UIItemStatic_Bag) HeroBag.Instance.invSlots[this.display.gameItem.indexInInventory].GetComponent<UIItemStatic_Bag>();
    else if ((object) ((object) this.display.gameItem.scripObjRef).GetType() == (object) typeof (SkillbitObject))
      bag = (UIItemStatic_Bag) HeroBag.Instance.invSlotsSkillbit[this.display.gameItem.indexInInventory].GetComponent<UIItemStatic_Bag>();
    else if ((object) ((object) this.display.gameItem.scripObjRef).GetType() == (object) typeof (WeaponObject))
      bag = (UIItemStatic_Bag) HeroBag.Instance.invSlotsWeapon[this.display.gameItem.indexInInventory].GetComponent<UIItemStatic_Bag>();
    this.display.gameItem.setBag(bag);
    this.display.gameItem.Equipped = false;
    if (Object.op_Inequality((Object) this.display.gameItem.getBag(), (Object) null))
    {
      this.display.gameItem.getBag().displayInfo.gameItem.Equipped = false;
      this.display.gameItem.getBag().verifyEquipped();
    }
    this.display.gameItem = (GameItem) null;
  }

  public virtual void OnPointerUp(PointerEventData evData)
  {
    this.Unequip();
  }
}
