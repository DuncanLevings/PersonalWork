using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEquipSlot_Hands : UIEquipSlot
{
  public UIEquipSlot_Hands.TypeHand hand;

  private void Start()
  {
    if (HeroBag.Instance.invSlots.Count == 0)
      HeroBag.Instance.PopulateInventory();
    this.display = (GameItemDisplay) ((Component) this).GetComponent<GameItemDisplay>();
    UIItemStatic_Bag component = (UIItemStatic_Bag) HeroBag.Instance.invSlots[this.display.gameItem.indexInInventory].GetComponent<UIItemStatic_Bag>();
    this.display.gameItem.setBag(component);
    ((GameItemDisplay) ((Component) component).GetComponent<GameItemDisplay>()).gameItem.equipReference = (UIEquipSlot) this;
    if (this.hand == UIEquipSlot_Hands.TypeHand.Left)
    {
      if (!Object.op_Inequality((Object) this.EquipmentStruc.leftHand.icon, (Object) null))
        return;
      ((Behaviour) ((Component) this).GetComponentInChildren<UIItemImage>()).set_enabled(true);
      ((Image) ((Component) this).GetComponentInChildren<UIItemImage>()).set_sprite(this.EquipmentStruc.leftHand.icon);
      this.display.gameItem = this.EquipmentStruc.leftHand;
    }
    else
    {
      if (!Object.op_Inequality((Object) this.EquipmentStruc, (Object) null) || this.EquipmentStruc.rightHand == null || !Object.op_Inequality((Object) this.EquipmentStruc.rightHand.icon, (Object) null))
        return;
      ((Behaviour) ((Component) this).GetComponentInChildren<UIItemImage>()).set_enabled(true);
      ((Image) ((Component) this).GetComponentInChildren<UIItemImage>()).set_sprite(this.EquipmentStruc.rightHand.icon);
      this.display.gameItem = this.EquipmentStruc.rightHand;
    }
  }

  private void Update()
  {
  }

  public override void PlaceItem(GameObject prefab = null, UIItemStatic caller = null)
  {
    GameItemDisplay component = (GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>();
    if ((object) ((object) component.gameItem.scripObjRef).GetType() != (object) typeof (WeaponObject))
      return;
    base.PlaceItem(prefab, caller);
    if (this.hand == UIEquipSlot_Hands.TypeHand.Left)
      this.EquipmentStruc.leftHand = ((GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>()).gameItem;
    else
      this.EquipmentStruc.rightHand = ((GameItemDisplay) ((Component) caller).GetComponent<GameItemDisplay>()).gameItem;
    UIItemStatic_Bag bag = (UIItemStatic_Bag) caller;
    this.display.gameItem.setBag(bag);
    if (Object.op_Equality((Object) component.gameItem.getBag(), (Object) null))
      bag.setGameItemReference();
    component.gameItem.getBag().verifyEquipped();
  }

  public override void OnPointerUp(PointerEventData evData)
  {
    base.OnPointerUp(evData);
    this.Unequip();
  }

  public override void Unequip()
  {
    base.Unequip();
    if (this.hand == UIEquipSlot_Hands.TypeHand.Left)
      this.EquipmentStruc.leftHand = (GameItem) null;
    if (this.hand != UIEquipSlot_Hands.TypeHand.Right)
      return;
    this.EquipmentStruc.rightHand = (GameItem) null;
  }

  public enum TypeHand
  {
    Left,
    Right,
  }
}
