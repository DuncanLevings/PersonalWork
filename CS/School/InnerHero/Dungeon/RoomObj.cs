using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomObj
{
  public GameObject room;
  public List<GameObject> AdjacentRooms;
  [HideInInspector]
  public bool visited;

  public RoomObj(GameObject roomObj, List<GameObject> roomsData, bool visit)
  {
    this.room = roomObj;
    this.AdjacentRooms = roomsData;
    this.visited = visit;
  }
}
