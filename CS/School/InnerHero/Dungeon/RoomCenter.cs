using UnityEngine;

[RequireComponent(typeof (IsAEntityType))]
public class RoomCenter : MonoBehaviour
{
  public float TimeToWait;
  private float origTime;
  private GameObject playerObj;
  private player playerScript;
  private GameObject firstEntity;

  public RoomCenter()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    this.origTime = this.TimeToWait;
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.playerScript = (player) this.playerObj.GetComponent<player>();
  }

  private void Update()
  {
    this.TimeToWait -= Time.get_deltaTime();
    if ((double) this.TimeToWait <= 0.0)
    {
      this.TimeToWait = this.origTime;
      this.removeSelfFromTimeline();
      ((Behaviour) this).set_enabled(false);
    }
    if (!Timeline.instance.TimeLineRoomEmptyCheck())
      this.firstEntity = Timeline.instance.FirstEvent();
    if (Object.op_Implicit((Object) this.firstEntity.GetComponent<DoorType>()) || !Object.op_Inequality((Object) this.firstEntity, (Object) ((Component) this).get_gameObject()))
      return;
    this.TimeToWait = this.origTime;
    this.removeSelfFromTimeline();
    ((Behaviour) this).set_enabled(false);
  }

  public void removeSelfFromTimeline()
  {
    Timeline.instance.RemoveAllObjectFromTimeline(((Component) this).get_gameObject());
  }
}
