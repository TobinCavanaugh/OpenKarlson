using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Vector3 rotationVel;

    private float speed = 8f;

    private float posSpeed = 0.075f;

    private float posOffset = 0.004f;

    private Vector3 defaultPos;

    private Vector3 posVel;

    private Rigidbody rb;

    private float rotationOffset;

    private float rotationOffsetZ;

    private float rotVelY;

    private float rotVelZ;

    private Vector3 prevRotation;

    private Vector3 desiredBob;

    private float xBob = 0.12f;

    private float yBob = 0.08f;

    private float zBob = 0.1f;

    private float bobSpeed = 0.45f;

    public PlayerMovement movement;

    public static Gun Instance
    {
        get;
        set;
    }

    public Gun()
    {
    }

    private void MoveGun()
    {
        if (!this.rb || !movement.grounded)
        {
            return;
        }
        if (Mathf.Abs(this.rb.velocity.magnitude) < 4f)
        {
            this.desiredBob = Vector3.zero;
            return;
        }
        float single = Mathf.PingPong(Time.time * this.bobSpeed, this.xBob) - this.xBob / 2f;
        float single1 = Mathf.PingPong(Time.time * this.bobSpeed, this.yBob) - this.yBob / 2f;
        float single2 = Mathf.PingPong(Time.time * this.bobSpeed, this.zBob) - this.zBob / 2f;
        this.desiredBob = new Vector3(single, single1, single2);
    }

    public void Shoot()
    {
        float recoil = movement.GetRecoil();
        Vector3 vector3 = ((Vector3.up / 4f) + (Vector3.back / 1.5f)) * recoil;
        base.transform.localPosition = base.transform.localPosition + vector3;
        Quaternion quaternion = Quaternion.Euler(-60f * recoil, 0f, 0f);
        base.transform.localRotation = quaternion;
    }

    private void Start()
    {
        Gun.Instance = this;
        this.defaultPos = base.transform.localPosition;
        this.rb = movement.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (movement && !movement.HasGun())
        {
            return;
        }
        this.MoveGun();
        Vector3 grapplePoint = movement.GetGrapplePoint();
        Vector3 vector3 = movement.GetGrapplePoint() - base.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vector3.normalized);
        float single = this.rotationOffset;
        Quaternion quaternion1 = base.transform.parent.rotation;
        this.rotationOffset = single + Mathf.DeltaAngle(quaternion1.eulerAngles.y, this.prevRotation.y);
        if (this.rotationOffset > 90f)
        {
            this.rotationOffset = 90f;
        }
        if (this.rotationOffset < -90f)
        {
            this.rotationOffset = -90f;
        }
        this.rotationOffset = Mathf.SmoothDamp(this.rotationOffset, 0f, ref this.rotVelY, 0.025f);
        Vector3 vector31 = new Vector3(0f, this.rotationOffset, this.rotationOffset);
        if (grapplePoint == Vector3.zero)
        {
            quaternion1 = base.transform.parent.rotation;
            quaternion = Quaternion.Euler(quaternion1.eulerAngles - vector31);
        }
        base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, Time.deltaTime * this.speed);
        Vector3 look = movement.FindVelRelativeToLook() * this.posOffset;
        float fallSpeed = movement.GetFallSpeed() * this.posOffset;
        if (fallSpeed < -0.08f)
        {
            fallSpeed = -0.08f;
        }
        Vector3 vector32 = this.defaultPos - new Vector3(look.x, fallSpeed, look.y);
        base.transform.localPosition = Vector3.SmoothDamp(base.transform.localPosition, vector32 + this.desiredBob, ref this.posVel, this.posSpeed);
        quaternion1 = base.transform.parent.rotation;
        this.prevRotation = quaternion1.eulerAngles;
    }
}