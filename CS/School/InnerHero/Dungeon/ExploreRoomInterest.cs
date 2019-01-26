using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExploreRoomInterest : MonoBehaviour, IEventSystemHandler, IPointerClickHandler
{
  private GameObject playerObj;
  private player playerScript;
  private GameObject room;
  private bool flashArrow;

  public ExploreRoomInterest()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    if (!Object.op_Inequality((Object) PlayerSingleton.Instance, (Object) null))
      return;
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.playerScript = (player) this.playerObj.GetComponent<player>();
    this.room = ((Component) ((Component) this).get_gameObject().get_transform().get_parent()).get_gameObject();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (Object.op_Equality((Object) this.playerScript, (Object) null))
    {
      this.playerObj = PlayerSingleton.Instance.getPlayer();
      this.playerScript = (player) this.playerObj.GetComponent<player>();
      this.room = ((Component) ((Component) this).get_gameObject().get_transform().get_parent()).get_gameObject();
    }
    if (Object.op_Inequality((Object) this.playerObj, (Object) null) && ((Animator) this.playerObj.GetComponent<Animator>()).GetBool("push"))
      return;
    this.playerScript.ResetSBtargets();
    if (PlayerSingleton.Instance.ManualMode)
    {
      this.AddRoomManual();
      this.SetPlayerTarget();
      this.playerScript.DisableEnableHitbox();
      this.flashArrow = true;
      this.StartCoroutine(this.Wait(2f));
    }
    else
      this.addRoom();
  }

  [DebuggerHidden]
  private IEnumerator Wait(float waitTime)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new ExploreRoomInterest.\u003CWait\u003Ec__Iterator14()
    {
      waitTime = waitTime,
      \u003C\u0024\u003EwaitTime = waitTime,
      \u003C\u003Ef__this = this
    };
  }

  private void addRoom()
  {
    RoomEntityList component = (RoomEntityList) this.room.GetComponent<RoomEntityList>();
    List<GameObject> events1 = new List<GameObject>();
    using (List<IsAEntityType>.Enumerator enumerator = component.EntitysInRoom.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        IsAEntityType current = enumerator.Current;
        if (this.hasSkillbit(((Component) current).get_gameObject()))
          events1.Add(((Component) current).get_gameObject());
      }
    }
    if (events1.Count > 0)
    {
      int roomIndex = Timeline.instance.FindRoomIndex(this.room);
      if (roomIndex == 0)
        return;
      Timeline.instance.TimeLineEvents.RemoveAt(roomIndex);
      Timeline.instance.TimeLineEvents.Insert(0, new roomData(this.room, events1));
    }
    else
    {
      if (this.TimeLineRoomCheck())
      {
        ((RoomEntityList) this.room.GetComponent<RoomEntityList>()).EntitysInRoom.Clear();
        int roomIndex = Timeline.instance.FindRoomIndex(this.room);
        Timeline.instance.TimeLineEvents.RemoveAt(roomIndex);
      }
      if (Timeline.instance.TimeLineEmptyCheck() || !Timeline.instance.TimeLineEvents[0].eventsInRoom.Contains(component.roomCenter))
      {
        List<GameObject> events2 = new List<GameObject>();
        events2.Add(component.roomCenter);
        Timeline.instance.TimeLineEvents.Insert(0, new roomData(this.room, events2));
      }
      ((Behaviour) component.roomCenter.GetComponent<RoomCenter>()).set_enabled(true);
    }
    this.SetPlayerTarget();
  }

  private void SetPlayerTarget()
  {
    if (!((AIPath) this.playerObj.GetComponent<PlayerPathingAI>()).canMove)
      ((AIPath) this.playerObj.GetComponent<PlayerPathingAI>()).canMove = true;
    this.playerScript.TargetRoom = this.room;
  }

  private void AddRoomManual()
  {
    RoomEntityList component = (RoomEntityList) this.room.GetComponent<RoomEntityList>();
    if (!Timeline.instance.TimeLineEmptyCheck())
    {
      if (!Timeline.instance.TimeLineEvents[0].eventsInRoom.Contains(component.roomCenter))
        Timeline.instance.TimeLineEvents[0].eventsInRoom.Insert(0, component.roomCenter);
    }
    else
    {
      List<GameObject> events = new List<GameObject>();
      events.Add(component.roomCenter);
      Timeline.instance.TimeLineEvents.Insert(0, new roomData(this.room, events));
    }
    ((Behaviour) component.roomCenter.GetComponent<RoomCenter>()).set_enabled(true);
  }

  private bool TimeLineRoomCheck()
  {
    for (int index = 0; index < Timeline.instance.TimeLineEvents.Count; ++index)
    {
      if (Object.op_Equality((Object) Timeline.instance.FirstRoom(), (Object) this.room))
        return true;
    }
    return false;
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
    if (Object.op_Equality((Object) PlayerSingleton.Instance, (Object) null) || !PlayerSingleton.Instance.ManualMode || !Object.op_Implicit((Object) ((Component) this).GetComponent<SpriteRenderer>()))
      return;
    if (this.flashArrow)
      ((SpriteRenderer) ((Component) this).GetComponent<SpriteRenderer>()).set_color(Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0.0f), Mathf.PingPong(Time.get_time() * 2.5f, 0.75f)));
    else
      ((SpriteRenderer) ((Component) this).GetComponent<SpriteRenderer>()).set_color(new Color(1f, 1f, 1f, 1f));
  }
}
