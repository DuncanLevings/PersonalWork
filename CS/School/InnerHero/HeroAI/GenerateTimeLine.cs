using System.Collections.Generic;
using UnityEngine;

public class GenerateTimeLine : MonoBehaviour
{
  private GameObject playerObj;
  private player playerScript;
  private List<GameObject> roomOrder;

  public GenerateTimeLine()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    if (!Object.op_Inequality((Object) PlayerSingleton.Instance, (Object) null))
      return;
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.playerScript = (player) this.playerObj.GetComponent<player>();
    this.setUpTimeLine();
  }

  private void setUpTimeLine()
  {
    this.roomOrder = !((Behaviour) ((Component) this).GetComponent<DungeonAdjacencySearch>()).get_enabled() ? ((EntireDungeonSearch) ((Component) this).GetComponent<EntireDungeonSearch>()).roomOrder : ((DungeonAdjacencySearch) ((Component) this).GetComponent<DungeonAdjacencySearch>()).roomOrder;
    this.CreateTimeLine();
  }

  private void CreateTimeLine()
  {
    using (List<GameObject>.Enumerator enumerator1 = this.roomOrder.GetEnumerator())
    {
      while (enumerator1.MoveNext())
      {
        GameObject current1 = enumerator1.Current;
        RoomEntityList component = (RoomEntityList) current1.GetComponent<RoomEntityList>();
        if (component.EntitysInRoom.Count > 0)
        {
          List<GameObject> events = new List<GameObject>();
          using (List<IsAEntityType>.Enumerator enumerator2 = component.EntitysInRoom.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              IsAEntityType current2 = enumerator2.Current;
              if (this.hasSkillbit(((Component) current2).get_gameObject()))
                events.Add(((Component) current2).get_gameObject());
            }
          }
          Timeline.instance.TimeLineEvents.Add(new roomData(current1, events));
        }
      }
    }
  }

  private bool hasSkillbit(GameObject obj)
  {
    switch (((IsAEntityType) obj.GetComponent<IsAEntityType>()).type)
    {
      case IsAEntityType.Type.enemy:
        return PlayerSingleton.Instance.canAttackEnemy;
      case IsAEntityType.Type.chest:
        return PlayerSingleton.Instance.canOpenChest;
      default:
        return false;
    }
  }

  private void Update()
  {
  }
}
