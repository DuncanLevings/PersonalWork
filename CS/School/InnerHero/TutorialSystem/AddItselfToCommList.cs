using System.Collections;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof (CommunicationHolder))]
public class AddItselfToCommList : TutorialSystem
{
  private TutorialSystem tutEventManager;
  public string systemType;
  private bool added;
  private System.Type t;
  private bool searched;

  private void Start()
  {
    this.t = System.Type.GetType(this.systemType);
    this.tutEventManager = (TutorialSystem) Object.FindObjectOfType(this.t);
    this.StartCoroutine(this.search());
  }

  [DebuggerHidden]
  private IEnumerator search()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AddItselfToCommList.\u003Csearch\u003Ec__Iterator23()
    {
      \u003C\u003Ef__this = this
    };
  }
}
