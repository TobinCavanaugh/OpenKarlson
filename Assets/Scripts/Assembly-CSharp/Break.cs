using System;
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject replace;

    private bool done;

    public Break()
    {
    }

    private void BreakDoor(Rigidbody rb)
    {
        Vector3 vector3 = rb.velocity;
        float single = vector3.magnitude;
        if (single > 20f)
        {
            vector3 = vector3 / (single / 20f);
        }
        Rigidbody[] componentsInChildren = Object.Instantiate<GameObject>(this.replace, base.transform.position, base.transform.rotation).GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < (int)componentsInChildren.Length; i++)
        {
            componentsInChildren[i].velocity = vector3 * 1.5f;
        }
        Object.Instantiate<GameObject>(PrefabManager.Instance.destructionAudio, base.transform.position, Quaternion.identity);
        Object.Destroy(base.gameObject);
        this.done = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (this.done)
        {
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            return;
        }
        Rigidbody component = other.gameObject.GetComponent<Rigidbody>();
        if (!component)
        {
            return;
        }
        if (component.velocity.magnitude > 18f)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (!PlayerMovement.Instance.IsCrouching())
                {
                    return;
                }
                PlayerMovement.Instance.Slowmo(0.35f, 0.8f);
                this.BreakDoor(component);
            }
            this.BreakDoor(component);
        }
    }
}