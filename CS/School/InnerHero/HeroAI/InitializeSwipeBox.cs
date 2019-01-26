using UnityEngine;

public class InitializeSwipeBox : MonoBehaviour
{
  public GameObject swipeBox;
  private GameObject parent;
  private Vector3 position;
  [HideInInspector]
  public GameObject go;

  public InitializeSwipeBox()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    this.parent = ((Component) Object.FindObjectOfType<isSwipeBoxParent>()).get_gameObject();
    if (!Object.op_Equality((Object) this.go, (Object) null))
      return;
    this.go = (GameObject) Object.Instantiate<GameObject>((M0) this.swipeBox);
    this.go.get_transform().SetParent(this.parent.get_transform());
    ((Transform) this.go.GetComponent<RectTransform>()).set_localScale(Vector3.get_one());
    ((DetectSwipe) this.go.GetComponent<DetectSwipe>()).enemy = ((Component) this).get_gameObject();
  }

  private void Update()
  {
    if (!Object.op_Inequality((Object) this.go, (Object) null) || !Object.op_Inequality((Object) GetCamera.Instance, (Object) null))
      return;
    this.position = GetCamera.Instance.camera.WorldToScreenPoint(((Component) this).get_transform().get_position());
    this.position.z = (__Null) 0.0;
    this.go.get_transform().set_position(this.position);
  }
}
