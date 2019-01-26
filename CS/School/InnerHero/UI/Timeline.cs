using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
  public static Timeline instance;
  public List<roomData> TimeLineEvents;
  public List<string> TimeLineHistory;

  public Timeline()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    Timeline.instance = this;
  }

  public bool TimeLineEmptyCheck()
  {
    return this.TimeLineEvents.Count == 0;
  }

  public bool TimeLineRoomEmptyCheck()
  {
    return this.TimeLineEmptyCheck() || this.TimeLineEvents[0].eventsInRoom.Count == 0;
  }

  public GameObject FirstEvent()
  {
    if (!this.TimeLineEmptyCheck() && !this.TimeLineRoomEmptyCheck())
      return this.TimeLineEvents[0].eventsInRoom[0].get_gameObject();
    return (GameObject) null;
  }

  public GameObject FirstRoom()
  {
    if (!this.TimeLineEmptyCheck())
      return this.TimeLineEvents[0].room;
    return (GameObject) null;
  }

  public int FindRoomIndex(GameObject room)
  {
    for (int index = 0; index < this.TimeLineEvents.Count; ++index)
    {
      if (Object.op_Equality((Object) this.TimeLineEvents[index].room, (Object) room))
        return index;
    }
    return -1;
  }

  public void RemoveFirstEvent()
  {
    this.TimeLineEvents[0].eventsInRoom.RemoveAt(0);
  }

  public void RemoveAllObjectFromTimeline(GameObject obj)
  {
    for (int index1 = 0; index1 < this.TimeLineEvents.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.TimeLineEvents[index1].eventsInRoom.Count; ++index2)
      {
        if (Object.op_Equality((Object) this.TimeLineEvents[index1].eventsInRoom[index2], (Object) obj))
          this.TimeLineEvents[index1].eventsInRoom.RemoveAt(index2);
      }
    }
  }

  private void Update()
  {
    for (int index = this.TimeLineEvents.Count - 1; index > -1; --index)
    {
      if (this.TimeLineEvents[index].eventsInRoom.Count == 0)
        this.TimeLineEvents.RemoveAt(index);
    }
    for (int index1 = this.TimeLineEvents.Count - 1; index1 > -1; --index1)
    {
      for (int index2 = this.TimeLineEvents[index1].eventsInRoom.Count - 1; index2 > -1; --index2)
      {
        if (Object.op_Equality((Object) this.TimeLineEvents[index1].eventsInRoom[index2], (Object) null))
          this.TimeLineEvents[index1].eventsInRoom.RemoveAt(index2);
      }
    }
  }
}
