using System.Collections;
using UnityEngine;

public class UpdateUIAllClients : MonoBehaviour
{
  public int health;
  public GameObject heartPrefab;
  public Sprite[] heartImgs;
  private int healthPerHeart;
  private ArrayList hearts;

  public UpdateUIAllClients()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.AddHeart(((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).health / this.healthPerHeart);
  }

  public void AddHeart(int n)
  {
    for (int index = 0; index < n; ++index)
    {
      Transform transform = ((Component) ((Component) this).GetComponentInChildren<isHeartPos>()).get_gameObject().get_transform();
      GameObject gameObject = (GameObject) Object.Instantiate((Object) this.heartPrefab, Vector3.op_Addition(((Component) transform).get_transform().get_position(), new Vector3((float) (index * 45), 0.0f, 0.0f)), Quaternion.get_identity());
      gameObject.get_transform().set_parent(transform);
      this.hearts.Add((object) gameObject);
    }
    this.UpdateHearts();
  }

  public void UpdateHearts()
  {
    if (PhotonView.Get((Component) this).isMine)
      this.health = ((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).health;
    this.health = Mathf.Clamp(this.health, 0, ((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).maxhealth);
    bool flag = false;
    int num = 0;
    if (this.hearts.Count < 1 || this.hearts == null)
      this.AddHeart(((CharacterStats) ((Component) this).GetComponent<CharacterStats>()).health / this.healthPerHeart);
    foreach (GameObject heart in this.hearts)
    {
      if (flag)
      {
        ((SpriteRenderer) heart.GetComponent<SpriteRenderer>()).set_sprite(this.heartImgs[0]);
      }
      else
      {
        ++num;
        if (this.health >= num * this.healthPerHeart)
        {
          ((SpriteRenderer) heart.GetComponent<SpriteRenderer>()).set_sprite(this.heartImgs[this.hearts.Count - 1]);
        }
        else
        {
          int index = this.healthPerHeart - (this.healthPerHeart * num - this.health);
          ((SpriteRenderer) heart.GetComponent<SpriteRenderer>()).set_sprite(this.heartImgs[index]);
          flag = true;
        }
      }
    }
  }

  private void Update()
  {
    this.UpdateHearts();
  }
}
