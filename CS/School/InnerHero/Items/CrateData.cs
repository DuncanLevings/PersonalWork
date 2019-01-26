using System.Collections.Generic;
using UnityEngine;

public class CrateData : MonoBehaviour
{
  public int LootAmount;
  public HeroEquipment equips;
  public HeroInventorySO inventory;
  private List<InventoryObject> consumables;
  [HideInInspector]
  public int origAmount;

  public CrateData()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.origAmount = this.LootAmount;
  }

  public int SelectLootType()
  {
    this.consumables = new List<InventoryObject>();
    if ((double) Random.get_value() >= 0.25)
      return -1;
    for (int index = 0; index < this.equips.Consumables.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.equips.Consumables[index].scripObjRef, (Object) null))
        this.consumables.Add(this.equips.Consumables[index].scripObjRef);
    }
    if (this.consumables.Count > 0)
      return Random.Range(0, this.consumables.Count);
    return -1;
  }

  private void Update()
  {
    if (this.TimeLineObjectCheck() <= 0)
      return;
    Timeline.instance.RemoveAllObjectFromTimeline(((Component) this).get_gameObject());
  }

  private int TimeLineObjectCheck()
  {
    for (int index1 = 0; index1 < Timeline.instance.TimeLineEvents.Count; ++index1)
    {
      for (int index2 = 0; index2 < Timeline.instance.TimeLineEvents[index1].eventsInRoom.Count; ++index2)
      {
        if (Object.op_Equality((Object) Timeline.instance.TimeLineEvents[index1].eventsInRoom[index2], (Object) ((Component) this).get_gameObject()))
          return index2;
      }
    }
    return -1;
  }
}
