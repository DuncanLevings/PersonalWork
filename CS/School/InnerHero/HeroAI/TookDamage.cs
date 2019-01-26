using UnityEngine;
using UnityEngine.UI;

public class TookDamage : MonoBehaviour
{
  private Color origColor;
  private GameObject player;
  private bool revert;
  public Image healthBar;
  private Color origHPcolor;
  private isaPlayerHitProc[] hitProcs;

  public TookDamage()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    if (Object.op_Inequality((Object) PlayerSingleton.Instance, (Object) null))
    {
      this.player = PlayerSingleton.Instance.getPlayer();
      this.SetHitProcs();
    }
    this.origHPcolor = ((Graphic) this.healthBar).get_color();
    this.origColor = new Color(1f, 1f, 1f, 0.0f);
  }

  public void SetHitProcs()
  {
    this.hitProcs = (isaPlayerHitProc[]) this.player.GetComponentsInChildren<isaPlayerHitProc>();
  }

  public void TookHit(int damage)
  {
    for (int index = 0; index < this.hitProcs.Length; ++index)
    {
      if (((Component) this.hitProcs[index]).get_gameObject().get_activeSelf())
      {
        ((PassiveSkillbitBase) ((Component) this.hitProcs[index]).GetComponent<PassiveSkillbitBase>()).passiveSBChance();
        if (((PassiveSkillbitBase) ((Component) this.hitProcs[index]).GetComponent<PassiveSkillbitBase>()).passiveSBChanceBool())
          return;
      }
    }
    Color color = ((Graphic) ((Component) this).GetComponent<Image>()).get_color();
    color.a = (__Null) 0.5;
    ((Graphic) ((Component) this).GetComponent<Image>()).set_color(color);
    ((Graphic) this.healthBar).get_color();
    ((Graphic) this.healthBar).set_color(Color.get_yellow());
    PlayerValues.Instance.CurrentHealth -= (float) damage;
    DamageFeedback.Instance.SpawnPlayerDamage(this.player.get_transform().get_position(), Color.get_red(), damage);
    ((Animator) this.player.GetComponent<Animator>()).SetTrigger("hit");
    this.revert = true;
  }

  private void Update()
  {
    if (!this.revert)
      return;
    ((Graphic) ((Component) this).GetComponent<Image>()).set_color(Color.Lerp(((Graphic) ((Component) this).GetComponent<Image>()).get_color(), this.origColor, Time.get_deltaTime() * 1.3f));
    ((Graphic) this.healthBar).set_color(Color.Lerp(((Graphic) this.healthBar).get_color(), this.origHPcolor, Time.get_deltaTime() * 1.3f));
    if (!Color.op_Equality(((Graphic) ((Component) this).GetComponent<Image>()).get_color(), this.origColor))
      return;
    this.revert = false;
  }
}
