using UnityEngine;
using UnityEngine.EventSystems;

public class DetectSwipe : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler
{
  public GameObject enemy;

  public DetectSwipe()
  {
    base.\u002Ector();
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (!Input.GetMouseButton(0) || CombatSystem.instance.enemys.Contains(this.enemy))
      return;
    CombatSystem.instance.enemys.Add(this.enemy);
  }
}
