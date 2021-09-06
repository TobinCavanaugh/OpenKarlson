using System;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private bool done;

    public GameObject explosion;

    public Barrel()
    {
    }

    private void Explode()
    {
        Object.Instantiate<GameObject>(explosion, base.transform.position, Quaternion.identity);
        Object.Destroy(base.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Explosion componentInChildren = (Explosion)Object.Instantiate<GameObject>(explosion, base.transform.position, Quaternion.identity).GetComponentInChildren(typeof(Explosion));
            Object.Destroy(base.gameObject);
            base.CancelInvoke();
            this.done = true;
            Bullet component = (Bullet)other.gameObject.GetComponent(typeof(Bullet));
            if (component && component.player)
            {
                componentInChildren.player = component.player;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Bullet"))
        {
            return;
        }
        this.done = true;
        base.Invoke("Explode", 0.2f);
    }
}