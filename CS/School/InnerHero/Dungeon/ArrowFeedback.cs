using UnityEngine;

public class ArrowFeedback : MonoBehaviour
{
  private GameObject playerTargetRoom;
  private ExploreRoomInterest[] arrows;
  private Color flashColor;
  private Color origColor;

  public ArrowFeedback()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.arrows = (ExploreRoomInterest[]) ((Component) this).GetComponentsInChildren<ExploreRoomInterest>();
    this.origColor = new Color(1f, 1f, 1f, 1f);
    this.flashColor = new Color(1f, 1f, 1f, 0.0f);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) PlayerSingleton.Instance, (Object) null) || PlayerSingleton.Instance.ManualMode)
      return;
    this.playerTargetRoom = ((playerBase) PlayerSingleton.Instance.getPlayer().GetComponent<player>()).TargetRoom;
    if (Object.op_Equality((Object) this.playerTargetRoom, (Object) ((Component) this).get_gameObject()))
    {
      for (int index = 0; index < this.arrows.Length; ++index)
      {
        if (Object.op_Implicit((Object) ((Component) this.arrows[index]).GetComponent<SpriteRenderer>()))
          ((SpriteRenderer) ((Component) this.arrows[index]).GetComponent<SpriteRenderer>()).set_color(Color.Lerp(this.origColor, this.flashColor, Mathf.PingPong(Time.get_time() * 2.5f, 0.75f)));
      }
    }
    else
    {
      for (int index = 0; index < this.arrows.Length; ++index)
      {
        if (Object.op_Implicit((Object) ((Component) this.arrows[index]).GetComponent<SpriteRenderer>()))
          ((SpriteRenderer) ((Component) this.arrows[index]).GetComponent<SpriteRenderer>()).set_color(this.origColor);
      }
    }
  }
}
