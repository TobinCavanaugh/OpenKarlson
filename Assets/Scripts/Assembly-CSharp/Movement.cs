using Audio;
using EZCameraShake;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
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

    private float moveSpeed = 4500f;

    private float walkSpeed = 20f;

    private float runSpeed = 10f;

    public bool grounded;

    public Transform groundChecker;

    public LayerMask whatIsGround;

    private bool readyToJump;

    private float jumpCooldown = 0.2f;

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

    private float actualWallRotation;

    private float wallRotationVel;

    private float desiredX;

    private float wallRunRotation;

    private bool airborne;

    private bool touchingGround;

    public LayerMask whatIsHittable;

    private float desiredTimeScale = 1f;

    private float timeScaleVel;

    public static Movement Instance
    {
        get;
        private set;
    }

    public Movement()
    {
    }

    private void Awake()
    {
        Movement.Instance = this;
        this.rb = base.GetComponent<Rigidbody>();
        MonoBehaviour.print(String.Concat((object)"rb: ", this.rb));
    }

    private void CameraShake()
    {
        float single = this.rb.velocity.magnitude / 9f;
        CameraShaker.Instance.ShakeOnce(single, 0.1f * single, 0.25f, 0.2f);
        base.Invoke("CameraShake", 0.2f);
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        Vector3 vector3;
        if (!this.grounded)
        {
            return;
        }
        float single = 0.2f;
        if (this.crouching)
        {
            Rigidbody rigidbody = this.rb;
            float single1 = this.moveSpeed * Time.deltaTime;
            vector3 = this.rb.velocity;
            rigidbody.AddForce((single1 * -vector3.normalized) * 0.045f);
            return;
        }
        if (Math.Abs(x) < 0.05f || mag.x < 0f && x > 0f || mag.x > 0f && x < 0f)
        {
            this.rb.AddForce((((this.moveSpeed * this.orientation.transform.right) * Time.deltaTime) * -mag.x) * single);
        }
        if (Math.Abs(y) < 0.05f || mag.y < 0f && y > 0f || mag.y > 0f && y < 0f)
        {
            this.rb.AddForce((((this.moveSpeed * this.orientation.transform.forward) * Time.deltaTime) * -mag.y) * single);
        }
        if (Mathf.Sqrt(Mathf.Pow(this.rb.velocity.x, 2f) + Mathf.Pow(this.rb.velocity.z, 2f)) > 20f)
        {
            float single2 = this.rb.velocity.y;
            vector3 = this.rb.velocity;
            Vector3 vector31 = vector3.normalized * 20f;
            this.rb.velocity = new Vector3(vector31.x, single2, vector31.z);
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
        //    Vector3 vector31 = (child + (((this.endPoint - child) / (float)num) * (float)i)) + single7 * single5 * Vector2.Perpendicular(vector3) + (this.offsetMultiplier * single6 * Vector3.down);
        //    this.lr.SetPosition(i, vector31);
        //}
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
    }

    private void FixedUpdate()
    {
        if (this.dead || Game.Instance.done || this.paused)
        {
            return;
        }
        this.Move();
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

    private void Jump()
    {
        if (this.grounded || this.wallRunning)
        {
            Vector3 vector3 = this.rb.velocity;
            this.rb.velocity = new Vector3(vector3.x, 0f, vector3.z);
            this.readyToJump = false;
            this.rb.AddForce((Vector2.up * this.jumpForce) * 1.5f);
            this.rb.AddForce((this.wallNormalVector * this.jumpForce) * 0.5f);
            if (this.wallRunning)
            {
                this.rb.AddForce((this.wallNormalVector * this.jumpForce) * 1.5f);
            }
            base.Invoke("ResetJump", this.jumpCooldown);
            if (this.wallRunning)
            {
                this.wallRunning = false;
            }
            AudioManager.Instance.PlayJump();
        }
    }

    public void KillPlayer()
    {
        if (Game.Instance.done)
        {
            return;
        }
        CameraShaker.Instance.ShakeOnce(3f * GameState.Instance.cameraShake, 2f, 0.1f, 0.6f);
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

    private void Move()
    {
        if (this.dead)
        {
            return;
        }
        this.grounded = Physics.OverlapSphere(this.groundChecker.position, 0.1f, this.whatIsGround).Length != 0;
        if (!this.touchingGround)
        {
            this.grounded = false;
        }
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
            this.rb.AddForce((Vector3.down * Time.deltaTime) * 2000f);
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
        }
        if (this.grounded && this.crouching)
        {
            single4 = 0f;
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
        this.sprinting = Input.GetKey(KeyCode.LeftShift);
        this.crouching = Input.GetKey(KeyCode.LeftControl);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.StartCrouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.StopCrouch();
        }
        if (Input.GetKey(KeyCode.Mouse0))
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
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            this.detectWeapons.StopUse();
            if (this.objectGrabbing)
            {
                this.StopGrab();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.detectWeapons.Pickup();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DetectWeapons detectWeapon = this.detectWeapons;
            Vector3 vector3 = this.HitPoint() - this.detectWeapons.weaponPos.position;
            detectWeapon.Throw(vector3.normalized);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.Pause();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 vector3;
        int num = other.gameObject.layer;
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            CameraShaker.Instance.ShakeOnce(5.5f * GameState.Instance.cameraShake, 1.2f, 0.2f, 0.3f);
            if (this.wallRunning && other.contacts[0].normal.y == -1f)
            {
                MonoBehaviour.print("ROOF");
                return;
            }
            this.wallNormalVector = other.contacts[0].normal;
            MonoBehaviour.print(String.Concat((object)"nv: ", this.wallNormalVector));
            AudioManager.Instance.PlayLanding();
            if (Math.Abs(this.wallNormalVector.y) < 0.1f)
            {
                this.StartWallRun();
            }
            this.airborne = false;
        }
        if (num == LayerMask.NameToLayer("Enemy"))
        {
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
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (Math.Abs(this.wallNormalVector.y) >= 0.1f)
            {
                this.touchingGround = false;
            }
            else
            {
                MonoBehaviour.print("oof");
                this.wallRunning = false;
                this.wallNormalVector = Vector3.up;
            }
            this.airborne = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            this.touchingGround = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && Math.Abs(other.contacts[0].normal.y) > 0.1f)
        {
            this.touchingGround = true;
            this.wallRunning = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            this.touchingGround = true;
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
        MonoBehaviour.print("reset");
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
        if (!GameState.Instance.shake)
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
        this.CameraShake();
        if (this.spawnWeapon != null)
        {
            GameObject gameObject = Object.Instantiate<GameObject>(this.spawnWeapon, base.transform.position, Quaternion.identity);
            this.detectWeapons.ForcePickup(gameObject);
        }
        if (GameState.Instance)
        {
            this.sensMultiplier = GameState.Instance.GetSensitivity();
        }
    }

    private void StartCrouch()
    {
        float single = 400f;
        base.transform.localScale = new Vector3(1f, 0.5f, 1f);
        base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.5f, base.transform.position.z);
        if (this.rb.velocity.magnitude > 0.1f && this.grounded)
        {
            this.rb.AddForce(this.orientation.transform.forward * single);
            AudioManager.Instance.Play("StartSlide");
            AudioManager.Instance.Play("Slide");
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

    private void StartGrapple()
    {
        RaycastHit[] raycastHitArray = Physics.RaycastAll(this.playerCam.transform.position, this.playerCam.transform.forward, 70f, this.whatIsGround);
        if ((int)raycastHitArray.Length < 1)
        {
            return;
        }
        this.grapplePoint = raycastHitArray[0].point;
        this.joint = base.gameObject.AddComponent<SpringJoint>();
        this.joint.autoConfigureConnectedAnchor = false;
        this.joint.connectedAnchor = this.grapplePoint;
        this.joint.spring = 6.5f;
        this.joint.damper = 2f;
        this.joint.maxDistance = Vector2.Distance(this.grapplePoint, base.transform.position) * 0.8f;
        this.joint.minDistance = Vector2.Distance(this.grapplePoint, base.transform.position) * 0.25f;
        this.joint.spring = 4.5f;
        this.joint.damper = 7f;
        this.joint.massScale = 4.5f;
        this.endPoint = this.gun.transform.GetChild(0).position;
        this.offsetMultiplier = 2f;
    }

    private void StartWallRun()
    {
        if (this.wallRunning)
        {
            MonoBehaviour.print("stopping since wallrunning");
            return;
        }
        if (this.touchingGround)
        {
            MonoBehaviour.print("stopping since grounded");
            return;
        }
        MonoBehaviour.print("got through");
        float single = 20f;
        this.wallRunning = true;
        this.rb.velocity = new Vector3(this.rb.velocity.x, 0f, this.rb.velocity.z);
        this.rb.AddForce(Vector3.up * single, ForceMode.Impulse);
    }

    private void StopCrouch()
    {
        base.transform.localScale = new Vector3(1f, 1.5f, 1f);
        base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, base.transform.position.z);
    }

    private void StopGrab()
    {
        Object.Destroy(this.grabJoint);
        Object.Destroy(this.grabLr);
        this.objectGrabbing.angularDrag = 0.05f;
        this.objectGrabbing.drag = 0f;
        this.objectGrabbing = null;
    }

    private void StopGrapple()
    {
        Object.Destroy(this.joint);
        this.grapplePoint = Vector3.zero;
    }

    private void Update()
    {
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
        }
    }
}