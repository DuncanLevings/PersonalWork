using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBag : MonoBehaviour
{
  private static HeroBag _instance;
  public bool showInv;
  public List<GameItem> inventory;
  public List<GameItem> inventoryEquipments;
  public List<GameItem> inventorySkillbits;
  public List<GameObject> invSlots;
  public List<GameObject> invSlotsWeapon;
  public List<GameObject> invSlotsSkillbit;
  public List<GameObject> consumableSlots;
  public List<GameObject> skillbitSlots_passive;
  public List<GameObject> skillbitSlots_active;
  public UIEquipSlot_Hands rightHand;
  public HeroInventorySO hIv;
  public HeroEquipment equips;
  public Sprite blankSprite;

  public HeroBag()
  {
    base.\u002Ector();
  }

  public static HeroBag Instance
  {
    get
    {
      return HeroBag._instance;
    }
  }

  private void Awake()
  {
    HeroBag._instance = this;
  }

  public void Start()
  {
    HeroBag._instance = this;
    this.FillInventory();
    this.SetConsumableSlot();
    this.setSkillBitSlots();
  }

  public void FillInventory()
  {
    if (this.invSlots.Count != 0)
      return;
    IEnumerator enumerator1 = ((Component) this).get_transform().GetEnumerator();
    try
    {
      while (enumerator1.MoveNext())
      {
        Transform current1 = (Transform) enumerator1.Current;
        if (Object.op_Inequality((Object) ((Component) current1).GetComponent<ConsumableWindow>(), (Object) null))
        {
          IEnumerator enumerator2 = ((Component) ((Component) current1).GetComponent<ConsumableWindow>()).get_transform().GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
            {
              Transform current2 = (Transform) enumerator2.Current;
              if (Object.op_Inequality((Object) ((Component) current2).GetComponent<CardsHolder>(), (Object) null))
              {
                CardsHolder component = (CardsHolder) ((Component) current2).GetComponent<CardsHolder>();
                IEnumerator enumerator3 = ((Component) component).get_transform().GetEnumerator();
                try
                {
                  while (enumerator3.MoveNext())
                  {
                    Transform current3 = (Transform) enumerator3.Current;
                    this.invSlots.Add(((Component) component).get_gameObject());
                  }
                }
                finally
                {
                  (enumerator3 as IDisposable)?.Dispose();
                }
              }
            }
          }
          finally
          {
            (enumerator2 as IDisposable)?.Dispose();
          }
        }
      }
    }
    finally
    {
      (enumerator1 as IDisposable)?.Dispose();
    }
  }

  public void SetSingleton()
  {
    HeroBag._instance = this;
    this.PopulateInventory();
  }

  public void SetConsumableSlot()
  {
    if (this.consumableSlots.Count != 0)
      return;
    foreach (Component componentsInChild in (UIConsumableSlot[]) ((Component) this).GetComponentsInChildren<UIConsumableSlot>())
      this.consumableSlots.Add(componentsInChild.get_gameObject());
  }

  private void verifyStrucutresToBeReorganized()
  {
    for (int index = 1; index < this.consumableSlots.Count; ++index)
    {
      GameItemDisplay component1 = (GameItemDisplay) this.consumableSlots[index - 1].GetComponent<GameItemDisplay>();
      GameItemDisplay component2 = (GameItemDisplay) this.consumableSlots[index].GetComponent<GameItemDisplay>();
      if (component1.gameItem == null && component2.gameItem != null)
      {
        component1.gameItem = new GameItem();
        component1.gameItem.itemName = component2.gameItem.itemName;
        component1.gameItem.icon = component2.gameItem.icon;
        component1.gameItem.value = component2.gameItem.value;
        component1.gameItem.amount = component2.gameItem.amount;
        component1.gameItem.scripObjRef = component2.gameItem.scripObjRef;
        component1.gameItem.Equipped = component2.gameItem.Equipped;
        component1.gameItem.skillbit = component2.gameItem.skillbit;
        component1.gameItem.slotReference = component2.gameItem.slotReference;
        component1.gameItem.equipReference = (UIEquipSlot) ((Component) component1).GetComponent<UIConsumableSlot>();
        component1.gameItem.indexInInventory = component2.gameItem.indexInInventory;
        Image img1 = (Image) ((UIConsumableSlot) ((Component) component1).GetComponent<UIConsumableSlot>()).img;
        ((Behaviour) img1).set_enabled(true);
        if (Object.op_Inequality((Object) component1.gameItem.icon, (Object) null))
          img1.set_sprite(component1.gameItem.icon);
        else
          img1.set_sprite(this.blankSprite);
        ((Text) ((Component) ((Component) component1).GetComponentInChildren<UITextCounter>()).GetComponent<Text>()).set_text("x" + component1.gameItem.amount.ToString());
        component2.gameItem = (GameItem) null;
        Image img2 = (Image) ((UIConsumableSlot) ((Component) component2).GetComponent<UIConsumableSlot>()).img;
        ((Behaviour) img2).set_enabled(false);
        img2.set_sprite(this.blankSprite);
        ((Text) ((Component) ((Component) component2).GetComponentInChildren<UITextCounter>()).GetComponent<Text>()).set_text("x 0");
        this.equips.Consumables[index - 1] = new GameItem_Consumable();
        this.equips.Consumables[index - 1].icon = this.equips.Consumables[index].icon;
        this.equips.Consumables[index - 1].itemName = this.equips.Consumables[index].itemName;
        this.equips.Consumables[index - 1].amount = this.equips.Consumables[index].amount;
        this.equips.Consumables[index - 1].scripObjRef = this.equips.Consumables[index].scripObjRef;
        this.equips.Consumables[index - 1].slotReference = component1.gameItem.slotReference;
        this.equips.Consumables[index - 1].equipReference = (UIEquipSlot) ((Component) component1).GetComponent<UIConsumableSlot>();
        this.equips.Consumables[index - 1].indexInInventory = this.equips.Consumables[index].indexInInventory;
        this.equips.Consumables[index - 1].InventoryIndex = this.equips.Consumables[index].InventoryIndex;
        this.equips.Consumables[index].icon = (Sprite) null;
        this.equips.Consumables[index].itemName = (string) null;
        this.equips.Consumables[index].scripObjRef = (InventoryObject) null;
        this.equips.Consumables[index].indexInInventory = 0;
        this.equips.Consumables[index].InventoryIndex = 0;
      }
    }
    for (int index = 1; index < this.skillbitSlots_passive.Count; ++index)
    {
      GameItemDisplay component1 = (GameItemDisplay) this.skillbitSlots_passive[index - 1].GetComponent<GameItemDisplay>();
      GameItemDisplay component2 = (GameItemDisplay) this.skillbitSlots_passive[index].GetComponent<GameItemDisplay>();
      if (component1.gameItem == null && component2.gameItem != null)
      {
        component1.gameItem = new GameItem();
        component1.gameItem.itemName = component2.gameItem.itemName;
        component1.gameItem.icon = component2.gameItem.icon;
        component1.gameItem.value = component2.gameItem.value;
        component1.gameItem.amount = component2.gameItem.amount;
        component1.gameItem.scripObjRef = component2.gameItem.scripObjRef;
        component1.gameItem.Equipped = component2.gameItem.Equipped;
        component1.gameItem.skillbit = component2.gameItem.skillbit;
        component1.gameItem.slotReference = component2.gameItem.slotReference;
        component1.gameItem.equipReference = (UIEquipSlot) ((Component) component1).GetComponent<UIEquipSlot_Skillbit>();
        component1.gameItem.indexInInventory = component2.gameItem.indexInInventory;
        Image img1 = (Image) ((UIEquipSlot_Skillbit) ((Component) component1).GetComponent<UIEquipSlot_Skillbit>()).img;
        ((Behaviour) img1).set_enabled(true);
        if (Object.op_Inequality((Object) component1.gameItem.icon, (Object) null))
          img1.set_sprite(component1.gameItem.icon);
        else
          img1.set_sprite(this.blankSprite);
        component2.gameItem = (GameItem) null;
        Image img2 = (Image) ((UIEquipSlot_Skillbit) ((Component) component2).GetComponent<UIEquipSlot_Skillbit>()).img;
        ((Behaviour) img2).set_enabled(false);
        img2.set_sprite(this.blankSprite);
        this.equips.EquipableSkillBits_Passive[index - 1] = new GameItem();
        this.equips.EquipableSkillBits_Passive[index - 1].icon = this.equips.EquipableSkillBits_Passive[index].icon;
        this.equips.EquipableSkillBits_Passive[index - 1].skillbit = this.equips.EquipableSkillBits_Passive[index].skillbit;
        this.equips.EquipableSkillBits_Passive[index - 1].itemName = this.equips.EquipableSkillBits_Passive[index].itemName;
        this.equips.EquipableSkillBits_Passive[index - 1].amount = this.equips.EquipableSkillBits_Passive[index].amount;
        this.equips.EquipableSkillBits_Passive[index - 1].scripObjRef = this.equips.EquipableSkillBits_Passive[index].scripObjRef;
        this.equips.EquipableSkillBits_Passive[index - 1].slotReference = component1.gameItem.slotReference;
        this.equips.EquipableSkillBits_Passive[index - 1].equipReference = (UIEquipSlot) ((Component) component1).GetComponent<UIEquipSlot_Skillbit>();
        this.equips.EquipableSkillBits_Passive[index - 1].indexInInventory = this.equips.EquipableSkillBits_Passive[index].indexInInventory;
        this.equips.EquipableSkillBits_Passive[index - 1].Equipped = true;
        this.equips.EquipableSkillBits_Passive[index].icon = this.blankSprite;
        this.equips.EquipableSkillBits_Passive[index].itemName = (string) null;
        this.equips.EquipableSkillBits_Passive[index].scripObjRef = (InventoryObject) null;
        this.equips.EquipableSkillBits_Passive[index].indexInInventory = 0;
        this.equips.EquipableSkillBits_Passive[index].Equipped = false;
        this.equips.EquipableSkillBits_Passive[index].skillbit = (GameObject) null;
      }
    }
    for (int index = 1; index < this.skillbitSlots_active.Count; ++index)
    {
      GameItemDisplay component1 = (GameItemDisplay) this.skillbitSlots_active[index - 1].GetComponent<GameItemDisplay>();
      GameItemDisplay component2 = (GameItemDisplay) this.skillbitSlots_active[index].GetComponent<GameItemDisplay>();
      if (component1.gameItem == null && component2.gameItem != null)
      {
        component1.gameItem = new GameItem();
        component1.gameItem.itemName = component2.gameItem.itemName;
        component1.gameItem.icon = component2.gameItem.icon;
        component1.gameItem.value = component2.gameItem.value;
        component1.gameItem.amount = component2.gameItem.amount;
        component1.gameItem.scripObjRef = component2.gameItem.scripObjRef;
        component1.gameItem.Equipped = component2.gameItem.Equipped;
        component1.gameItem.skillbit = component2.gameItem.skillbit;
        component1.gameItem.slotReference = component2.gameItem.slotReference;
        component1.gameItem.equipReference = (UIEquipSlot) ((Component) component1).GetComponent<UIEquipSlot_Skillbit>();
        component1.gameItem.indexInInventory = component2.gameItem.indexInInventory;
        Image img1 = (Image) ((UIEquipSlot_Skillbit) ((Component) component1).GetComponent<UIEquipSlot_Skillbit>()).img;
        ((Behaviour) img1).set_enabled(true);
        if (Object.op_Inequality((Object) component1.gameItem.icon, (Object) null))
          img1.set_sprite(component1.gameItem.icon);
        else
          img1.set_sprite(this.blankSprite);
        component2.gameItem = (GameItem) null;
        Image img2 = (Image) ((UIEquipSlot_Skillbit) ((Component) component2).GetComponent<UIEquipSlot_Skillbit>()).img;
        ((Behaviour) img2).set_enabled(false);
        img2.set_sprite(this.blankSprite);
        this.equips.EquipableSkillBits_Active[index - 1] = new GameItem();
        this.equips.EquipableSkillBits_Active[index - 1].icon = this.equips.EquipableSkillBits_Active[index].icon;
        this.equips.EquipableSkillBits_Active[index - 1].skillbit = this.equips.EquipableSkillBits_Active[index].skillbit;
        this.equips.EquipableSkillBits_Active[index - 1].itemName = this.equips.EquipableSkillBits_Active[index].itemName;
        this.equips.EquipableSkillBits_Active[index - 1].amount = this.equips.EquipableSkillBits_Active[index].amount;
        this.equips.EquipableSkillBits_Active[index - 1].scripObjRef = this.equips.EquipableSkillBits_Active[index].scripObjRef;
        this.equips.EquipableSkillBits_Active[index - 1].slotReference = component1.gameItem.slotReference;
        this.equips.EquipableSkillBits_Active[index - 1].equipReference = (UIEquipSlot) ((Component) component1).GetComponent<UIEquipSlot_Skillbit>();
        this.equips.EquipableSkillBits_Active[index - 1].indexInInventory = this.equips.EquipableSkillBits_Active[index].indexInInventory;
        this.equips.EquipableSkillBits_Active[index - 1].Equipped = true;
        this.equips.EquipableSkillBits_Active[index].icon = this.blankSprite;
        this.equips.EquipableSkillBits_Active[index].itemName = (string) null;
        this.equips.EquipableSkillBits_Active[index].scripObjRef = (InventoryObject) null;
        this.equips.EquipableSkillBits_Active[index].indexInInventory = 0;
        this.equips.EquipableSkillBits_Active[index].Equipped = false;
        this.equips.EquipableSkillBits_Active[index].skillbit = (GameObject) null;
      }
    }
  }

  public void setSkillBitSlots()
  {
    if (this.skillbitSlots_active.Count != 0)
      return;
    foreach (Component componentsInChild in (UIEquipSlot_Skillbit[]) ((Component) this).GetComponentsInChildren<UIEquipSlot_Skillbit>())
      this.skillbitSlots_active.Add(componentsInChild.get_gameObject());
  }

  private void Update()
  {
    this.verifyStrucutresToBeReorganized();
  }

  public void ToggleInventory(bool toggle)
  {
    ((Component) this).get_gameObject().SetActive(toggle);
    this.PopulateInventory();
  }

  public void PopulateInventory()
  {
    this.FillInventory();
    this.SetConsumableSlot();
    if (!Object.op_Inequality((Object) this.hIv, (Object) null))
      return;
    this.inventory.Clear();
    for (int index = 0; index < this.invSlots.Count; ++index)
    {
      if (index < this.hIv.InventorySlots.Length)
      {
        this.invSlots[index].SetActive(true);
        this.inventory.Add(this.hIv.InventorySlots[index]);
        GameItemDisplay component = (GameItemDisplay) this.invSlots[index].GetComponent<GameItemDisplay>();
        component.gameItem = this.inventory[index];
        component.gameItem.indexInInventory = index;
        component.gameItem.scripObjRef = this.inventory[index].scripObjRef;
        component.SetIconImg();
      }
      else
        this.invSlots[index].SetActive(false);
    }
    this.inventorySkillbits.Clear();
    for (int index = 0; index < this.invSlotsSkillbit.Count; ++index)
    {
      if (index < this.hIv.InventorySlots_Skillbits.Length)
      {
        this.invSlotsSkillbit[index].SetActive(true);
        this.inventorySkillbits.Add(this.hIv.InventorySlots_Skillbits[index]);
        GameItemDisplay component = (GameItemDisplay) this.invSlotsSkillbit[index].GetComponent<GameItemDisplay>();
        component.gameItem = this.inventorySkillbits[index];
        component.gameItem.indexInInventory = index;
        component.gameItem.scripObjRef = this.inventorySkillbits[index].scripObjRef;
        component.SetIconImg();
      }
      else
        this.invSlotsSkillbit[index].SetActive(false);
    }
    this.inventoryEquipments.Clear();
    for (int index = 0; index < this.invSlotsWeapon.Count; ++index)
    {
      if (index < this.hIv.InventorySlots_Equipment.Length)
      {
        this.invSlotsWeapon[index].SetActive(true);
        this.inventoryEquipments.Add(this.hIv.InventorySlots_Equipment[index]);
        GameItemDisplay component = (GameItemDisplay) this.invSlotsWeapon[index].GetComponent<GameItemDisplay>();
        component.gameItem = this.inventoryEquipments[index];
        component.gameItem.indexInInventory = index;
        component.gameItem.scripObjRef = this.inventoryEquipments[index].scripObjRef;
        component.SetIconImg();
      }
      else
        this.invSlotsWeapon[index].SetActive(false);
    }
  }
}
