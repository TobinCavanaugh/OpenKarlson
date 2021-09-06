using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Pickup : MonoBehaviour, IPickup
{
    protected bool player;

    private bool thrown;

    public float recoil;

    private Transform outline;

    public bool pickedUp
    {
        get;
        set;
    }

    public bool readyToUse
    {
        get;
        set;
    }

    protected Pickup()
    {
    }

    private void Awake()
    {
        this.readyToUse = true;
        this.outline = base.transform.GetChild(1);
    }

    public void Drop()
    {
        this.readyToUse = true;
        base.Invoke("DropWeapon", 0.5f);
        this.thrown = true;
    }

    private void DropWeapon()
    {
        base.CancelInvoke();
        this.pickedUp = false;
        this.outline.gameObject.SetActive(true);
    }

    public bool IsPickedUp()
    {
        return this.pickedUp;
    }

    public abstract void OnAim();

    private void OnCollisionEnter(Collision other)
    {
        if (!this.thrown)
        {
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Object.Instantiate<GameObject>(PrefabManager.Instance.enemyHitAudio, other.contacts[0].point, Quaternion.identity);
            ((RagdollController)other.transform.root.GetComponent(typeof(RagdollController))).MakeRagdoll(-base.transform.right * 60f);
            Rigidbody component = other.gameObject.GetComponent<Rigidbody>();
            if (component)
            {
                component.AddForce(-base.transform.right * 1500f);
            }
            ((Enemy)other.transform.root.GetComponent(typeof(Enemy))).DropGun(Vector3.up);
        }
        this.thrown = false;
    }

    public void PickupWeapon(bool player)
    {
        this.pickedUp = true;
        this.player = player;
        this.outline.gameObject.SetActive(false);
    }

    public abstract void StopUse();

    private void Update()
    {
        bool flag = this.pickedUp;
    }

    public abstract void Use(Vector3 attackDirection);
}