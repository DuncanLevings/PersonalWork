using BehaviourMachine;
using System.Collections.Generic;
using UnityEngine;

public class SearchForObjectSB : PassiveSkillbitBase
{
  public SearchForObjectSB.SearchType type;

  public override void GetEventTarget()
  {
    using (List<roomData>.Enumerator enumerator = Timeline.instance.TimeLineEvents.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        roomData current = enumerator.Current;
        if (Object.op_Equality((Object) current.room, (Object) this.playerScript.TargetRoom) && current.eventsInRoom.Count > 0 && (Object.op_Inequality((Object) current.eventsInRoom[0], (Object) null) && ((IsAEntityType) current.eventsInRoom[0].GetComponent<IsAEntityType>()).type == (IsAEntityType.Type) this.type))
        {
          if (PlayerSingleton.Instance.ManualMode)
          {
            if (this.type != SearchForObjectSB.SearchType.door)
              break;
            this.target = current.eventsInRoom[0];
            break;
          }
          this.target = current.eventsInRoom[0];
        }
      }
    }
  }

  private void Update()
  {
    if (Object.op_Equality((Object) ((Component) this).get_transform().get_parent(), (Object) null))
      this.Active = false;
    if (!this.Active)
    {
      this.target = (GameObject) null;
      ((InternalStateBehaviour) ((Component) this).GetComponent<BehaviourTree>()).enabled = false;
    }
    if (!Object.op_Inequality((Object) this.target, (Object) null) || ((IsAEntityType) this.target.GetComponent<IsAEntityType>()).type != IsAEntityType.Type.Undefined)
      return;
    this.target = (GameObject) null;
  }

  public override void passiveSBChance()
  {
  }

  public override bool passiveSBChanceBool()
  {
    return false;
  }

  public enum SearchType
  {
    Undefined,
    door,
    enemy,
    chest,
    crate,
  }
}
