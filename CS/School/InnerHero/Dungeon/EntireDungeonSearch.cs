using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntireDungeonSearch : MonoBehaviour
{
  [HideInInspector]
  public List<RoomObj> rooms;
  [HideInInspector]
  public List<GameObject> roomOrder;
  private List<GameObject> tempList;
  private List<GameObject> ImportantRoomList;
  private GameObject targetRoom;
  private GameObject player;

  public EntireDungeonSearch()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    if (!Object.op_Inequality((Object) PlayerSingleton.Instance, (Object) null))
      return;
    this.player = PlayerSingleton.Instance.getPlayer();
    this.tempList = new List<GameObject>();
    this.ImportantRoomList = GlobalFunctions.ConvertToGameobjectList((Component[]) ((Object) this).FindObjectsOfTypeExtended<roomChilds>());
    this.GenerateRoomList();
    this.BFS();
  }

  public void Reset()
  {
    using (List<RoomObj>.Enumerator enumerator = this.rooms.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        RoomObj current = enumerator.Current;
        ((roomChilds) current.room.GetComponent<roomChilds>()).visited = false;
        ((ImportanceOfRoom) current.room.GetComponent<ImportanceOfRoom>()).importance = 0;
        ((ImportanceOfRoom) current.room.GetComponent<ImportanceOfRoom>()).CalcImportanceOfRoom();
      }
    }
    this.roomOrder.Clear();
    this.tempList.Clear();
    this.CreateRoomOrderList();
    Timeline.instance.TimeLineEvents.Clear();
    ((GenerateTimeLine) ((Component) this).GetComponent<GenerateTimeLine>()).Start();
  }

  private void CreateRoomOrderList()
  {
    if (!Object.op_Inequality((Object) this.player, (Object) null))
      return;
    this.rooms.Clear();
    this.GenerateRoomList();
    this.BFS();
  }

  private void GenerateRoomList()
  {
    List<GameObject> gameObjectList1 = new List<GameObject>();
    List<GameObject> gameObjectList2;
    if (Object.op_Equality((Object) ((playerBase) this.player.GetComponent<global::player>()).CurrentRoom, (Object) null))
    {
      gameObjectList2 = ((IEnumerable<GameObject>) ((IEnumerable<GameObject>) GlobalFunctions.ConvertToGameobjectList((Component[]) ((Object) this).FindObjectsOfTypeExtended<roomChilds>())).OrderBy<GameObject, string>((Func<GameObject, string>) (go => ((Object) go).get_name()))).ToList<GameObject>();
      ((playerBase) this.player.GetComponent<global::player>()).CurrentRoom = gameObjectList2[0];
    }
    else
    {
      this.CreateListFromCurrentRoom(((playerBase) this.player.GetComponent<global::player>()).CurrentRoom);
      gameObjectList2 = this.tempList;
    }
    if (gameObjectList2 == null)
      return;
    using (List<GameObject>.Enumerator enumerator = gameObjectList2.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        GameObject current = enumerator.Current;
        this.rooms.Add(new RoomObj(current, ((roomChilds) current.GetComponent<roomChilds>()).AdjacentRooms, ((roomChilds) current.GetComponent<roomChilds>()).visited));
      }
    }
  }

  private void CreateListFromCurrentRoom(GameObject room)
  {
    if (!this.tempList.Contains(room))
      this.tempList.Add(room);
    using (List<GameObject>.Enumerator enumerator1 = ((IEnumerable<GameObject>) this.tempList).ToList<GameObject>().GetEnumerator())
    {
      while (enumerator1.MoveNext())
      {
        GameObject current1 = enumerator1.Current;
        if (Object.op_Inequality((Object) current1, (Object) null))
        {
          using (List<GameObject>.Enumerator enumerator2 = ((roomChilds) current1.GetComponent<roomChilds>()).AdjacentRooms.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              GameObject current2 = enumerator2.Current;
              if (!this.tempList.Contains(current2))
                this.CreateListFromCurrentRoom(current2);
            }
          }
        }
      }
    }
  }

  private void BFS()
  {
    using (List<RoomObj>.Enumerator enumerator = this.rooms.GetEnumerator())
    {
      while (enumerator.MoveNext())
        ((roomChilds) enumerator.Current.room.GetComponent<roomChilds>()).distance = int.MaxValue;
    }
    this.targetRoom = this.getImportantRoomTargetIndex();
    int index = this.getIndex(this.targetRoom);
    Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
    gameObjectQueue.Enqueue(this.rooms[index].room);
    ((roomChilds) this.rooms[index].room.GetComponent<roomChilds>()).distance = 0;
    while (gameObjectQueue.Count > 0)
    {
      GameObject gameObject = gameObjectQueue.Dequeue();
      ((roomChilds) gameObject.GetComponent<roomChilds>()).visited = true;
      if (!Object.op_Equality((Object) gameObject, (Object) ((playerBase) this.player.GetComponent<global::player>()).TargetRoom))
      {
        using (List<GameObject>.Enumerator enumerator = ((roomChilds) gameObject.GetComponent<roomChilds>()).AdjacentRooms.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            GameObject current = enumerator.Current;
            if (((roomChilds) current.GetComponent<roomChilds>()).distance == int.MaxValue)
            {
              ((roomChilds) current.GetComponent<roomChilds>()).distance = ((roomChilds) gameObject.GetComponent<roomChilds>()).distance + 1;
              gameObjectQueue.Enqueue(current);
            }
          }
        }
      }
      else
        break;
    }
    this.TraverseGraph(0);
    if (this.ImportantRoomList.Count <= 0 || this.ImportantRoomList == null)
      return;
    this.ImportantRoomList.RemoveAt(0);
    ((playerBase) this.player.GetComponent<global::player>()).TargetRoom = this.targetRoom;
    this.CreateRoomOrderList();
  }

  private void TraverseGraph(int vertex)
  {
    if (!this.roomOrder.Contains(this.rooms[0].room))
      this.roomOrder.Add(this.rooms[0].room);
    this.calcDistanceOfRooms(this.getIndex(this.rooms[vertex].room));
    using (List<GameObject>.Enumerator enumerator = this.rooms[vertex].AdjacentRooms.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        GameObject current = enumerator.Current;
        if (((ImportanceOfRoom) current.GetComponent<ImportanceOfRoom>()).importance > -1 && !this.roomOrder.Contains(current))
        {
          this.roomOrder.Add(current);
          this.TraverseGraph(this.getIndex(current));
        }
      }
    }
  }

  private GameObject getImportantRoomTargetIndex()
  {
    for (int index = 0; index < this.ImportantRoomList.Count; ++index)
    {
      if (((roomChilds) this.ImportantRoomList[index].GetComponent<roomChilds>()).visited)
        this.ImportantRoomList.RemoveAt(index);
    }
    this.ImportantRoomList.Sort(new Comparison<GameObject>(EntireDungeonSearch.SortByImportance));
    if (this.ImportantRoomList.Count > 0)
      return this.ImportantRoomList[0];
    return (GameObject) null;
  }

  private void calcimportanceOfRooms(int index)
  {
    this.rooms[index].AdjacentRooms.Sort(new Comparison<GameObject>(EntireDungeonSearch.SortByImportance));
  }

  private static int SortByImportance(GameObject r1, GameObject r2)
  {
    return ((ImportanceOfRoom) r2.GetComponent<ImportanceOfRoom>()).importance.CompareTo(((ImportanceOfRoom) r1.GetComponent<ImportanceOfRoom>()).importance);
  }

  private void calcDistanceOfRooms(int index)
  {
    this.rooms[index].AdjacentRooms.Sort(new Comparison<GameObject>(EntireDungeonSearch.SortByDistance));
  }

  private static int SortByDistance(GameObject r1, GameObject r2)
  {
    return ((roomChilds) r1.GetComponent<roomChilds>()).distance.CompareTo(((roomChilds) r2.GetComponent<roomChilds>()).distance);
  }

  private int getIndex(GameObject go)
  {
    for (int index = 0; index < this.rooms.Count; ++index)
    {
      if (Object.op_Equality((Object) this.rooms[index].room, (Object) go))
        return index;
    }
    return 0;
  }

  private void LateUpdate()
  {
  }
}
