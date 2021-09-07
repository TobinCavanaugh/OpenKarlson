using Audio;
using EZCameraShake;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject spawnWeapon;

    private float sensitivity = 50f;

    private float sensMultiplier = 1f;

    private bool dead;

    public PhysicMaterial deadMat;

    public Transform playerCam;

    public Transform orientation;

    public Transform gun;

    private float xRotation;

    public Rigidbody rb;

    [SerializeField]
    private float moveSpeed = 4500f;

    [SerializeField]
    private float walkSpeed = 20f;

    [SerializeField]
    private float runSpeed = 10f;

    public bool grounded;

    public Transform groundChecker;

    public LayerMask whatIsGround;

    public LayerMask whatIsWallrunnable;

    private bool readyToJump;

    private float jumpCooldown = 0.25f;

    [SerializeField]
    private float jumpForce = 550f;

    private float x;

    private float y;

    private bool jumping;

    private bool sprinting;

    private bool crouching;

    public LineRenderer lr;

    private Vector3 grapplePoint;

    private SpringJoint joint;

    private Vector3 normalVector;

    private Vector3 wallNormalVector;

    private bool wallRunning;

    private Vector3 wallRunPos;

    private DetectWeapons detectWeapons;

    public ParticleSystem ps;

    private ParticleSystem.EmissionModule psEmission;

    private Collider playerCollider;

    public bool exploded;

    public bool paused;

    public LayerMask whatIsGrabbable;

    private Rigidbody objectGrabbing;

    private Vector3 previousLookdir;

    private Vector3 grabPoint;

    private float dragForce = 700000f;

    private SpringJoint grabJoint;

    private LineRenderer grabLr;

    private Vector3 myGrabPoint;

    private Vector3 myHandPoint;

    private Vector3 endPoint;

    private Vector3 grappleVel;

    private float offsetMultiplier;

    private float offsetVel;

    private float distance;

    private float slideSlowdown = 0.2f;

    private float actualWallRotation;

    private float wallRotationVel;

    private float desiredX;

    private bool cancelling;

    private bool readyToWallrun = true;

    [SerializeField]
    private float wallRunGravity = 1f;

    private float maxSlopeAngle = 35f;

    private float wallRunRotation;

    private bool airborne;

    private int nw;

    private bool onWall;

    private bool onGround;

    private bool surfing;

    private bool cancellingGrounded;

    private bool cancellingWall;

    private bool cancellingSurf;

    public LayerMask whatIsHittable;

    private float desiredTimeScale = 1f;

    private float timeScaleVel;

    private float actionMeter;

    private float vel;

        //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;

    public Game game;
    public CameraShaker cameraShake;

    public static PlayerMovement Instance
    {
        get;
        private set;
    }

    public PlayerMovement()
    {
    }

    private void Awake()
    {
        PlayerMovement.Instance = this;
        this.rb = base.GetComponent<Rigidbody>();
    }

    private void CameraShake()
    {
        float single = this.rb.velocity.magnitude / 9f;
        cameraShake.ShakeOnce(single, 0.1f * single, 0.25f, 0.2f);
        base.Invoke("CameraShake", 0.2f);
    }

    private void CancelWallrun()
    {
        MonoBehaviour.print("cancelled");
        base.Invoke("GetReadyToWallrun", 0.1f);
        this.rb.AddForce(this.wallNormalVector * 600f);
        this.readyToWallrun = false;
        AudioManager.Instance.PlayLanding();
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        Vector3 vector3;
        if (!this.grounded || this.jumping || this.exploded)
        {
            return;
        }
        float single = 0.16f;
        float single1 = 0.01f;
        if (this.crouching)
        {
            Rigidbody rigidbody = this.rb;
            float single2 = this.moveSpeed * Time.deltaTime;
            vector3 = this.rb.velocity;
            rigidbody.AddForce((single2 * -vector3.normalized) * this.slideSlowdown);
            return;
        }
        if (Math.Abs(mag.x) > single1 && Math.Abs(x) < 0.05f || mag.x < -single1 && x > 0f || mag.x > single1 && x < 0f)
        {
            this.rb.AddForce((((this.moveSpeed * this.orientation.transform.right) * Time.deltaTime) * -mag.x) * single);
        }
        if (Math.Abs(mag.y) > single1 && Math.Abs(y) < 0.05f || mag.y < -single1 && y > 0f || mag.y > single1 && y < 0f)
        {
            this.rb.AddForce((((this.moveSpeed * this.orientation.transform.forward) * Time.deltaTime) * -mag.y) * single);
        }
        if (Mathf.Sqrt(Mathf.Pow(this.rb.velocity.x, 2f) + Mathf.Pow(this.rb.velocity.z, 2f)) > this.walkSpeed)
        {
            float single3 = this.rb.velocity.y;
            vector3 = this.rb.velocity;
            Vector3 vector31 = vector3.normalized * this.walkSpeed;
            this.rb.velocity = new Vector3(vector31.x, single3, vector31.z);
        }
    }

    private void DrawGrabbing()
    {
        if (!this.objectGrabbing)
        {
            return;
        }
        this.myGrabPoint = Vector3.Lerp(this.myGrabPoint, this.objectGrabbing.position, Time.deltaTime * 45f);
        this.myHandPoint = Vector3.Lerp(this.myHandPoint, this.grabJoint.connectedAnchor, Time.deltaTime * 45f);
        this.grabLr.SetPosition(0, this.myGrabPoint);
        this.grabLr.SetPosition(1, this.myHandPoint);
    }

    private void DrawGrapple()
    {
        //if (this.grapplePoint == Vector3.zero || this.joint == null)
        //{
        //    this.lr.positionCount = 0;
        //    return;
        //}
        //this.lr.positionCount = 2;
        //this.endPoint = Vector3.Lerp(this.endPoint, this.grapplePoint, Time.deltaTime * 15f);
        //this.offsetMultiplier = Mathf.SmoothDamp(this.offsetMultiplier, 0f, ref this.offsetVel, 0.1f);
        //int num = 100;
        //this.lr.positionCount = num;
        //Vector3 child = this.gun.transform.GetChild(0).position;
        //float single = Vector3.Distance(this.endPoint, child);
        //this.lr.SetPosition(0, child);
        //this.lr.SetPosition(num - 1, this.endPoint);
        //float single1 = single;
        //float single2 = 1f;
        //for (int i = 1; i < num - 1; i++)
        //{
        //    float single3 = (float)i / (float)num;
        //    float single4 = single3 * this.offsetMultiplier;
        //    float single5 = (Mathf.Sin(single4 * single1) - 0.5f) * single2 * (single4 * 2f);
        //    Vector3 vector3 = (this.endPoint - child).normalized;
        //    float single6 = Mathf.Sin(single3 * 180f * 0.0174532924f);
        //    float single7 = Mathf.Cos(this.offsetMultiplier * 90f * 0.0174532924f);
//
//
        //    //Vector3 vector31 = (child + (((this.endPoint - child) / (float)num) * (float)i)) + single7 * single5 * Vector2.Perpendicular(vector3) + (this.offsetMultiplier * single6 * Vector3.down);
        //    Vector3 vector31 = ((child + ((vector3 / (float)num) + (float)i)) + (single7 * single5 * Vector2.Perpendicular(vector3)) + (this.offsetMultiplier * single6 * Vector3.down));
        //    this.lr.SetPosition(i, vector31);
        //}
    }

    public void Explode()
    {
        this.exploded = true;
        base.Invoke("StopExplosion", 0.1f);
    }

    public Vector2 FindVelRelativeToLook()
    {
        float single = Mathf.Atan2(this.rb.velocity.x, this.rb.velocity.z) * 57.29578f;
        float single1 = Mathf.DeltaAngle(this.orientation.transform.eulerAngles.y, single);
        float single2 = 90f - single1;
        float single3 = this.rb.velocity.magnitude;
        float single4 = single3 * Mathf.Cos(single1 * 0.0174532924f);
        return new Vector2(single3 * Mathf.Cos(single2 * 0.0174532924f), single4);
    }

    private void FindWallRunRotation()
    {
        if (!this.wallRunning)
        {
            this.wallRunRotation = 0f;
            return;
        }
        Vector3 vector3 = new Vector3(0f, this.playerCam.transform.rotation.y, 0f);
        Vector3 vector31 = vector3.normalized;
        Vector3 vector32 = new Vector3(0f, 0f, 1f);
        float single = 0f;
        float single1 = this.playerCam.transform.rotation.eulerAngles.y;
        if (Math.Abs(this.wallNormalVector.x - 1f) < 0.1f)
        {
            single = 90f;
        }
        else if (Math.Abs(this.wallNormalVector.x - -1f) < 0.1f)
        {
            single = 270f;
        }
        else if (Math.Abs(this.wallNormalVector.z - 1f) < 0.1f)
        {
            single = 0f;
        }
        else if (Math.Abs(this.wallNormalVector.z - -1f) < 0.1f)
        {
            single = 180f;
        }
        single = Vector3.SignedAngle(new Vector3(0f, 0f, 1f), this.wallNormalVector, Vector3.up);
        float single2 = Mathf.DeltaAngle(single1, single);
        this.wallRunRotation = -(single2 / 90f) * 15f;
        if (!this.readyToWallrun)
        {
            return;
        }
        if ((Mathf.Abs(this.wallRunRotation) >= 4f || this.y <= 0f || Math.Abs(this.x) >= 0.1f) && (Mathf.Abs(this.wallRunRotation) <= 22f || this.y >= 0f || Math.Abs(this.x) >= 0.1f))
        {
            this.cancelling = false;
            base.CancelInvoke("CancelWallrun");
            return;
        }
        if (this.cancelling)
        {
            return;
        }
        this.cancelling = true;
        base.CancelInvoke("CancelWallrun");
        base.Invoke("CancelWallrun", 0.2f);
    }

    private void FixedUpdate()
    {
        if (this.dead || /** game.done || **/ this.paused)
        {
            return;
        }
        this.Movement();
    }

    private void FootSteps()
    {
        if (this.crouching || this.dead)
        {
            return;
        }
        if (this.grounded || this.wallRunning)
        {
            float single = 1.2f;
            float single1 = this.rb.velocity.magnitude;
            if (single1 > 20f)
            {
                single1 = 20f;
            }
            this.distance += single1;
            if (this.distance > 300f / single)
            {
                AudioManager.Instance.PlayFootStep();
                this.distance = 0f;
            }
        }
    }

    public float GetActionMeter()
    {
        return this.actionMeter * 22000f;
    }

    public float GetFallSpeed()
    {
        return this.rb.velocity.y;
    }

    public Vector3 GetGrapplePoint()
    {
        return this.detectWeapons.GetGrapplerPoint();
    }

    public Transform GetPlayerCamTransform()
    {
        return this.playerCam.transform;
    }

    public Collider GetPlayerCollider()
    {
        return this.playerCollider;
    }

    public Rigidbody GetRb()
    {
        return this.rb;
    }

    private void GetReadyToWallrun()
    {
        this.readyToWallrun = true;
    }

    public float GetRecoil()
    {
        return this.detectWeapons.GetRecoil();
    }

    public Vector3 GetVelocity()
    {
        return this.rb.velocity;
    }

    private void GrabObject()
    {
        if (this.objectGrabbing == null)
        {
            this.StartGrab();
            return;
        }
        this.HoldGrab();
    }

    public bool HasGun()
    {
        return this.detectWeapons.HasGun();
    }

    public Vector3 HitPoint()
    {
        RaycastHit[] raycastHitArray = Physics.RaycastAll(this.playerCam.transform.position, this.playerCam.transform.forward, (float)this.whatIsHittable);
        if ((int)raycastHitArray.Length < 1)
        {
            return this.playerCam.transform.position + (this.playerCam.transform.forward * 100f);
        }
        if ((int)raycastHitArray.Length > 1)
        {
            for (int i = 0; i < (int)raycastHitArray.Length; i++)
            {
                if (raycastHitArray[i].transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    return raycastHitArray[i].point;
                }
            }
        }
        return raycastHitArray[0].point;
    }

    private void HoldGrab()
    {
        this.grabJoint.connectedAnchor = this.playerCam.transform.position + (this.playerCam.transform.forward * 5.5f);
        this.grabLr.startWidth = 0f;
        this.grabLr.endWidth = 0.0075f * this.objectGrabbing.velocity.magnitude;
        this.previousLookdir = this.playerCam.transform.forward;
    }

    public bool IsCrouching()
    {
        return this.crouching;
    }

    public bool IsDead()
    {
        return this.dead;
    }

    private bool IsFloor(Vector3 v)
    {
        return Vector3.Angle(Vector3.up, v) < this.maxSlopeAngle;
    }

    private bool IsRoof(Vector3 v)
    {
        return v.y == -1f;
    }

    private bool IsSurf(Vector3 v)
    {
        float single = Vector3.Angle(Vector3.up, v);
        if (single >= 89f)
        {
            return false;
        }
        return single > this.maxSlopeAngle;
    }

    private bool IsWall(Vector3 v)
    {
        return Math.Abs(90f - Vector3.Angle(Vector3.up, v)) < 0.1f;
    }

    private void Jump()
    {
        if ((this.grounded || this.wallRunning || this.surfing) && this.readyToJump)
        {
            MonoBehaviour.print("jumping");
            Vector3 vector3 = this.rb.velocity;
            this.readyToJump = false;
            this.rb.AddForce((Vector2.up * this.jumpForce) * 1.5f);
            this.rb.AddForce((this.normalVector * this.jumpForce) * 0.5f);
            if (this.rb.velocity.y < 0.5f)
            {
                this.rb.velocity = new Vector3(vector3.x, 0f, vector3.z);
            }
            else if (this.rb.velocity.y > 0f)
            {
                this.rb.velocity = new Vector3(vector3.x, vector3.y / 2f, vector3.z);
            }
            if (this.wallRunning)
            {
                this.rb.AddForce((this.wallNormalVector * this.jumpForce) * 3f);
            }
            base.Invoke("ResetJump", this.jumpCooldown);
            if (this.wallRunning)
            {
                this.wallRunning = false;
            }
            AudioManager.Instance.PlayJump();
        }
    }

    private void KillEnemy(Collision other)
    {
        Vector3 vector3;
        if (this.grounded && !this.crouching)
        {
            return;
        }
        if (this.rb.velocity.magnitude < 3f)
        {
            return;
        }
        Enemy component = (Enemy)other.transform.root.GetComponent(typeof(Enemy));
        if (!component)
        {
            return;
        }
        if (component.IsDead())
        {
            return;
        }
        Object.Instantiate<GameObject>(PrefabManager.Instance.enemyHitAudio, other.contacts[0].point, Quaternion.identity);
        RagdollController ragdollController = (RagdollController)other.transform.root.GetComponent(typeof(RagdollController));
        if (!this.grounded || !this.crouching)
        {
            vector3 = this.rb.velocity;
            ragdollController.MakeRagdoll(vector3.normalized * 250f);
        }
        else
        {
            ragdollController.MakeRagdoll((this.rb.velocity * 1.2f) * 34f);
        }
        Rigidbody rigidbody = this.rb;
        vector3 = this.rb.velocity;
        rigidbody.AddForce(vector3.normalized * 2f, ForceMode.Impulse);
        vector3 = this.rb.velocity;
        component.DropGun(vector3.normalized * 2f);
    }

    public void KillPlayer()
    {
        if (Game.Instance.done)
        {
            return;
        }
        cameraShake.ShakeOnce(3f * GameState.Instance.cameraShake, 2f, 0.1f, 0.6f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManger.Instance.DeadUI(true);
        Timer.Instance.Stop();
        this.dead = true;
        this.rb.freezeRotation = false;
        this.playerCollider.material = this.deadMat;
        this.detectWeapons.Throw(Vector3.zero);
        this.paused = false;
        this.ResetSlowmo();
    }

    private void LateUpdate()
    {
        if (this.dead || this.paused)
        {
            return;
        }
        this.DrawGrapple();
        this.DrawGrabbing();
        this.WallRunning();
    }

    private void Look()
    {
        float axis = Input.GetAxis("Mouse X") * this.sensitivity * Time.fixedDeltaTime * this.sensMultiplier;
        float single = Input.GetAxis("Mouse Y") * this.sensitivity * Time.fixedDeltaTime * this.sensMultiplier;
        Quaternion quaternion = this.playerCam.transform.localRotation;
        this.desiredX = quaternion.eulerAngles.y + axis;
        this.xRotation -= single;
        this.xRotation = Mathf.Clamp(this.xRotation, -90f, 90f);
        this.FindWallRunRotation();
        this.actualWallRotation = Mathf.SmoothDamp(this.actualWallRotation, this.wallRunRotation, ref this.wallRotationVel, 0.2f);
        this.playerCam.transform.localRotation = Quaternion.Euler(this.xRotation, this.desiredX, this.actualWallRotation);
        this.orientation.transform.localRotation = Quaternion.Euler(0f, this.desiredX, 0f);
    }

    private void Movement()
    {
        if (this.dead)
        {
            return;
        }
        this.rb.AddForce((Vector3.down * Time.deltaTime) * 10f);
        Vector2 look = this.FindVelRelativeToLook();
        float single = look.x;
        float single1 = look.y;
        this.FootSteps();
        this.CounterMovement(this.x, this.y, look);
        if (this.readyToJump && this.jumping)
        {
            this.Jump();
        }
        float single2 = this.walkSpeed;
        if (this.sprinting)
        {
            single2 = this.runSpeed;
        }
        if (this.crouching && this.grounded && this.readyToJump)
        {
            this.rb.AddForce((Vector3.down * Time.deltaTime) * 3000f);
            return;
        }
        if (this.x > 0f && single > single2)
        {
            this.x = 0f;
        }
        if (this.x < 0f && single < -single2)
        {
            this.x = 0f;
        }
        if (this.y > 0f && single1 > single2)
        {
            this.y = 0f;
        }
        if (this.y < 0f && single1 < -single2)
        {
            this.y = 0f;
        }
        float single3 = 1f;
        float single4 = 1f;
        if (!this.grounded)
        {
            single3 = 0.5f;
            single4 = 0.5f;
        }
        if (this.grounded && this.crouching)
        {
            single4 = 0f;
        }
        if (this.wallRunning)
        {
            single4 = 0.3f;
            single3 = 0.3f;
        }
        if (this.surfing)
        {
            single3 = 0.7f;
            single4 = 0.3f;
        }
        this.rb.AddForce(((((this.orientation.transform.forward * this.y) * this.moveSpeed) * Time.deltaTime) * single3) * single4);
        this.rb.AddForce((((this.orientation.transform.right * this.x) * this.moveSpeed) * Time.deltaTime) * single3);
        this.SpeedLines();
    }

    private void MyInput()
    {
        if (this.dead || Game.Instance.done)
        {
            return;
        }
        this.x = Input.GetAxisRaw("Horizontal");
        this.y = Input.GetAxisRaw("Vertical");
        this.jumping = Input.GetButton("Jump");
        this.crouching = Input.GetButton("Crouch");
        if (Input.GetButtonDown("Cancel"))
        {
            this.Pause();
        }
        if (this.paused)
        {
            return;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            this.StartCrouch();
        }
        if (Input.GetButtonUp("Crouch"))
        {
            this.StopCrouch();
        }
        if (Input.GetButton("Fire1"))
        {
            if (!this.detectWeapons.HasGun())
            {
                this.GrabObject();
            }
            else
            {
                this.detectWeapons.Shoot(this.HitPoint());
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            this.detectWeapons.StopUse();
            if (this.objectGrabbing)
            {
                this.StopGrab();
            }
        }
        if (Input.GetButtonDown("Pickup"))
        {
            this.detectWeapons.Pickup();
        }
        if (Input.GetButtonDown("Drop"))
        {
            DetectWeapons detectWeapon = this.detectWeapons;
            Vector3 vector3 = this.HitPoint() - this.detectWeapons.weaponPos.position;
            detectWeapon.Throw(vector3.normalized);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.KillEnemy(other);
        }
    }

    private void OnCollisionExit(Collision other)
    {
    }

    private void OnCollisionStay(Collision other)
    {
        int num = other.gameObject.layer;
        if (this.whatIsGround != (this.whatIsGround | 1 << (num & 31)))
        {
            return;
        }
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 vector3 = other.contacts[i].normal;
            if (this.IsFloor(vector3))
            {
                if (this.wallRunning)
                {
                    this.wallRunning = false;
                }
                if (!this.grounded && this.crouching)
                {
                    AudioManager.Instance.Play("StartSlide");
                    AudioManager.Instance.Play("Slide");
                }
                this.grounded = true;
                this.normalVector = vector3;
                this.cancellingGrounded = false;
                base.CancelInvoke("StopGrounded");
            }
            if (this.IsWall(vector3) && num == LayerMask.NameToLayer("Ground"))
            {
                if (!this.onWall)
                {
                    AudioManager.Instance.Play("StartSlide");
                    AudioManager.Instance.Play("Slide");
                }
                this.StartWallRun(vector3);
                this.onWall = true;
                this.cancellingWall = false;
                base.CancelInvoke("StopWall");
            }
            if (this.IsSurf(vector3))
            {
                this.surfing = true;
                this.cancellingSurf = false;
                base.CancelInvoke("StopSurf");
            }
            this.IsRoof(vector3);
        }
        float single = 3f;
        if (!this.cancellingGrounded)
        {
            this.cancellingGrounded = true;
            base.Invoke("StopGrounded", Time.deltaTime * single);
        }
        if (!this.cancellingWall)
        {
            this.cancellingWall = true;
            base.Invoke("StopWall", Time.deltaTime * single);
        }
        if (!this.cancellingSurf)
        {
            this.cancellingSurf = true;
            base.Invoke("StopSurf", Time.deltaTime * single);
        }
    }

    private void Pause()
    {
        if (this.dead)
        {
            return;
        }
        if (this.paused)
        {
            Time.timeScale = 1f;
            UIManger.Instance.DeadUI(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.paused = false;
            return;
        }
        this.paused = true;
        Time.timeScale = 0f;
        UIManger.Instance.DeadUI(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResetJump()
    {
        this.readyToJump = true;
    }

    private void ResetSlowmo()
    {
        this.desiredTimeScale = 1f;
        AudioManager.Instance.Play("SlowmoEnd");
    }

    public void Respawn()
    {
        this.detectWeapons.StopUse();
    }

    public void Slowmo(float timescale, float length)
    {
        if (!GameState.Instance.slowmo)
        {
            return;
        }
        base.CancelInvoke("Slowmo");
        this.desiredTimeScale = timescale;
        base.Invoke("ResetSlowmo", length);
        AudioManager.Instance.Play("SlowmoStart");
    }

    private void SpeedLines()
    {
        float single = Vector3.Angle(this.rb.velocity, this.playerCam.transform.forward) * 0.15f;
        if (single < 1f)
        {
            single = 1f;
        }
        float single1 = this.rb.velocity.magnitude / single;
        if (this.grounded && !this.wallRunning)
        {
            single1 = 0f;
        }
        this.psEmission.rateOverTimeMultiplier = single1;
    }

    private void Start()
    {
        this.psEmission = this.ps.emission;
        this.playerCollider = base.GetComponent<Collider>();
        this.detectWeapons = (DetectWeapons)base.GetComponentInChildren(typeof(DetectWeapons));
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.readyToJump = true;
        this.wallNormalVector = Vector3.up;
        playerScale =  transform.localScale;

        this.CameraShake();
        if (this.spawnWeapon != null)
        {
            GameObject gameObject = Object.Instantiate<GameObject>(this.spawnWeapon, base.transform.position, Quaternion.identity);
            this.detectWeapons.ForcePickup(gameObject);
        }
        this.UpdateSensitivity();
    }

    private void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f) {
            if (grounded) {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StartGrab()
    {
        RaycastHit[] raycastHitArray = Physics.RaycastAll(this.playerCam.transform.position, this.playerCam.transform.forward, 8f, this.whatIsGrabbable);
        if ((int)raycastHitArray.Length < 1)
        {
            return;
        }
        for (int i = 0; i < (int)raycastHitArray.Length; i++)
        {
            MonoBehaviour.print(String.Concat((object)"testing on: ", raycastHitArray[i].collider.gameObject.layer));
            if (raycastHitArray[i].transform.GetComponent<Rigidbody>())
            {
                this.objectGrabbing = raycastHitArray[i].transform.GetComponent<Rigidbody>();
                this.grabPoint = raycastHitArray[i].point;
                this.grabJoint = this.objectGrabbing.gameObject.AddComponent<SpringJoint>();
                this.grabJoint.autoConfigureConnectedAnchor = false;
                this.grabJoint.minDistance = 0f;
                this.grabJoint.maxDistance = 0f;
                this.grabJoint.damper = 4f;
                this.grabJoint.spring = 40f;
                this.grabJoint.massScale = 5f;
                this.objectGrabbing.angularDrag = 5f;
                this.objectGrabbing.drag = 1f;
                this.previousLookdir = this.playerCam.transform.forward;
                this.grabLr = this.objectGrabbing.gameObject.AddComponent<LineRenderer>();
                this.grabLr.positionCount = 2;
                this.grabLr.startWidth = 0.05f;
                this.grabLr.material = new Material(Shader.Find("Sprites/Default"));
                this.grabLr.numCapVertices = 10;
                this.grabLr.numCornerVertices = 10;
                return;
            }
        }
    }

    private void StartWallRun(Vector3 normal)
    {
        if (this.grounded || !this.readyToWallrun)
        {
            return;
        }
        this.wallNormalVector = normal;
        float single = 20f;
        if (!this.wallRunning)
        {
            this.rb.velocity = new Vector3(this.rb.velocity.x, 0f, this.rb.velocity.z);
            this.rb.AddForce(Vector3.up * single, ForceMode.Impulse);
        }
        this.wallRunning = true;
    }

    private void StopCrouch()
    {
        base.transform.localScale = new Vector3(1f, 1.5f, 1f);
        base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, base.transform.position.z);
    }

    private void StopExplosion()
    {
        this.exploded = false;
    }

    private void StopGrab()
    {
        Object.Destroy(this.grabJoint);
        Object.Destroy(this.grabLr);
        this.objectGrabbing.angularDrag = 0.05f;
        this.objectGrabbing.drag = 0f;
        this.objectGrabbing = null;
    }

    private void StopGrounded()
    {
        this.grounded = false;
    }

    private void StopSurf()
    {
        this.surfing = false;
    }

    private void StopWall()
    {
        this.onWall = false;
        this.wallRunning = false;
    }

    private void Update()
    {
        this.UpdateActionMeter();
        this.MyInput();
        if (this.dead || Game.Instance.done || this.paused)
        {
            return;
        }
        this.Look();
        this.DrawGrabbing();
        this.UpdateTimescale();
        if (base.transform.position.y < -200f)
        {
            this.KillPlayer();
        }
    }

    private void UpdateActionMeter()
    {
        float single = 0.09f;
        if (this.rb.velocity.magnitude > 15f && (!this.dead || !Game.Instance.playing))
        {
            single = 1f;
        }
        this.actionMeter = Mathf.SmoothDamp(this.actionMeter, single, ref this.vel, 0.7f);
    }

    public void UpdateSensitivity()
    {
        if (!GameState.Instance)
        {
            return;
        }
        this.sensMultiplier = GameState.Instance.GetSensitivity();
    }

    private void UpdateTimescale()
    {
        if (Game.Instance.done || this.paused || this.dead)
        {
            return;
        }
        Time.timeScale = Mathf.SmoothDamp(Time.timeScale, this.desiredTimeScale, ref this.timeScaleVel, 0.15f);
    }

    private void WallRunning()
    {
        if (this.wallRunning)
        {
            this.rb.AddForce((-this.wallNormalVector * Time.deltaTime) * this.moveSpeed);
            this.rb.AddForce((((Vector3.up * Time.deltaTime) * this.rb.mass) * 100f) * this.wallRunGravity);
        }
    }
}