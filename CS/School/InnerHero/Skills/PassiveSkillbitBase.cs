using BehaviourMachine;
using UnityEngine;

[RequireComponent(typeof (isaPassiveSB))]
[RequireComponent(typeof (BehaviourTree))]
public abstract class PassiveSkillbitBase : MonoBehaviour
{
  [HideInInspector]
  public GameObject target;
  public bool Active;
  protected GameObject playerObj;
  protected player playerScript;

  protected PassiveSkillbitBase()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.playerObj = PlayerSingleton.Instance.getPlayer();
    this.playerScript = (player) this.playerObj.GetComponent<player>();
  }

  public abstract void GetEventTarget();

  public abstract void passiveSBChance();

  public abstract bool passiveSBChanceBool();

  public void updatePlayerTimeline()
  {
  }

  private void Update()
  {
  }
}
