using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
  private GameObject FinishPoint;
  private Animator anim;
  public Sprite[] icons;
  public Image icon;
  public Text textAmount;
  public GameObject button;
  private bool triggered;
  private float timer;

  public Chest()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.FinishPoint = ((Component) Object.FindObjectOfType<CheckEndGameState>()).get_gameObject();
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.timer = 1f;
  }

  private void OnEnable()
  {
    ((AudioSource) ((Component) Timeline.instance).GetComponent<AudioSource>()).Stop();
    if (PlayerValues.Instance.Chest > 0)
    {
      ((Component) Object.FindObjectOfType<isEconomies>()).get_gameObject().get_transform().SetParent(((Component) this).get_transform().get_root());
      ((Component) Object.FindObjectOfType<isEconomies>()).get_gameObject().get_transform().SetAsLastSibling();
    }
    else
    {
      ((Component) this).get_gameObject().SetActive(false);
      if (Object.op_Equality((Object) this.FinishPoint, (Object) null))
        this.FinishPoint = ((Component) Object.FindObjectOfType<CheckEndGameState>()).get_gameObject();
      else
        ((CheckEndGameState) this.FinishPoint.GetComponent<CheckEndGameState>()).AllChestsOpen = true;
    }
  }

  public void GiveLoot()
  {
    this.anim.SetFloat("speed", 1f);
    if (PlayerValues.Instance.normalChests.Count <= 0)
      return;
    int currencyLoot = PlayerValues.Instance.normalChests[0].currencyLoot;
    switch (PlayerValues.Instance.normalChests[0].currencyType)
    {
      case ChestLoot.Currency.gold:
        PlayerValues.Instance.AddGold(currencyLoot);
        break;
      case ChestLoot.Currency.darkmatter:
        PlayerValues.Instance.AddDarkMatter(currencyLoot);
        break;
      case ChestLoot.Currency.building:
        PlayerValues.Instance.AddBuildMat(currencyLoot);
        break;
    }
    --PlayerValues.Instance.Chest;
    this.textAmount.set_text("+" + (object) currencyLoot);
    PlayerValues.Instance.normalChests.RemoveAt(0);
  }

  public void setIcon()
  {
    if (PlayerValues.Instance.normalChests.Count <= 0)
      return;
    switch (PlayerValues.Instance.normalChests[0].currencyType)
    {
      case ChestLoot.Currency.gold:
        this.icon.set_sprite(this.icons[0]);
        break;
      case ChestLoot.Currency.darkmatter:
        this.icon.set_sprite(this.icons[1]);
        break;
      case ChestLoot.Currency.building:
        this.icon.set_sprite(this.icons[2]);
        break;
    }
  }

  public void enableButton()
  {
  }

  public void triggerTrue()
  {
    this.triggered = true;
  }

  public void triggerFalse()
  {
    this.triggered = false;
  }

  public void PlaySound()
  {
    ((AudioSource) ((Component) this).GetComponent<AudioSource>()).Play();
  }

  public void NextChest()
  {
    if (PlayerValues.Instance.Chest <= 0)
      return;
    if (!this.triggered)
      this.anim.SetFloat("speed", 5f);
    AnimatorStateInfo animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
    if (!((AnimatorStateInfo) ref animatorStateInfo).IsName("OpenChestText"))
      return;
    this.anim.SetTrigger("nextChest");
  }

  public void ReturnHome()
  {
    ((CheckEndGameState) this.FinishPoint.GetComponent<CheckEndGameState>()).AllChestsOpen = true;
  }

  private void Update()
  {
    if (PlayerValues.Instance.Chest > 0)
      return;
    this.timer -= Time.get_deltaTime();
    if ((double) this.timer > 0.0)
      return;
    this.button.SetActive(true);
  }
}
