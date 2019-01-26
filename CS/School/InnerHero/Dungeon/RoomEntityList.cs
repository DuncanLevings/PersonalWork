using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomEntityList : MonoBehaviour
{
  public GameObject roomCenter;
  public List<IsAEntityType> EntitysInRoom;

  public RoomEntityList()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    this.roomCenter = ((Component) ((Component) this).GetComponentInChildren<isaRoomCenter>()).get_gameObject();
    this.EntitysInRoom = ((IEnumerable<IsAEntityType>) ((Component) this).GetComponentsInChildren<IsAEntityType>()).ToList<IsAEntityType>();
    this.organizeEntityList();
  }

  private void organizeEntityList()
  {
    List<IsAEntityType> list1 = new List<IsAEntityType>();
    List<IsAEntityType> list2 = new List<IsAEntityType>();
    using (List<IsAEntityType>.Enumerator enumerator = this.EntitysInRoom.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        IsAEntityType current = enumerator.Current;
        switch (((IsAEntityType) ((Component) current).GetComponent<IsAEntityType>()).type)
        {
          case IsAEntityType.Type.enemy:
            list1.Add(current);
            continue;
          case IsAEntityType.Type.chest:
            list2.Add(current);
            continue;
          default:
            continue;
        }
      }
    }
    this.EntitysInRoom.Clear();
    this.addToList(list1);
    this.addToList(list2);
  }

  private void addToList(List<IsAEntityType> list)
  {
    using (List<IsAEntityType>.Enumerator enumerator = list.GetEnumerator())
    {
      while (enumerator.MoveNext())
        this.EntitysInRoom.Add(enumerator.Current);
    }
  }

  private void Update()
  {
    for (int index = this.EntitysInRoom.Count - 1; index > -1; --index)
    {
      if (Object.op_Equality((Object) this.EntitysInRoom[index], (Object) null))
        this.EntitysInRoom.RemoveAt(index);
    }
  }
}
