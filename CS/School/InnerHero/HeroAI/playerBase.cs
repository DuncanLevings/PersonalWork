using BehaviourMachine;
using System.Collections.Generic;
using UnityEngine;

public abstract class playerBase : MonoBehaviour
{
  [HideInInspector]
  public List<BehaviourTree> ActiveSkillBitTrees;
  [HideInInspector]
  public List<BehaviourTree> PassiveSkillBitTrees;
  [HideInInspector]
  [Header("Skillbit Type Lists")]
  public List<GameObject> enemySkillbits;
  [HideInInspector]
  public List<GameObject> doorSkillbits;
  [HideInInspector]
  public List<GameObject> scavengeSkillbits;
  [HideInInspector]
  public List<GameObject> chestSkillbits;
  [Header("Target and Room Data")]
  [HideInInspector]
  public GameObject target;
  [HideInInspector]
  public GameObject TargetRoom;
  [HideInInspector]
  public GameObject CurrentRoom;

  protected playerBase()
  {
    base.\u002Ector();
  }

  protected void getTrees()
  {
    this.ActiveSkillBitTrees.Clear();
    this.PassiveSkillBitTrees.Clear();
    foreach (Transform componentsInChild in (Transform[]) ((Component) ((Component) this).get_transform()).GetComponentsInChildren<Transform>())
    {
      if (((Component) componentsInChild).get_gameObject().get_activeSelf())
      {
        if (Object.op_Implicit((Object) ((Component) componentsInChild).get_gameObject().GetComponent<isaActiveSB>()))
        {
          if (((ActiveSkillbitBase) ((Component) componentsInChild).GetComponent<ActiveSkillbitBase>()).Active)
            this.ActiveSkillBitTrees.Add((BehaviourTree) ((Component) componentsInChild).GetComponent<BehaviourTree>());
          else
            ((InternalStateBehaviour) ((Component) componentsInChild).GetComponent<BehaviourTree>()).enabled = false;
        }
        else if (Object.op_Implicit((Object) ((Component) componentsInChild).get_gameObject().GetComponent<isaPassiveSB>()))
        {
          if (((PassiveSkillbitBase) ((Component) componentsInChild).GetComponent<PassiveSkillbitBase>()).Active)
            this.PassiveSkillBitTrees.Add((BehaviourTree) ((Component) componentsInChild).GetComponent<BehaviourTree>());
          else
            ((InternalStateBehaviour) ((Component) componentsInChild).GetComponent<BehaviourTree>()).enabled = false;
        }
      }
    }
  }

  public void GenerateTypeLists()
  {
    this.enemySkillbits.Clear();
    this.doorSkillbits.Clear();
    this.scavengeSkillbits.Clear();
    this.chestSkillbits.Clear();
    for (int index = 0; index < this.ActiveSkillBitTrees.Count; ++index)
    {
      switch (((isaActivesbType) ((Component) this.ActiveSkillBitTrees[index]).GetComponent<isaActivesbType>()).type)
      {
        case isaActivesbType.Type.enemy:
          this.enemySkillbits.Add(((Component) this.ActiveSkillBitTrees[index]).get_gameObject());
          break;
        case isaActivesbType.Type.chest:
          this.chestSkillbits.Add(((Component) this.ActiveSkillBitTrees[index]).get_gameObject());
          break;
        case isaActivesbType.Type.scavenge:
          this.scavengeSkillbits.Add(((Component) this.ActiveSkillBitTrees[index]).get_gameObject());
          break;
        case isaActivesbType.Type.door:
          this.doorSkillbits.Add(((Component) this.ActiveSkillBitTrees[index]).get_gameObject());
          break;
      }
    }
  }

  public void enableOnlyDefaultSB()
  {
    for (int index = 0; index < this.enemySkillbits.Count; ++index)
    {
      if (Object.op_Implicit((Object) this.enemySkillbits[index].GetComponent<isDefaultSB>()))
        ((ActiveSkillbitBase) this.enemySkillbits[index].GetComponent<ActiveSkillbitBase>()).Active = true;
      else
        ((ActiveSkillbitBase) this.enemySkillbits[index].GetComponent<ActiveSkillbitBase>()).Active = false;
    }
  }

  public void enableSelectedSB(GameObject selected, isaActivesbType.Type type)
  {
    switch (type)
    {
      case isaActivesbType.Type.enemy:
        for (int index = 0; index < this.enemySkillbits.Count; ++index)
        {
          if (Object.op_Equality((Object) this.enemySkillbits[index], (Object) selected))
            ((ActiveSkillbitBase) this.enemySkillbits[index].GetComponent<ActiveSkillbitBase>()).Active = true;
          else
            ((ActiveSkillbitBase) this.enemySkillbits[index].GetComponent<ActiveSkillbitBase>()).Active = false;
        }
        break;
    }
  }

  public void UpdateActiveSkillbitTrees()
  {
    for (int index = 0; index < this.ActiveSkillBitTrees.Count; ++index)
      this.ActiveSkillBitTrees[index].enabled = true;
  }

  public void UpdatePassiveSkillbitTrees()
  {
    for (int index = 0; index < this.PassiveSkillBitTrees.Count; ++index)
      this.PassiveSkillBitTrees[index].enabled = true;
  }

  protected void getTarget()
  {
    for (int index = 0; index < this.ActiveSkillBitTrees.Count; ++index)
    {
      ActiveSkillbitBase component = (ActiveSkillbitBase) ((Component) this.ActiveSkillBitTrees[index]).get_gameObject().GetComponent<ActiveSkillbitBase>();
      if (Object.op_Inequality((Object) component.target, (Object) null))
      {
        this.target = component.target;
        break;
      }
      this.target = (GameObject) null;
    }
  }

  public void ResetSBtargets()
  {
    using (List<BehaviourTree>.Enumerator enumerator = this.ActiveSkillBitTrees.GetEnumerator())
    {
      while (enumerator.MoveNext())
        ((ActiveSkillbitBase) ((Component) enumerator.Current).get_gameObject().GetComponent<ActiveSkillbitBase>()).target = (GameObject) null;
    }
    using (List<BehaviourTree>.Enumerator enumerator = this.PassiveSkillBitTrees.GetEnumerator())
    {
      while (enumerator.MoveNext())
        ((PassiveSkillbitBase) ((Component) enumerator.Current).get_gameObject().GetComponent<PassiveSkillbitBase>()).target = (GameObject) null;
    }
  }

  public void FullResetSBTargets()
  {
    using (List<BehaviourTree>.Enumerator enumerator = this.ActiveSkillBitTrees.GetEnumerator())
    {
      while (enumerator.MoveNext())
        ((ActiveSkillbitBase) ((Component) enumerator.Current).get_gameObject().GetComponent<ActiveSkillbitBase>()).target = (GameObject) null;
    }
    using (List<BehaviourTree>.Enumerator enumerator = this.PassiveSkillBitTrees.GetEnumerator())
    {
      while (enumerator.MoveNext())
        ((PassiveSkillbitBase) ((Component) enumerator.Current).get_gameObject().GetComponent<PassiveSkillbitBase>()).target = (GameObject) null;
    }
    foreach (RoomCenter roomCenter in (RoomCenter[]) Object.FindObjectsOfType<RoomCenter>())
      roomCenter.removeSelfFromTimeline();
    if (!Object.op_Inequality((Object) PowerBarInstance.Instance, (Object) null))
      return;
    PowerBarInstance.Instance.DisableAllPowerBar();
  }
}
