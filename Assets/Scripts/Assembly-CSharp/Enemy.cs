using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float hipSpeed = 3f;

    private float headAndHandSpeed = 4f;

    private Transform target;

    public LayerMask objectsAndPlayer;

    private NavMeshAgent agent;

    private bool spottedPlayer;

    private Animator animator;

    public GameObject startGun;

    public Transform gunPosition;

    private Weapon gunScript;

    public GameObject currentGun;

    private float attackSpeed;

    private bool readyToShoot;

    private RagdollController ragdoll;

    public Transform leftArm;

    public Transform rightArm;

    public Transform head;

    public Transform hips;

    public Transform player;

    private bool takingAim;

    public PlayerMovement playerMovement;

    public Enemy()
    {
    }

    private void Awake() {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Aim()
    {
        if (this.currentGun == null)
        {
            return;
        }
        if (this.ragdoll.IsRagdoll())
        {
            return;
        }
        if (!this.animator.GetBool("Aiming"))
        {
            return;
        }
        Vector3 vector3 = this.target.transform.position - base.transform.position;
        if (Vector3.Angle(base.transform.forward, vector3) > 70f)
        {
            base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(vector3), Time.deltaTime * this.hipSpeed);
        }
        this.head.transform.rotation = Quaternion.Slerp(this.head.transform.rotation, Quaternion.LookRotation(vector3), Time.deltaTime * this.headAndHandSpeed);
        this.rightArm.transform.rotation = Quaternion.Slerp(this.head.transform.rotation, Quaternion.LookRotation(vector3), Time.deltaTime * this.headAndHandSpeed);
        this.leftArm.transform.rotation = Quaternion.Slerp(this.head.transform.rotation, Quaternion.LookRotation(vector3), Time.deltaTime * this.headAndHandSpeed);
        if (this.readyToShoot)
        {
            this.gunScript.Use(this.target.position);
            this.readyToShoot = false;
            base.Invoke("Cooldown", this.attackSpeed + UnityEngine.Random.Range(this.attackSpeed, this.attackSpeed * 5f));
        }
    }

    private void Cooldown()
    {
        this.readyToShoot = true;
    }

    public void DropGun(Vector3 dir)
    {
        if (this.gunScript == null)
        {
            return;
        }
        this.gunScript.Drop();
        Rigidbody rigidbody = this.currentGun.AddComponent<Rigidbody>();
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        this.currentGun.transform.parent = null;
        rigidbody.AddForce(dir, ForceMode.Impulse);
        float single = 10f;
        rigidbody.AddTorque(new Vector3((float)UnityEngine.Random.Range(-1, 1), (float)UnityEngine.Random.Range(-1, 1), (float)UnityEngine.Random.Range(-1, 1)) * single);
        this.gunScript = null;
    }

    private void FindPlayer()
    {
        this.FindTarget();
        if (!this.agent || !this.target)
        {
            return;
        }
        Vector3 vector3 = this.target.position - base.transform.position;
        Vector3 vector31 = vector3.normalized;
        RaycastHit[] raycastHitArray = Physics.RaycastAll(base.transform.position + vector31, vector31, (float)this.objectsAndPlayer);
        if ((int)raycastHitArray.Length < 1)
        {
            return;
        }
        bool flag = false;
        float single = 1001f;
        float single1 = 1000f;
        for (int i = 0; i < (int)raycastHitArray.Length; i++)
        {
            int num = raycastHitArray[i].collider.gameObject.layer;
            if (!(raycastHitArray[i].collider.transform.root.gameObject.name == base.gameObject.name) && num != LayerMask.NameToLayer("TransparentFX"))
            {
                if (num == LayerMask.NameToLayer("Player"))
                {
                    single = raycastHitArray[i].distance;
                    flag = true;
                }
                else if (raycastHitArray[i].distance < single1)
                {
                    single1 = raycastHitArray[i].distance;
                }
            }
        }
        if (!flag)
        {
            return;
        }
        if (single1 >= single || single == 1001f)
        {
            if (this.takingAim || this.animator.GetBool("Aiming"))
            {
                return;
            }
            if (!this.spottedPlayer)
            {
                this.spottedPlayer = true;
            }
            base.Invoke("TakeAim", UnityEngine.Random.Range(0.3f, 1f));
            this.takingAim = true;
            return;
        }
        this.readyToShoot = false;
        if (this.animator.GetBool("Running") && this.agent.remainingDistance < 0.2f)
        {
            this.animator.SetBool("Running", false);
            this.spottedPlayer = false;
        }
        if (!this.spottedPlayer || !this.agent.isOnNavMesh || this.animator.GetBool("Running"))
        {
            return;
        }
        MonoBehaviour.print("oof");
        this.takingAim = false;
        this.agent.destination = this.target.transform.position;
        this.animator.SetBool("Running", true);
        this.animator.SetBool("Aiming", false);
        this.readyToShoot = false;
    }

    private void FindTarget()
    {
        if (this.target != null)
        {
            return;
        }
        if (!playerMovement)
        {
            return;
        }
        this.target = playerMovement.playerCam;
    }

    private void GiveGun()
    {
        if (this.startGun == null)
        {
            return;
        }
        GameObject gameObject = Object.Instantiate<GameObject>(this.startGun);
        Object.Destroy(gameObject.GetComponent<Rigidbody>());
        this.gunScript = (Weapon)gameObject.GetComponent(typeof(Weapon));
        this.gunScript.PickupWeapon(false);
        gameObject.transform.parent = this.gunPosition;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        this.currentGun = gameObject;
        this.attackSpeed = this.gunScript.GetAttackSpeed();
    }

    public bool IsDead()
    {
        return this.ragdoll.IsRagdoll();
    }

    private void LateUpdate()
    {
        this.FindPlayer();
        this.Aim();
    }

    private void Start()
    {
        this.ragdoll = (RagdollController)base.GetComponent(typeof(RagdollController));
        this.animator = base.GetComponentInChildren<Animator>();
        this.agent = base.GetComponent<NavMeshAgent>();
        this.GiveGun();
    }

    private void TakeAim()
    {
        this.animator.SetBool("Running", false);
        this.animator.SetBool("Aiming", true);
        base.CancelInvoke();
        base.Invoke("Cooldown", UnityEngine.Random.Range(0.3f, 1f));
        if (this.agent && this.agent.isOnNavMesh)
        {
            this.agent.destination = base.transform.position;
        }
    }
}