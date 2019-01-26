using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonAdjacencySearch : MonoBehaviour
{
  [HideInInspector]
  public List<RoomObj> rooms;
  [HideInInspector]
  public List<GameObject> roomOrder;
  private List<GameObject> tempList;

  public DungeonAdjacencySearch()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.tempList = new List<GameObject>();
    this.GenerateRoomList();
    this.DFS(0);
  }

  private void CreateRoomOrderList()
  {
    this.ClearVisited();
    this.rooms.Clear();
    this.roomOrder.Clear();
    this.GenerateRoomList();
    this.DFS(0);
  }

  private void GenerateRoomList()
  {
    GameObject player = PlayerSingleton.Instance.getPlayer();
    List<GameObject> gameObjectList1 = new List<GameObject>();
    List<GameObject> gameObjectList2;
    if (Object.op_Equality((Object) ((playerBase) player.GetComponent<player>()).TargetRoom, (Object) null))
    {
      gameObjectList2 = ((IEnumerable<GameObject>) ((IEnumerable<GameObject>) GlobalFunctions.ConvertToGameobjectList((Component[]) ((Object) this).FindObjectsOfTypeExtended<roomChilds>())).OrderBy<GameObject, string>((Func<GameObject, string>) (go => ((Object) go).get_name()))).ToList<GameObject>();
    }
    else
    {
      this.CreateListFromCurrentRoom(((playerBase) player.GetComponent<player>()).TargetRoom);
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

  private void DFS(int vertex)
  {
    ((roomChilds) this.rooms[vertex].room.GetComponent<roomChilds>()).visited = true;
    if (vertex == 0)
      this.roomOrder.Add(this.rooms[vertex].room);
    this.calcimportanceOfRooms(vertex);
    using (List<GameObject>.Enumerator enumerator = this.rooms[vertex].AdjacentRooms.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        GameObject current = enumerator.Current;
        if (Object.op_Inequality((Object) current, (Object) null))
        {
          if (!((roomChilds) current.GetComponent<roomChilds>()).visited)
          {
            if (((ImportanceOfRoom) current.GetComponent<ImportanceOfRoom>()).importance > 0 && this.getIndex(current) != 0)
            {
              this.roomOrder.Add(current);
              this.DFS(this.getIndex(current));
            }
          }
          else if (this.getUnvisited() != 0)
            this.DFS(this.getUnvisited());
        }
      }
    }
  }

  private void ClearVisited()
  {
    using (List<RoomObj>.Enumerator enumerator = this.rooms.GetEnumerator())
    {
      while (enumerator.MoveNext())
        ((roomChilds) enumerator.Current.room.GetComponent<roomChilds>()).visited = false;
    }
  }

  private void calcimportanceOfRooms(int index)
  {
    this.rooms[index].AdjacentRooms.Sort(new Comparison<GameObject>(DungeonAdjacencySearch.SortByImportance));
  }

  private static int SortByImportance(GameObject r1, GameObject r2)
  {
    return ((ImportanceOfRoom) r2.GetComponent<ImportanceOfRoom>()).importance.CompareTo(((ImportanceOfRoom) r1.GetComponent<ImportanceOfRoom>()).importance);
  }

  private int getUnvisited()
  {
    for (int index = 0; index < this.rooms.Count; ++index)
    {
      if (!this.rooms[index].visited)
        return index;
    }
    return 0;
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

  private void Update()
  {
  }
}
