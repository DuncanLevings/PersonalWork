using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class DamageFeedback : MonoBehaviour
{
  public static DamageFeedback Instance;
  public GameObject PlayerDamageShow;
  public GameObject DamageShow;
  public GameObject Coin;
  public GameObject AddChest;
  public GameObject Unlocked;
  private Transform canvas;
  private Vector3 p;
  private int v;

  public DamageFeedback()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    DamageFeedback.Instance = this;
    this.canvas = GameObject.Find("Canvas").get_transform().FindChild("FeedbackPanel");
    if (!Object.op_Equality((Object) this.canvas, (Object) null))
      return;
    this.canvas = GameObject.Find("Canvas").get_transform();
    GameObject gameObject = new GameObject("FeedbackPanel");
    gameObject.AddComponent<RectTransform>();
    gameObject.get_transform().SetParent(this.canvas);
    gameObject.get_transform().set_localScale(gameObject.get_transform().get_lossyScale());
    RectTransform component = (RectTransform) gameObject.GetComponent<RectTransform>();
    component.set_anchoredPosition(new Vector2(0.5f, 0.5f));
    component.set_anchorMin(Vector2.get_zero());
    component.set_anchorMax(Vector2.get_one());
    component.set_offsetMax(Vector2.get_zero());
    component.set_offsetMin(Vector2.get_zero());
    this.canvas = gameObject.get_transform();
  }

  private void Update()
  {
  }

  public void SpawnDamage(Vector3 pos, Color color, int value = 0)
  {
    GameObject gameObject = this.SpawnObject(this.DamageShow, GetCamera.Instance.camera.WorldToScreenPoint(pos));
    ((DamageValue) gameObject.GetComponent<DamageValue>()).value.set_text(value.ToString());
    ((Graphic) ((DamageValue) gameObject.GetComponent<DamageValue>()).value).set_color(color);
  }

  public void SpawnPlayerDamage(Vector3 pos, Color color, int value = 0)
  {
    GameObject gameObject = this.SpawnObject(this.PlayerDamageShow, GetCamera.Instance.camera.WorldToScreenPoint(pos));
    ((DamageValue) gameObject.GetComponent<DamageValue>()).value.set_text("-" + value.ToString());
    ((Graphic) ((DamageValue) gameObject.GetComponent<DamageValue>()).value).set_color(color);
  }

  public void SpawnPlayerHit(Vector3 pos, Color color, string value = "")
  {
    GameObject gameObject = this.SpawnObject(this.PlayerDamageShow, GetCamera.Instance.camera.WorldToScreenPoint(pos));
    ((DamageValue) gameObject.GetComponent<DamageValue>()).value.set_text(value);
    ((Graphic) ((DamageValue) gameObject.GetComponent<DamageValue>()).value).set_color(color);
  }

  public void SpawnCoin(Vector3 pos, int value = 0)
  {
    ((Text) this.SpawnObject(this.Coin, Camera.get_main().WorldToScreenPoint(pos)).GetComponentInChildren<Text>()).set_text(value.ToString());
  }

  public void SpawnItem(Sprite img, Vector3 pos, int value = 0)
  {
    GameObject gameObject = this.SpawnObject(this.Coin, Camera.get_main().WorldToScreenPoint(pos));
    ((Image) gameObject.GetComponent<Image>()).set_sprite(img);
    ((Text) gameObject.GetComponentInChildren<Text>()).set_text(value.ToString());
  }

  public void SpawnChestAdd(Vector3 pos, int value = 0)
  {
    this.SpawnObject(this.AddChest, Camera.get_main().WorldToScreenPoint(pos));
  }

  public void SpawnUnlock(Vector3 pos, Color color)
  {
    ((Graphic) this.SpawnObject(this.Unlocked, Camera.get_main().WorldToScreenPoint(pos)).GetComponent<Image>()).set_color(color);
  }

  private GameObject SpawnObject(GameObject uiObj, Vector3 screenPos)
  {
    GameObject gameObject = Object.Instantiate((Object) uiObj, screenPos, Quaternion.get_identity()) as GameObject;
    gameObject.get_transform().SetParent(this.canvas);
    gameObject.get_transform().set_localScale(gameObject.get_transform().get_lossyScale());
    return gameObject;
  }

  public void SpawnCoinDelayed(Vector3 pos, int value = 0)
  {
    this.p = pos;
    this.v = value;
    this.StartCoroutine(this.SpawnCoinDelayed());
  }

  [DebuggerHidden]
  public IEnumerator SpawnCoinDelayed()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DamageFeedback.\u003CSpawnCoinDelayed\u003Ec__Iterator13()
    {
      \u003C\u003Ef__this = this
    };
  }
}
