using UnityEngine;

public abstract class ActiveSkillbitBase : MonoBehaviour
{
  public bool Active;
  public float UseCooldown;
  [HideInInspector]
  public GameObject target;
  [HideInInspector]
  public GameObject playerObj;
  protected Animator anim;
  protected SearchForObjectSB[] searchSBList;

  protected ActiveSkillbitBase()
  {
    base.\u002Ector();
  }

  public abstract void getEventTarget();

  public abstract void UseSkillbit();

  protected void consumeEvent()
  {
    if (!Object.op_Implicit((Object) this.target.GetComponent<DoorType>()))
      TimelineBar.Instance.SetMoveElement();
    string name = ((Object) this.target).get_name();
    Timeline.instance.TimeLineHistory.Add(name);
    Object.Destroy((Object) this.target);
    this.target = (GameObject) null;
    Timeline.instance.RemoveFirstEvent();
  }

  protected void consumeEventNoDestroy()
  {
    if (!Object.op_Implicit((Object) this.target.GetComponent<DoorType>()))
      TimelineBar.Instance.SetMoveElement();
    string name = ((Object) this.target).get_name();
    Timeline.instance.TimeLineHistory.Add(name);
    if (!Timeline.instance.TimeLineEmptyCheck() && Timeline.instance.TimeLineEvents[0].eventsInRoom.Contains(this.target) && Object.op_Equality((Object) Timeline.instance.FirstEvent(), (Object) this.target))
      Timeline.instance.RemoveFirstEvent();
    ((IsAEntityType) this.target.GetComponent<IsAEntityType>()).type = IsAEntityType.Type.Undefined;
    this.target = (GameObject) null;
  }

  protected SearchForObjectSB getEventType(SearchForObjectSB.SearchType type)
  {
    for (int index = 0; index < this.searchSBList.Length; ++index)
    {
      if (this.searchSBList[index].type == type)
        return this.searchSBList[index];
    }
    return (SearchForObjectSB) null;
  }
}
