using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class player : playerBase
{
  private float invulnFrameTime = 0.25f;
  [Header("Player Joints and Equipment ScriptObj")]
  public Transform Head;
  public Transform Body;
  public Transform RightHand;
  public Transform LeftHand;
  public HeroEquipment equipment;
  public HeroInventorySO inventory;
  public TookDamage tookDamage;
  private List<GameObject> skillbitSlotType;
  private SkillbitSlotHolder[] SkillbitSlots;
  private GameObject FinishPoint;
  private bool started;
  [HideInInspector]
  public bool running;
  [HideInInspector]
  public bool DetectHits;
  private bool triggeredHit;

  public void Start()
  {
    if (this.started)
      return;
    this.started = true;
    this.skillbitSlotType = new List<GameObject>();
    this.SkillbitSlots = ((IEnumerable<SkillbitSlotHolder>) Object.FindObjectsOfType<SkillbitSlotHolder>()).OrderBy<SkillbitSlotHolder, string>((Func<SkillbitSlotHolder, string>) (go => ((Object) go).get_name())).ToArray<SkillbitSlotHolder>();
    this.EquipSkillbits();
    this.EquipWeapon();
    this.EquipArmor();
    this.SetSlotSkillbits();
    this.getTrees();
    this.GenerateTypeLists();
    this.enableOnlyDefaultSB();
    this.FinishPoint = GameObject.Find("FinishPoint");
    PlayerSingleton.Instance.CheckSkillbits();
    this.DetectHits = true;
  }

  private void EquipSkillbits()
  {
    for (int index = 0; index < this.equipment.FixedSkillBits.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.equipment.FixedSkillBits[index].skillbit, (Object) null))
        ((GameObject) Object.Instantiate((Object) this.equipment.FixedSkillBits[index].skillbit, ((Component) this).get_transform().get_position(), Quaternion.get_identity())).get_transform().set_parent(((Component) this).get_gameObject().get_transform());
    }
    for (int index = 0; index < this.equipment.EquipableSkillBits_Active.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.equipment.EquipableSkillBits_Active[index].skillbit, (Object) null))
      {
        GameObject gameObject = (GameObject) Object.Instantiate((Object) this.equipment.EquipableSkillBits_Active[index].skillbit, ((Component) this).get_transform().get_position(), Quaternion.get_identity());
        gameObject.get_transform().set_parent(((Component) this).get_gameObject().get_transform());
        if (Object.op_Implicit((Object) gameObject.GetComponent<SlotSkillbitData>()))
          this.skillbitSlotType.Add(gameObject);
      }
    }
    for (int index = 0; index < this.equipment.EquipableSkillBits_Passive.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.equipment.EquipableSkillBits_Passive[index].skillbit, (Object) null))
        ((GameObject) Object.Instantiate((Object) this.equipment.EquipableSkillBits_Passive[index].skillbit, ((Component) this).get_transform().get_position(), Quaternion.get_identity())).get_transform().set_parent(((Component) this).get_gameObject().get_transform());
    }
  }

  private void EquipWeapon()
  {
    if (this.equipment.rightHand != null && Object.op_Inequality((Object) this.equipment.rightHand.scripObjRef, (Object) null))
    {
      this.RemoveEquipInSlot(this.RightHand);
      GameObject gameObject = (GameObject) Object.Instantiate((Object) ((WeaponObject) this.equipment.rightHand.scripObjRef).weaponObject, this.RightHand.get_position(), Quaternion.get_identity());
      gameObject.get_transform().set_parent(this.RightHand);
      ((SetRotation) gameObject.GetComponent<SetRotation>()).SetRightHandRotation();
      ((isSwordHitbox) ((Component) this).GetComponentInChildren<isSwordHitbox>()).baseDamage = ((SetRotation) gameObject.GetComponent<SetRotation>()).Damage;
    }
    if (this.equipment.leftHand == null || !Object.op_Inequality((Object) this.equipment.leftHand.scripObjRef, (Object) null))
      return;
    this.RemoveEquipInSlot(this.LeftHand);
    GameObject gameObject1 = (GameObject) Object.Instantiate((Object) ((ArmorObject) this.equipment.leftHand.scripObjRef).armorObject, this.LeftHand.get_position(), Quaternion.get_identity());
    gameObject1.get_transform().set_parent(this.LeftHand);
    ((SetRotation) gameObject1.GetComponent<SetRotation>()).SetLeftHandRotation();
  }

  private void EquipArmor()
  {
    if (this.equipment.head != null && Object.op_Inequality((Object) this.equipment.head.scripObjRef, (Object) null))
    {
      this.RemoveEquipInSlot(this.Head);
      GameObject gameObject = (GameObject) Object.Instantiate((Object) ((ArmorObject) this.equipment.head.scripObjRef).armorObject, this.Head.get_position(), Quaternion.get_identity());
      gameObject.get_transform().set_parent(this.Head);
      ((setPosition) gameObject.GetComponent<setPosition>()).SetHeadPosition();
    }
    if (this.equipment.body == null || !Object.op_Inequality((Object) this.equipment.body.scripObjRef, (Object) null))
      return;
    ((GameObject) Object.Instantiate((Object) ((ArmorObject) this.equipment.body.scripObjRef).armorObject, this.Body.get_position(), Quaternion.get_identity())).get_transform().set_parent(this.Body);
  }

  private void RemoveEquipInSlot(Transform slot)
  {
    Transform[] componentsInChildren = (Transform[]) ((Component) slot).GetComponentsInChildren<Transform>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (Object.op_Implicit((Object) ((Component) componentsInChildren[index]).GetComponent<isEquipable>()) && Object.op_Inequality((Object) componentsInChildren[index], (Object) slot))
        Object.Destroy((Object) ((Component) componentsInChildren[index]).get_gameObject());
    }
  }

  private void SetSlotSkillbits()
  {
    for (int index1 = 0; index1 < this.SkillbitSlots.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.skillbitSlotType.Count; ++index2)
      {
        if (Object.op_Equality((Object) this.SkillbitSlots[index1].skillbit, (Object) null) && !this.skillbitSlotCheck(this.skillbitSlotType[index2]))
        {
          this.SkillbitSlots[index1].skillbit = this.skillbitSlotType[index2];
          this.SkillbitSlots[index1].slot.SetActive(true);
          this.SkillbitSlots[index1].SlotSkillbit();
        }
      }
    }
  }

  private bool skillbitSlotCheck(GameObject skillbit)
  {
    for (int index = 0; index < this.SkillbitSlots.Length; ++index)
    {
      if (Object.op_Equality((Object) this.SkillbitSlots[index].skillbit, (Object) skillbit))
        return true;
    }
    return false;
  }

  public void MoveToEnd()
  {
    if (!Object.op_Inequality((Object) this.FinishPoint, (Object) null))
      return;
    ((AIPath) ((Component) this).GetComponent<PlayerPathingAI>()).target = this.FinishPoint.get_transform();
  }

  public void MoveToRoomCenter()
  {
    ((AIPath) ((Component) this).GetComponent<PlayerPathingAI>()).target = Timeline.instance.FirstEvent().get_transform();
  }

  private void Update()
  {
    this.getTarget();
    this.running = this.CheckEventInRadius();
    if (!Timeline.instance.TimeLineEmptyCheck())
      this.TargetRoom = Timeline.instance.FirstRoom();
    if (!this.triggeredHit)
      return;
    this.invulnFrameTime -= Time.get_deltaTime();
    if ((double) this.invulnFrameTime > 0.0)
      return;
    this.invulnFrameTime = 0.25f;
    this.triggeredHit = false;
  }

  private bool CheckEventInRadius()
  {
    return ((RoomEntityList) this.CurrentRoom.GetComponent<RoomEntityList>()).EntitysInRoom.Count == 0;
  }

  public bool checkExploreCenter()
  {
    return !Timeline.instance.TimeLineRoomEmptyCheck() && Object.op_Implicit((Object) Timeline.instance.FirstEvent().GetComponent<isaRoomCenter>());
  }

  public void EnableHitbox()
  {
    ((Collider) ((Component) ((Component) this).GetComponentInChildren<isSwordHitbox>()).GetComponent<BoxCollider>()).set_enabled(true);
    ((AudioSource) ((Component) ((Component) this).GetComponentInChildren<isSwordHitbox>()).GetComponent<AudioSource>()).Play();
  }

  public void DisableEnableHitbox()
  {
    ((Collider) ((Component) ((Component) this).GetComponentInChildren<isSwordHitbox>()).GetComponent<BoxCollider>()).set_enabled(false);
  }

  private void OnTriggerEnter(Collider col)
  {
    if (this.triggeredHit || !this.DetectHits)
      return;
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<isaBossHitbox>()))
    {
      if (Object.op_Equality((Object) this.tookDamage, (Object) null))
        this.tookDamage = (TookDamage) GameObject.Find("TookDamage").GetComponent<TookDamage>();
      this.tookDamage.TookHit(((isaBossHitbox) ((Component) col).GetComponent<isaBossHitbox>()).damage);
      ((Animator) ((Component) this).GetComponent<Animator>()).SetTrigger("hit");
      ((AudioSource) ((Component) this).GetComponent<AudioSource>()).Play();
      this.triggeredHit = true;
    }
    if (Object.op_Implicit((Object) ((Component) col).GetComponent<isaEnemyHitbox>()))
    {
      if (Object.op_Equality((Object) this.tookDamage, (Object) null))
        this.tookDamage = (TookDamage) GameObject.Find("TookDamage").GetComponent<TookDamage>();
      this.tookDamage.TookHit(((isaEnemyHitbox) ((Component) col).GetComponent<isaEnemyHitbox>()).damage);
      ((Animator) ((Component) this).GetComponent<Animator>()).SetTrigger("hit");
      ((AudioSource) ((Component) this).GetComponent<AudioSource>()).Play();
      this.triggeredHit = true;
    }
    if (!Object.op_Implicit((Object) ((Component) col).GetComponent<EnemyProjectile>()))
      return;
    if (Object.op_Equality((Object) this.tookDamage, (Object) null))
      this.tookDamage = (TookDamage) GameObject.Find("TookDamage").GetComponent<TookDamage>();
    this.tookDamage.TookHit(((EnemyProjectile) ((Component) col).GetComponent<EnemyProjectile>()).damage);
    ((AudioSource) ((Component) this).GetComponent<AudioSource>()).Play();
    this.triggeredHit = true;
  }
}
