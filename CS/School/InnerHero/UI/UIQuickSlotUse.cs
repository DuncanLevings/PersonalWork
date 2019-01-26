using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQuickSlotUse : UISlotInteractable, IUsable
{
  public HeroInventorySO inventory;
  public GameItem_Consumable_OnDungeon item;
  public Image Display;
  public GameObject ParticleEffect;
  public GameObject OnPlayerEffect;
  public GameObject spawnedItem;
  public InventoryObject scriptObjRef;
  private UICounterHolder holder;
  private bool allSet;
  public bool durationType;
  public Image cooldownImg;
  public Text cooldownText;
  public float cooldown;
  public float duration;
  public GameObject itemUseText;
  private float timer;
  private bool used;

  private new void Start()
  {
    base.Start();
    this.GetImage();
    ((Graphic) this.image).set_color(Color.get_white());
  }

  private void Update()
  {
    if (this.item == null)
      return;
    if (Object.op_Equality((Object) this.Display, (Object) null))
      this.GetImage();
    else if (!this.allSet)
    {
      ((Component) this.Display).get_gameObject().SetActive(true);
      this.Display.set_sprite(this.item.icon);
      this.holder = (UICounterHolder) ((Component) this).GetComponentInChildren<UICounterHolder>();
      this.holder.iv = this.inventory;
      this.holder.gameItem = (GameItem) this.item;
      this.holder.gameItem.indexInInventory = this.item.indexInInventory;
      this.allSet = true;
    }
    if (!this.used)
      return;
    this.timer -= Time.get_deltaTime();
    if (this.durationType)
    {
      this.cooldownImg.set_fillAmount(Mathf.Clamp(this.timer / this.duration, 0.0f, 1f));
      if ((double) this.timer > 0.0)
        return;
      this.cooldownImg.set_fillAmount(0.0f);
      this.used = false;
    }
    else
    {
      this.cooldownText.set_text(string.Empty + (object) (int) this.timer);
      this.cooldownImg.set_fillAmount(Mathf.Clamp(this.timer / this.cooldown, 0.0f, 1f));
      if ((double) this.timer > 0.0)
        return;
      this.cooldownText.set_text(string.Empty);
      this.cooldownImg.set_fillAmount(0.0f);
      this.used = false;
    }
  }

  public void AppearItem()
  {
    this.GetImage();
    ((Component) this.Display).get_gameObject().SetActive(true);
    this.Display.set_sprite(this.item.icon);
    this.holder = (UICounterHolder) ((Component) this).GetComponentInChildren<UICounterHolder>();
    this.holder.iv = this.inventory;
    this.holder.gameItem = (GameItem) this.item;
    this.holder.gameItem.indexInInventory = this.item.indexInInventory;
    this.allSet = true;
  }

  public void Use()
  {
  }

  public void GetImage()
  {
    IEnumerator enumerator = ((Component) this).get_transform().GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        Image component = (Image) ((Component) enumerator.Current).GetComponent<Image>();
        if (Object.op_Inequality((Object) component, (Object) null))
          this.Display = component;
      }
    }
    finally
    {
      (enumerator as IDisposable)?.Dispose();
    }
  }

  public override void OnPointerDown(PointerEventData evData)
  {
    if (this.item == null || this.inventory.InventorySlots[this.item.indexInInventory].amount < 1 || this.used)
      return;
    --this.inventory.InventorySlots[this.item.indexInInventory].amount;
    ((ItemUseBase) this.spawnedItem.GetComponent<ItemUseBase>()).UseItem();
    if (Object.op_Inequality((Object) this.itemUseText, (Object) null))
    {
      this.itemUseText.SetActive(false);
      this.itemUseText.SetActive(true);
      ((Text) this.itemUseText.GetComponent<Text>()).set_text(((ItemUseBase) this.spawnedItem.GetComponent<ItemUseBase>()).ItemFeedbackText);
    }
    ((AudioSource) ((Component) this).GetComponentInChildren<AudioSource>()).Play();
    if (this.durationType)
    {
      this.timer = this.duration;
      this.cooldownImg.set_fillMethod((Image.FillMethod) 1);
      this.used = true;
    }
    else
    {
      this.timer = this.cooldown;
      this.cooldownImg.set_fillMethod((Image.FillMethod) 4);
      this.used = true;
    }
    if (!Object.op_Equality((Object) this.inventory.InventorySlots[this.item.indexInInventory].scripObjRef, (Object) this.scriptObjRef))
      ;
    if (this.inventory.InventorySlots[this.item.indexInInventory].amount < 1)
      ;
  }

  private void ItemUseFeedback()
  {
    if (Object.op_Inequality((Object) this.ParticleEffect, (Object) null))
    {
      GameObject gameObject = (GameObject) Object.Instantiate((Object) this.ParticleEffect, ((Component) this).get_transform().get_position(), Quaternion.get_identity());
      gameObject.get_transform().set_parent(((Component) this.canvasRef).get_transform());
      gameObject.get_transform().set_parent(this.canvasRef);
    }
    if (!Object.op_Inequality((Object) this.OnPlayerEffect, (Object) null))
      return;
    GameObject gameObject1 = (GameObject) Object.Instantiate((Object) this.OnPlayerEffect, Vector3.get_zero(), Quaternion.get_identity());
    gameObject1.get_transform().set_parent(((Component) this.canvasRef).get_transform());
    gameObject1.get_transform().set_position(Camera.get_main().WorldToScreenPoint(((Component) PlayerSingleton.Instance).get_transform().get_position()));
  }

  public bool HasItem()
  {
    return this.item != null;
  }
}
