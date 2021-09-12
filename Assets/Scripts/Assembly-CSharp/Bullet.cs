using Audio;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool changeCol;

    public bool player;

    private float damage;

    private float push;

    private bool done;

    private Color col;

    public bool explosive;

    private GameObject limbHit;

    private Rigidbody rb;

    public PrefabManager manager;

    public PlayerMovement movement;
    
    public AudioManager audioManager;

    public Bullet()
    {
    }

    private void BulletExplosion(ContactPoint contact)
    {
        Vector3 vector3 = contact.point;
        Vector3 vector31 = contact.normal;
        ParticleSystem component = Object.Instantiate<GameObject>(manager.bulletDestroy, vector3 + (vector31 * 0.05f), Quaternion.identity).GetComponent<ParticleSystem>();
        component.transform.rotation = Quaternion.LookRotation(vector31);
        component.startColor = Color.blue;
    }

    private void HitPlayer(GameObject other)
    {
        movement.KillPlayer();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (this.done)
        { 
            return;
        }

        this.done = true;
        if (this.explosive)
        {
            Object.Destroy(base.gameObject);
            ((Explosion)Object.Instantiate<GameObject>(manager.explosion, other.contacts[0].point, Quaternion.identity).GetComponentInChildren(typeof(Explosion))).player = this.player;
            return;
        }
        this.BulletExplosion(other.contacts[0]);
        Object.Instantiate<GameObject>(manager.bulletHitAudio, other.contacts[0].point, Quaternion.identity);
        int num = other.gameObject.layer;
        if (num == LayerMask.NameToLayer("Player"))
        {
            this.HitPlayer(other.gameObject);
            Object.Destroy(base.gameObject);
            return;
        }
        if (num != LayerMask.NameToLayer("Enemy"))
        {
            if (num == LayerMask.NameToLayer("Bullet"))
            {
                if (other.gameObject.name == base.gameObject.name)
                {
                    return;
                }
                Object.Destroy(base.gameObject);
                Object.Destroy(other.gameObject);
                this.BulletExplosion(other.contacts[0]);
            }
            Object.Destroy(base.gameObject);
            return;
        }
        if (this.col == Color.blue)
        {
            audioManager.Play("Hitmarker");
            MonoBehaviour.print("HITMARKER");
        }
        Object.Instantiate<GameObject>(manager.enemyHitAudio, other.contacts[0].point, Quaternion.identity);
        ((RagdollController)other.transform.root.GetComponent(typeof(RagdollController))).MakeRagdoll(-base.transform.right * 350f);
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(-base.transform.right * 1500f);
        }
        ((Enemy)other.transform.root.GetComponent(typeof(Enemy))).DropGun(Vector3.up);
        Object.Destroy(base.gameObject);
    }

    public void SetBullet(float damage, float push, Color col)
    {
        this.damage = damage;
        this.push = push;
        this.col = col;
        if (this.changeCol)
        {
            SpriteRenderer[] componentsInChildren = base.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < (int)componentsInChildren.Length; i++)
            {
                componentsInChildren[i].color = col;
            }
        }
        TrailRenderer componentInChildren = base.GetComponentInChildren<TrailRenderer>();
        if (componentInChildren == null)
        {
            return;
        }
        componentInChildren.startColor = col;
        componentInChildren.endColor = col;
    }

    private void Start()
    {
        this.rb = base.GetComponent<Rigidbody>();
        manager = GameObject.Find("Managers").transform.GetChild(0).GetComponent<PrefabManager>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioManager =GameObject.Find("Managers").transform.GetChild(0).GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (!this.explosive)
        {
            return;
        }
        this.rb.AddForce((Vector3.up * Time.deltaTime) * 1000f);
    }
}