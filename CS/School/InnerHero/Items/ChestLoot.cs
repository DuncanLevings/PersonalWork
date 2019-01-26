using UnityEngine;

public class ChestLoot : MonoBehaviour
{
  [HideInInspector]
  public int currencyLoot;
  public ChestLoot.Type lootType;
  public int forcedAmount;
  public ChestLoot.Currency currencyType;

  public ChestLoot()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.setLoot();
  }

  private void setLoot()
  {
    if (this.lootType == ChestLoot.Type.forced)
    {
      this.currencyLoot = this.forcedAmount;
    }
    else
    {
      if (this.lootType != ChestLoot.Type.random)
        return;
      int num = Random.Range(0, 3);
      if (num == 0)
      {
        this.currencyLoot = Random.Range(50, 100);
        this.currencyType = ChestLoot.Currency.gold;
      }
      if (num == 1)
      {
        this.currencyLoot = Random.Range(5, 50);
        this.currencyType = ChestLoot.Currency.darkmatter;
      }
      if (num != 2)
        return;
      this.currencyLoot = Random.Range(1, 5);
      this.currencyType = ChestLoot.Currency.building;
    }
  }

  private void Update()
  {
  }

  public enum Type
  {
    random,
    forced,
  }

  public enum Currency
  {
    gold,
    darkmatter,
    building,
  }
}
