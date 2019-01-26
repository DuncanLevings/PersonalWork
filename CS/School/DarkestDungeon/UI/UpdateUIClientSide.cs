using UnityEngine;
using UnityEngine.UI;

public class UpdateUIClientSide : MonoBehaviour
{
  public int StartingMana;
  public int CurrentMana;
  private Slider manaSlider;
  private Text coinsText;
  private int coins;
  private GameObject livesGO;
  private int lives;

  public UpdateUIClientSide()
  {
    base.\u002Ector();
  }

  private void Start()
  {
  }

  private void OnEnable()
  {
    if (Object.op_Implicit((Object) GameObject.Find("ManaSlider")))
      this.manaSlider = (Slider) GameObject.Find("ManaSlider").GetComponent<Slider>();
    this.lives = ((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).lives;
    if (Object.op_Implicit((Object) GameObject.Find("Lives")))
    {
      switch (this.lives)
      {
        case 1:
          ((Component) this.livesGO.get_transform().GetChild(0)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(1)).get_gameObject().SetActive(false);
          ((Component) this.livesGO.get_transform().GetChild(2)).get_gameObject().SetActive(false);
          break;
        case 2:
          ((Component) this.livesGO.get_transform().GetChild(0)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(1)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(2)).get_gameObject().SetActive(false);
          break;
        case 3:
          ((Component) this.livesGO.get_transform().GetChild(0)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(1)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(2)).get_gameObject().SetActive(true);
          break;
      }
    }
    this.coins = ((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).coins;
    if (!Object.op_Implicit((Object) GameObject.Find("CoinsText")))
      return;
    this.coinsText = (Text) GameObject.Find("CoinsText").get_gameObject().GetComponent<Text>();
    this.coinsText.set_text("Coins: " + (object) this.coins);
  }

  public void UseMana(int amount)
  {
    this.CurrentMana -= amount;
    this.manaSlider.set_value((float) this.CurrentMana);
  }

  public void addMana(int amount)
  {
    if (this.CurrentMana >= this.StartingMana)
      return;
    this.CurrentMana += amount;
    this.manaSlider.set_value((float) this.CurrentMana);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.manaSlider, (Object) null))
      this.manaSlider = (Slider) GameObject.Find("ManaSlider").GetComponent<Slider>();
    else
      this.manaSlider.set_value((float) this.CurrentMana);
    this.lives = ((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).lives;
    if (Object.op_Equality((Object) this.livesGO, (Object) null))
    {
      this.livesGO = GameObject.Find("Lives");
    }
    else
    {
      switch (this.lives)
      {
        case 1:
          ((Component) this.livesGO.get_transform().GetChild(0)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(1)).get_gameObject().SetActive(false);
          ((Component) this.livesGO.get_transform().GetChild(2)).get_gameObject().SetActive(false);
          break;
        case 2:
          ((Component) this.livesGO.get_transform().GetChild(0)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(1)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(2)).get_gameObject().SetActive(false);
          break;
        case 3:
          ((Component) this.livesGO.get_transform().GetChild(0)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(1)).get_gameObject().SetActive(true);
          ((Component) this.livesGO.get_transform().GetChild(2)).get_gameObject().SetActive(true);
          break;
      }
    }
    this.coins = ((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).coins;
    if (Object.op_Equality((Object) this.coinsText, (Object) null))
      this.coinsText = (Text) GameObject.Find("CoinsText").get_gameObject().GetComponent<Text>();
    else
      this.coinsText.set_text(this.coins.ToString());
  }
}
