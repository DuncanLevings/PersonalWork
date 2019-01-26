using UnityEngine;

public class BlockPassiveSB : PassiveSkillbitBase
{
  [Range(0.0f, 1f)]
  [Tooltip("lower value = higher chance ie. 0.35 = 75% chance")]
  public float procChance;
  private GameObject player;

  private void Start()
  {
    this.player = PlayerSingleton.Instance.getPlayer();
  }

  public override bool passiveSBChanceBool()
  {
    if ((double) Random.get_value() < (double) this.procChance)
      return false;
    DamageFeedback.Instance.SpawnPlayerHit(this.player.get_transform().get_position(), Color.get_red(), "Block!");
    return true;
  }

  private void Update()
  {
  }

  public override void GetEventTarget()
  {
  }

  public override void passiveSBChance()
  {
  }
}
