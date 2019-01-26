using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PowerBarInstance : MonoBehaviour
{
  public static PowerBarInstance Instance;
  [HideInInspector]
  public GameObject targetPos;
  private GameObject playerObj;
  [HideInInspector]
  public Image powerBarTop;
  [HideInInspector]
  public Image powerBarBottom;
  [HideInInspector]
  public Image powerBarButton;
  [HideInInspector]
  public Image powerBarButtonIcon;
  [HideInInspector]
  public Image powerBarTimer;
  private Color powerBarTopOrigColor;
  private Image[] powerBars;

  public PowerBarInstance()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    PowerBarInstance.Instance = this;
    this.powerBars = (Image[]) ((Component) this).GetComponentsInChildren<Image>();
    for (int index = 0; index < this.powerBars.Length; ++index)
    {
      if (Object.op_Implicit((Object) ((Component) this.powerBars[index]).GetComponent<isPowerBarBottom>()))
        this.powerBarBottom = this.powerBars[index];
      if (Object.op_Implicit((Object) ((Component) this.powerBars[index]).GetComponent<isPowerBarTop>()))
        this.powerBarTop = this.powerBars[index];
      if (Object.op_Implicit((Object) ((Component) this.powerBars[index]).get_gameObject().GetComponent<Button>()))
        this.powerBarButton = this.powerBars[index];
      if (Object.op_Implicit((Object) ((Component) this.powerBars[index]).GetComponent<isPowerBarButtonIcon>()))
        this.powerBarButtonIcon = this.powerBars[index];
      if (Object.op_Implicit((Object) ((Component) this.powerBars[index]).GetComponent<PowerBarTimer>()))
        this.powerBarTimer = this.powerBars[index];
    }
    this.powerBarTopOrigColor = ((Graphic) this.powerBarTop).get_color();
    this.SetActiveOfPowerBars(false);
  }

  public void EnablePowerBar(
    GameObject target,
    GameObject positionTarget,
    bool val,
    Sprite icon,
    Color btnColor)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PowerBarInstance.\u003CEnablePowerBar\u003Ec__AnonStorey49 barCAnonStorey49 = new PowerBarInstance.\u003CEnablePowerBar\u003Ec__AnonStorey49();
    // ISSUE: reference to a compiler-generated field
    barCAnonStorey49.target = target;
    ((UnityEventBase) ((Button) ((Component) this.powerBarButton).GetComponent<Button>()).get_onClick()).RemoveAllListeners();
    this.placement(positionTarget);
    this.SetActiveOfPowerBars(val);
    if (val)
    {
      // ISSUE: method pointer
      ((UnityEvent) ((Button) ((Component) this.powerBarButton).GetComponent<Button>()).get_onClick()).AddListener(new UnityAction((object) barCAnonStorey49, __methodptr(\u003C\u003Em__2E)));
    }
    else
      ((UnityEventBase) ((Button) ((Component) this.powerBarButton).GetComponent<Button>()).get_onClick()).RemoveAllListeners();
    ((Graphic) this.powerBarButton).set_color(btnColor);
    this.powerBarButtonIcon.set_sprite(icon);
    ((Graphic) this.powerBarButtonIcon).set_color(Color.get_white());
    ((Graphic) this.powerBarTop).set_color(new Color((float) btnColor.r, (float) btnColor.g, (float) btnColor.b, (float) this.powerBarTopOrigColor.a));
  }

  private void SetActiveOfPowerBars(bool val)
  {
    for (int index = 0; index < this.powerBars.Length; ++index)
      ((Component) this.powerBars[index]).get_gameObject().SetActive(val);
  }

  public void DisableAllPowerBar()
  {
    this.SetActiveOfPowerBars(false);
  }

  public void removeAnyScripts(Image obj)
  {
    foreach (Component component in ((Component) obj).GetComponents(typeof (Component)))
    {
      if (((Object) component).GetInstanceID() != ((Object) component.GetComponent<RectTransform>()).GetInstanceID() && ((Object) component).GetInstanceID() != ((Object) component.GetComponent<CanvasRenderer>()).GetInstanceID() && (((Object) component).GetInstanceID() != ((Object) component.GetComponent<Image>()).GetInstanceID() && ((Object) component).GetInstanceID() != ((Object) component.GetComponent<Button>()).GetInstanceID()))
        Object.Destroy((Object) component);
    }
  }

  private void placement(GameObject position)
  {
    if (!Object.op_Implicit((Object) GetCamera.Instance))
      return;
    ((Component) this).get_transform().set_position(GetCamera.Instance.camera.WorldToScreenPoint(position.get_transform().get_position()));
  }

  private void Update()
  {
  }
}
