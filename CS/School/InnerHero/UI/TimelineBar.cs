using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineBar : MonoBehaviour
{
  public static TimelineBar Instance;
  public GameObject iconPrefab;
  private List<GameObject> events;
  private LayoutElement[] icons;
  private RectTransform contentTransform;
  private Vector2 ContentStartPos;
  private Vector2 newPos;
  public float xMovement;
  private int iIndexStart;
  private bool move;
  private Animator anim;
  private bool triggered;

  public TimelineBar()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    TimelineBar.Instance = this;
    this.iIndexStart = 0;
    this.icons = (LayoutElement[]) ((Component) this).GetComponentsInChildren<LayoutElement>();
    this.contentTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
    this.ContentStartPos = Vector2.op_Implicit(((Transform) this.contentTransform).get_localPosition());
    this.anim = (Animator) ((Component) ((Component) ((Component) this).get_transform().get_parent()).get_transform().get_parent()).GetComponent<Animator>();
  }

  private void Get5Events()
  {
    this.events.Clear();
    int num = 0;
    for (int index1 = 0; index1 < Timeline.instance.TimeLineEvents.Count; ++index1)
    {
      for (int index2 = 0; index2 < Timeline.instance.TimeLineEvents[index1].eventsInRoom.Count; ++index2)
      {
        if (!Timeline.instance.TimeLineRoomEmptyCheck())
        {
          GameObject gameObject = Timeline.instance.TimeLineEvents[index1].eventsInRoom[index2];
          if (!Object.op_Implicit((Object) gameObject.GetComponent<DoorType>()) && !Object.op_Implicit((Object) gameObject.GetComponent<isaRoomCenter>()) && !this.events.Contains(Timeline.instance.TimeLineEvents[index1].eventsInRoom[index2]))
          {
            this.events.Add(Timeline.instance.TimeLineEvents[index1].eventsInRoom[index2]);
            ++num;
          }
        }
        if (num == 5)
          return;
      }
    }
  }

  public void SetMoveElement()
  {
    this.newPos = new Vector2((float) ((Transform) this.contentTransform).get_localPosition().x - this.xMovement, 0.0f);
    this.iIndexStart = 1;
    this.move = true;
  }

  private void MoveOverOneElement()
  {
    ((Transform) this.contentTransform).set_localPosition(Vector2.op_Implicit(Vector2.Lerp(Vector2.op_Implicit(((Transform) this.contentTransform).get_localPosition()), this.newPos, Time.get_deltaTime() * 1.5f)));
  }

  private void Update()
  {
    if (this.move)
    {
      this.MoveOverOneElement();
      if (!this.triggered)
      {
        this.anim.SetTrigger("next");
        this.triggered = true;
      }
      if (((Transform) this.contentTransform).get_localPosition().x < this.newPos.x + 1.0)
      {
        this.move = false;
        this.triggered = false;
        this.iIndexStart = 0;
        ((Transform) this.contentTransform).set_localPosition(Vector2.op_Implicit(this.ContentStartPos));
      }
    }
    this.Get5Events();
    int index = 0;
    for (int iIndexStart = this.iIndexStart; iIndexStart < 5; ++iIndexStart)
    {
      if (index < this.events.Count)
      {
        ((Image) ((Component) this.icons[iIndexStart]).GetComponent<Image>()).set_sprite(((IsAEntityType) this.events[index].GetComponent<IsAEntityType>()).EntityIcon);
        ++index;
      }
      else
        ((Image) ((Component) this.icons[iIndexStart]).GetComponent<Image>()).set_sprite((Sprite) null);
    }
  }
}
