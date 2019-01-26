using System;
using UnityEngine;

[RequireComponent(typeof (Seeker))]
public class PlayerPathingAI : AIPath
{
  public float sleepVelocity = 0.4f;
  public float speedDampTime = 0.1f;
  public float angularSpeedDampTime = 0.7f;
  public float angleResponseTime = 0.6f;
  public Animator anim;
  public float OrginalBaseSpeed;
  public GameObject endOfPathEffect;
  private player playerScript;
  private bool run;
  protected Vector3 lastTarget;

  public new void Start()
  {
    Debug.Log((object) "AStarPath, 850 disabled, logs debug path info");
    this.anim = (Animator) ((Component) this).GetComponent<Animator>();
    base.Start();
    this.OrginalBaseSpeed = this.speed;
    this.playerScript = (player) ((Component) this).GetComponent<player>();
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
    if (Object.op_Inequality((Object) this.playerScript.target, (Object) null) && !Object.op_Implicit((Object) this.playerScript.target.GetComponent<DoorType>()) && Object.op_Inequality((Object) this.target, (Object) this.playerScript.target))
      this.target = this.playerScript.target.get_transform();
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
    Vector3 vector3_2 = this.tr.InverseTransformDirection(vector3_1);
    vector3_2.y = (__Null) 0.0;
    float z = (float) vector3_2.z;
    this.run = this.playerScript.running;
    if (this.run)
    {
      this.speed = this.OrginalBaseSpeed * 2f * TimeScale.Instance.scale;
    }
    else
    {
      this.speed = this.OrginalBaseSpeed * TimeScale.Instance.scale;
      z *= this.OrginalBaseSpeed;
    }
    float num1 = Mathf.Clamp(z, 0.0f, 2f);
    float num2 = this.FindAngle(((Component) this).get_transform().get_forward(), toVector, ((Component) this).get_transform().get_up()) / this.angleResponseTime;
    if (this.run)
    {
      this.anim.SetFloat("Speed", 0.0f);
      this.anim.SetFloat("AngularSpeed", 0.0f);
      this.anim.SetFloat("runSpeed", num1);
      this.anim.SetFloat("runAngularSpeed", num2);
    }
    else
    {
      this.anim.SetFloat("runSpeed", 0.0f);
      this.anim.SetFloat("runAngularSpeed", 0.0f);
      this.anim.SetFloat("Speed", num1);
      this.anim.SetFloat("AngularSpeed", num2);
    }
    this.anim.SetFloat("TimeScale", TimeScale.Instance.scale);
  }

  private float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
  {
    if (Vector3.op_Equality(toVector, Vector3.get_zero()))
      return 0.0f;
    return Vector3.Angle(fromVector, toVector) * Mathf.Sign(Vector3.Dot(Vector3.Cross(fromVector, toVector), upVector)) * ((float) Math.PI / 180f);
  }
}
