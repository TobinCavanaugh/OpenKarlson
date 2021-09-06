using Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public GameObject projectile;

    public float pushBackForce;

    public float force;

    public float accuracy;

    public int bullets;

    public float boostRecoil;

    private Transform guntip;

    private Rigidbody rb;

    private Collider[] projectileColliders;

    public RangedWeapon()
    {
    }

    private void GetReady()
    {
        base.readyToUse = true;
    }

    public override void OnAim()
    {
    }

    private void Recoil()
    {
    }

    private void RemoveCollisionWithPlayer()
    {
        Collider[] colliderArray;
        colliderArray = (!this.player ? base.transform.root.GetComponentsInChildren<Collider>() : new Collider[] { PlayerMovement.Instance.GetPlayerCollider() });
        for (int i = 0; i < (int)colliderArray.Length; i++)
        {
            for (int j = 0; j < (int)this.projectileColliders.Length; j++)
            {
                Physics.IgnoreCollision(colliderArray[i], this.projectileColliders[j], true);
            }
        }
    }

    private void SpawnProjectile(Vector3 attackDirection)
    {
        Vector3 vector3 = this.guntip.position - (this.guntip.transform.right / 4f);
        Vector3 vector31 = (attackDirection - vector3).normalized;
        List<Collider> colliders = new List<Collider>();
        if (this.player)
        {
            PlayerMovement.Instance.GetRb().AddForce(base.transform.right * this.boostRecoil, ForceMode.Impulse);
        }
        for (int i = 0; i < this.bullets; i++)
        {
            Object.Instantiate<GameObject>(PrefabManager.Instance.muzzle, vector3, Quaternion.identity);
            GameObject gameObject = Object.Instantiate<GameObject>(this.projectile, vector3, base.transform.rotation);
            Rigidbody componentInChildren = gameObject.GetComponentInChildren<Rigidbody>();
            this.projectileColliders = gameObject.GetComponentsInChildren<Collider>();
            this.RemoveCollisionWithPlayer();
            componentInChildren.transform.rotation = base.transform.rotation;
            Vector3 vector32 = vector31 + (this.guntip.transform.up * UnityEngine.Random.Range(-this.accuracy, this.accuracy)) + (this.guntip.transform.forward * UnityEngine.Random.Range(-this.accuracy, this.accuracy));
            componentInChildren.AddForce(componentInChildren.mass * this.force * vector32);
            Bullet component = (Bullet)gameObject.GetComponent(typeof(Bullet));
            if (component != null)
            {
                Color color = Color.red;
                if (!this.player)
                {
                    Object.Instantiate<GameObject>(PrefabManager.Instance.gunShotAudio, base.transform.position, Quaternion.identity);
                }
                else
                {
                    color = Color.blue;
                    Gun.Instance.Shoot();
                    if (!component.explosive)
                    {
                        AudioManager.Instance.PlayPitched("GunBass", 0.3f);
                        AudioManager.Instance.PlayPitched("GunHigh", 0.3f);
                        AudioManager.Instance.PlayPitched("GunLow", 0.3f);
                    }
                    else
                    {
                        Object.Instantiate<GameObject>(PrefabManager.Instance.thumpAudio, base.transform.position, Quaternion.identity);
                    }
                    componentInChildren.AddForce(componentInChildren.mass * this.force * vector32);
                }
                component.SetBullet(this.damage, this.pushBackForce, color);
                component.player = this.player;
            }
            foreach (Collider collider in colliders)
            {
                Physics.IgnoreCollision(collider, this.projectileColliders[0]);
            }
            colliders.Add(this.projectileColliders[0]);
        }
    }

    private new void Start()
    {
        base.Start();
        this.rb = base.GetComponent<Rigidbody>();
        this.guntip = base.transform.GetChild(0);
    }

    public override void StopUse()
    {
    }

    public override void Use(Vector3 attackDirection)
    {
        if (!base.readyToUse || !base.pickedUp)
        {
            return;
        }
        this.SpawnProjectile(attackDirection);
        this.Recoil();
        base.readyToUse = false;
        base.Invoke("GetReady", this.attackSpeed);
    }
}