using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
  public int coins;
  public int lives;
  public int maxhealth;
  public int health;
  public int armor;
  public int mana;
  public int meleeDamage;
  public int rangedDamage;
  public int magicDamage;
  public int magicShield;
  private Animator anim;
  public string OriginalTag;
  public bool hasBow;
  public GameObject bow;
  public List<GameObject> respawnPoints;
  public float RespawnTime;
  private GameObject diePanel;
  private GameObject GameOverPanel;
  public bool GodMode;

  public CharacterStats()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    Social.ReportProgress("CgkIp57Sv44CEAIQBQ", 100.0, (Action<bool>) (success => {}));
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    this.diePanel = GameObject.Find("Die");
    this.GameOverPanel = GameObject.Find("GameOver");
    IEnumerator enumerator = GameObject.Find("PlayerRespawns").get_transform().GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
        this.respawnPoints.Add(((Component) enumerator.Current).get_gameObject());
    }
    finally
    {
      (enumerator as IDisposable)?.Dispose();
    }
  }

  private void Update()
  {
    if (Input.GetKey((KeyCode) 112))
      this.GodMode = true;
    if (this.GodMode)
      return;
    if (this.lives > 0 && this.health < 1)
    {
      this.UpdateStats();
      if (this.lives < 1)
        return;
      PhotonView.Get((Component) this).RPC("disablePlayerForAllClients", PhotonTargets.All);
      if (PhotonView.Get((Component) this).isMine)
      {
        this.Died();
        this.Invoke("Respawn", this.RespawnTime);
      }
      this.Invoke("callEnablePlayerForAllClient", this.RespawnTime + 0.5f);
    }
    else
    {
      if (this.lives >= 1)
        return;
      PhotonView.Get((Component) this).RPC("disablePlayerForAllClients", PhotonTargets.All);
      if (!PhotonView.Get((Component) this).isMine)
        return;
      this.GameOver();
    }
  }

  private void GameOver()
  {
    this.disablePlayer(false);
    ((setActiveLose) this.GameOverPanel.GetComponent<setActiveLose>()).setActiveOnLose(true);
  }

  private void UpdateStats()
  {
    --this.lives;
    this.health = this.maxhealth;
    this.UpdateHearts();
  }

  public void UpdateHearts()
  {
    ((UpdateUIAllClients) ((Component) this).GetComponent<UpdateUIAllClients>()).UpdateHearts();
  }

  private void Died()
  {
    this.disablePlayer(false);
    ((setActiveLose) this.diePanel.GetComponent<setActiveLose>()).setActiveOnLose(true);
  }

  public void Respawn()
  {
    if (((Component) this).get_gameObject().get_tag() == "Invader")
      ((Component) this).get_transform().set_position(((invaderRespawnScript) GameObject.Find("InvaderRespawns").GetComponent<invaderRespawnScript>()).invaderRespawnPoints[0].get_gameObject().get_transform().get_position());
    else
      ((Component) this).get_transform().set_position(this.respawnPoints[0].get_gameObject().get_transform().get_position());
    this.disablePlayer(true);
    ((setActiveLose) this.diePanel.GetComponent<setActiveLose>()).setActiveOnLose(false);
  }

  public void callEnablePlayerForAllClient()
  {
    PhotonView.Get((Component) this).RPC("enablePlayerForAllClients", PhotonTargets.All);
  }

  private void disablePlayer(bool val)
  {
    ((Behaviour) ((Component) this).GetComponent<CharacterMovement>()).set_enabled(val);
    ((Behaviour) ((Component) this).GetComponent<CharacterController>()).set_enabled(val);
  }

  public void spawnBow()
  {
    if (!this.hasBow)
      return;
    PhotonNetwork.Instantiate(((Object) this.bow).get_name(), GameObject.Find("StartBowSpawn").get_transform().get_position(), ((Component) this).get_transform().get_rotation(), 0);
  }

  [PunRPC]
  private void disablePlayerForAllClients()
  {
    ((Renderer) ((Component) this).GetComponent<SpriteRenderer>()).set_enabled(false);
    ((Behaviour) ((Component) this).GetComponent<BoxCollider2D>()).set_enabled(false);
    ((Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>()).set_isKinematic(true);
    foreach (Renderer componentsInChild in (SpriteRenderer[]) ((Component) this).GetComponentsInChildren<SpriteRenderer>())
      componentsInChild.set_enabled(false);
  }

  [PunRPC]
  private void enablePlayerForAllClients()
  {
    ((Renderer) ((Component) this).GetComponent<SpriteRenderer>()).set_enabled(true);
    ((Behaviour) ((Component) this).GetComponent<BoxCollider2D>()).set_enabled(true);
    ((Rigidbody2D) ((Component) this).GetComponent<Rigidbody2D>()).set_isKinematic(false);
    foreach (Renderer componentsInChild in (SpriteRenderer[]) ((Component) this).GetComponentsInChildren<SpriteRenderer>())
      componentsInChild.set_enabled(true);
  }

  [PunRPC]
  private void TakeDamage(int damage)
  {
    this.health -= damage;
    this.health = Mathf.Clamp(this.health, 0, this.maxhealth);
    this.UpdateHearts();
  }

  private void OnCollisionEnter2D(Collision2D coll)
  {
    if (!Object.op_Inequality((Object) this.anim, (Object) null))
      return;
    if (coll.get_gameObject().get_tag() == "Enemy")
    {
      AnimatorStateInfo animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
      if (!((AnimatorStateInfo) ref animatorStateInfo).IsName("jumpAttack"))
      {
        Debug.Log((object) "hit");
        PhotonView.Get((Component) this).RPC("TakeDamage", PhotonTargets.All, (object) 1);
      }
    }
    if (!(coll.get_gameObject().get_tag() == "Invader"))
      return;
    AnimatorStateInfo animatorStateInfo1 = this.anim.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo1).IsName("jumpAttack"))
      return;
    Debug.Log((object) "hit");
  }

  private void OnTriggerEnter2D(Collider2D coll)
  {
    if (!Object.op_Inequality((Object) this.anim, (Object) null) || !(((Component) coll).get_gameObject().get_tag() == "Hitbox"))
      return;
    Debug.Log((object) "Trigger hit");
    AnimatorStateInfo animatorStateInfo = this.anim.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo).IsName("idle") && Object.op_Inequality((Object) ((Component) coll).get_gameObject().GetComponent<isProjectile>(), (Object) null))
    {
      Debug.Log((object) "block");
      this.anim.SetBool("block", true);
    }
    else
      PhotonView.Get((Component) this).RPC("TakeDamage", PhotonTargets.All, (object) 1);
  }
}
