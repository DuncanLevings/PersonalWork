using UnityEngine;
using UnityEngine.EventSystems;

public class InteractWithEntity : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
  private GameObject skillbit;
  private GameObject playerObj;
  private player playerScript;

  public InteractWithEntity()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.playerScript = (player) this.playerObj.GetComponent<player>();
    this.getSkillbit();
  }

  private void getSkillbit()
  {
    switch (((IsAEntityType) ((Component) this).GetComponent<IsAEntityType>()).type)
    {
      case IsAEntityType.Type.chest:
        if (this.playerScript.chestSkillbits.Count <= 0)
          break;
        this.skillbit = this.playerScript.chestSkillbits[0].get_gameObject();
        break;
      case IsAEntityType.Type.crate:
        if (this.playerScript.scavengeSkillbits.Count <= 0)
          break;
        this.skillbit = this.playerScript.scavengeSkillbits[0].get_gameObject();
        break;
    }
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    if (!Object.op_Inequality((Object) this.skillbit, (Object) null))
      return;
    ((ActivateSelf) this.skillbit.GetComponent<ActivateSelf>()).ActivateTree(((Component) this).get_gameObject());
  }
}
