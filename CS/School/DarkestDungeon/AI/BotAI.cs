using UnityEngine;

public class BotAI : MonoBehaviour
{
  private float health;
  public float movespeed;
  public GameObject player;
  public GameObject enemyTarget;
  public float followRadius;
  public float seekRadius;
  public float destroyRadius;
  public bool ShowRadiusDebug;
  private Vector3 oldPos;
  private Animator anim;

  public BotAI()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.player = GameObject.FindGameObjectWithTag("Player");
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
  }

  public bool PlayerStillCheck()
  {
    if (Object.op_Inequality((Object) this.player, (Object) null))
    {
      if (this.oldPos.x == this.player.get_transform().get_position().x)
        return true;
      this.oldPos = this.player.get_transform().get_position();
    }
    return false;
  }

  public bool WithinFollowRadius()
  {
    if (Object.op_Inequality((Object) this.player, (Object) null))
    {
      foreach (Component component in Physics2D.OverlapCircleAll(Vector2.op_Implicit(this.player.get_transform().get_position()), this.followRadius))
      {
        if (component.get_tag() == "LinkBot")
          return true;
      }
    }
    return false;
  }

  public bool FindEnemy(float radius)
  {
    if (Object.op_Inequality((Object) this.player, (Object) null))
    {
      Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(Vector2.op_Implicit(this.player.get_transform().get_position()), radius);
      for (int index = 0; index < collider2DArray.Length; ++index)
      {
        if (((Component) collider2DArray[index]).get_tag() == "Enemy")
        {
          this.enemyTarget = ((Component) collider2DArray[index]).get_gameObject();
          return true;
        }
      }
    }
    return false;
  }

  public bool FindBoss(float radius)
  {
    if (Object.op_Inequality((Object) this.player, (Object) null))
    {
      Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(Vector2.op_Implicit(this.player.get_transform().get_position()), radius);
      for (int index = 0; index < collider2DArray.Length; ++index)
      {
        if (((Object) ((Component) collider2DArray[index]).get_gameObject()).get_name() == "Boss")
        {
          this.enemyTarget = ((Component) collider2DArray[index]).get_gameObject();
          return true;
        }
      }
    }
    return false;
  }

  public bool outsideDestroyRadius()
  {
    if (Object.op_Inequality((Object) this.player, (Object) null))
    {
      foreach (Component component in Physics2D.OverlapCircleAll(Vector2.op_Implicit(this.player.get_transform().get_position()), this.destroyRadius))
      {
        if (component.get_tag() == "LinkBot")
          return true;
      }
    }
    return false;
  }

  public void Idle()
  {
    if (!Object.op_Inequality((Object) this.anim, (Object) null))
      return;
    this.anim.SetBool("run", false);
  }

  private void moveLeft()
  {
    Transform transform = ((Component) this).get_transform();
    transform.set_position(Vector3.op_Subtraction(transform.get_position(), new Vector3(this.movespeed, 0.0f, 0.0f)));
    ((Component) this).get_transform().set_rotation(new Quaternion((float) ((Component) this).get_transform().get_rotation().x, 180f, (float) ((Component) this).get_transform().get_rotation().z, (float) ((Component) this).get_transform().get_rotation().w));
    this.anim.SetBool("run", true);
  }

  private void moveRight()
  {
    Transform transform = ((Component) this).get_transform();
    transform.set_position(Vector3.op_Addition(transform.get_position(), new Vector3(this.movespeed, 0.0f, 0.0f)));
    ((Component) this).get_transform().set_rotation(new Quaternion((float) ((Component) this).get_transform().get_rotation().x, 0.0f, (float) ((Component) this).get_transform().get_rotation().z, (float) ((Component) this).get_transform().get_rotation().w));
    this.anim.SetBool("run", true);
  }

  public void FollowTarget(GameObject target)
  {
    if (!Object.op_Inequality((Object) this.player, (Object) null))
      return;
    if (((Component) this).get_transform().get_position().x < target.get_transform().get_position().x)
      this.moveRight();
    else
      this.moveLeft();
  }

  public bool InRangeOfTarget(float value)
  {
    return Object.op_Inequality((Object) this.enemyTarget, (Object) null) && (double) Vector3.Distance(this.enemyTarget.get_transform().get_position(), ((Component) this).get_transform().get_position()) < (double) value;
  }

  public void AttackTarget()
  {
    this.anim.SetTrigger("attack");
    this.anim.SetBool("run", false);
  }

  private void Update()
  {
  }

  private void OnDrawGizmosSelected()
  {
    if (!this.ShowRadiusDebug)
      return;
    Gizmos.set_color(Color.get_green());
    Gizmos.DrawWireSphere(this.player.get_transform().get_position(), this.followRadius);
    Gizmos.set_color(Color.get_yellow());
    Gizmos.DrawWireSphere(this.player.get_transform().get_position(), this.seekRadius);
    Gizmos.set_color(Color.get_red());
    Gizmos.DrawWireSphere(this.player.get_transform().get_position(), this.destroyRadius);
  }
}
