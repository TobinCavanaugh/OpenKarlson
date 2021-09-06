using System;
using UnityEngine;

public class Object : MonoBehaviour
{
    // private bool ready = true;

    // private bool hitReady = true;

    // public Object()
    // {
    // }

    // private void GetReady()
    // {
    //     this.ready = true;
    // }

    // private void OnCollisionEnter(Collision other)
    // {
    //     Vector3 component = other.relativeVelocity;
    //     float single = component.magnitude * 0.025f;
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && this.hitReady && single > 0.8f)
    //     {
    //         this.hitReady = false;
    //         component = base.GetComponent<Rigidbody>().velocity;
    //         Vector3 vector3 = component.normalized;
    //         Object.Instantiate<GameObject>(PrefabManager.Instance.enemyHitAudio, other.contacts[0].point, Quaternion.identity);
    //         ((RagdollController)other.transform.root.GetComponent(typeof(RagdollController))).MakeRagdoll(vector3 * 350f);
    //         Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
    //         if (rigidbody)
    //         {
    //             rigidbody.AddForce(vector3 * 1100f);
    //         }
    //         ((Enemy)other.transform.root.GetComponent(typeof(Enemy))).DropGun(Vector3.up);
    //     }
    //     if (!this.ready)
    //     {
    //         return;
    //     }
    //     this.ready = false;
    //     AudioSource audioSource = Object.Instantiate<GameObject>(PrefabManager.Instance.objectImpactAudio, base.transform.position, Quaternion.identity).GetComponent<AudioSource>();
    //     Rigidbody component1 = base.GetComponent<Rigidbody>();
    //     float single1 = 1f;
    //     if (component1)
    //     {
    //         single1 = component1.mass;
    //     }
    //     if (single1 < 0.3f)
    //     {
    //         single1 = 0.5f;
    //     }
    //     if (single1 > 1f)
    //     {
    //         single1 = 1f;
    //     }
    //     float single2 = audioSource.volume;
    //     if (single > 1f)
    //     {
    //         single = 1f;
    //     }
    //     audioSource.volume = single * single1;
    //     base.Invoke("GetReady", 0.1f);
    // }
}