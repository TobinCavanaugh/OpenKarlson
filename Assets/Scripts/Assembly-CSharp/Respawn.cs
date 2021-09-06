using System;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;

    public Respawn()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        MonoBehaviour.print(other.gameObject.layer);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Transform transforms = other.transform.root;
            transforms.transform.position = this.respawnPoint.position;
            transforms.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}