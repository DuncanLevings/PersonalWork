using System;
using UnityEngine;

[RequireComponent(typeof (Seeker))]
public class EnemyPathingAI : AIPath
{
  public float sleepVelocity = 0.4f;
  public float speedDampTime = 0.1f;
  public float angularSpeedDampTime = 0.7f;
  public float angleResponseTime = 0.6f;
  public Animator anim;
  private float OrginalBaseSpeed;
  [HideInInspector]
  public float UniqueTimeScale;
  [HideInInspector]
  public float spellEffectDuration;
  [HideInInspector]
  public bool EffectBySpell;
  public GameObject endOfPathEffect;
  protected Vector3 lastTarget;

  public new void Start()
  {
    Debug.Log((object) "AStarPath, 850 disabled, logs debug path info");
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    base.Start();
    this.OrginalBaseSpeed = this.speed;
    this.UniqueTimeScale = 1f;
  }

  public override void OnTargetReached()
  {
    if (!Object.op_Inequality((Object) this.endOfPathEffect, (Object) null) || (double) Vector3.Distance(this.tr.get_position(), this.lastTarget) <= 1.0)
      return;
    Object.Instantiate((Object) this.endOfPathEffect, this.tr.get_position(), this.tr.get_rotation());
    this.lastTarget = this.tr.get_position();
  }

  public override Vector3 GetFeetPosition()
  {
    return this.tr.get_position();
  }

  protected new void Update()
  {
    Vector3 toVector = Vector3.get_zero();
    Vector3 vector3_1;
    if (this.canMove)
    {
      toVector = this.CalculateVelocity(this.GetFeetPosition());
      this.RotateTowards(this.targetDirection);
      toVector.y = (__Null) 0.0;
      if ((double) ((Vector3) ref toVector).get_sqrMagnitude() <= (double) this.sleepVelocity * (double) this.sleepVelocity)
        toVector = Vector3.get_zero();
      if (Object.op_Inequality((Object) this.controller, (Object) null))
      {
        this.controller.SimpleMove(toVector);
        vector3_1 = this.controller.get_velocity();
      }
      else
      {
        Debug.LogWarning((object) "No NavmeshController or CharacterController attached to GameObject");
        vector3_1 = Vector3.get_zero();
      }
    }
    else
      vector3_1 = Vector3.get_zero();
    if (this.EffectBySpell)
    {
      this.anim.SetFloat("TimeScale", this.UniqueTimeScale);
      this.speed = this.OrginalBaseSpeed * this.UniqueTimeScale;
      this.spellEffectDuration -= Time.get_deltaTime();
      if ((double) this.spellEffectDuration <= 0.0)
        this.EffectBySpell = false;
    }
    else
    {
      this.anim.SetFloat("TimeScale", TimeScale.Instance.scale);
      this.speed = this.OrginalBaseSpeed * TimeScale.Instance.scale;
    }
    Vector3 vector3_2 = this.tr.InverseTransformDirection(vector3_1);
    vector3_2.y = (__Null) 0.0;
    float num1 = Mathf.Clamp((float) vector3_2.z * this.OrginalBaseSpeed, 0.0f, 2f);
    float num2 = this.FindAngle(((Component) this).get_transform().get_forward(), toVector, ((Component) this).get_transform().get_up()) / this.angleResponseTime;
    this.anim.SetFloat("Speed", num1);
    this.anim.SetFloat("AngularSpeed", num2);
  }

  private float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
  {
    if (Vector3.op_Equality(toVector, Vector3.get_zero()))
      return 0.0f;
    return Vector3.Angle(fromVector, toVector) * Mathf.Sign(Vector3.Dot(Vector3.Cross(fromVector, toVector), upVector)) * ((float) Math.PI / 180f);
  }
}
