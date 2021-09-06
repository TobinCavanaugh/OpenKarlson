using EZCameraShake;
using System;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public GameObject glass;

    public GameObject glassSfx;

    public PlayerMovement movement;
    public CameraShaker shaker;

    public Glass()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            Object.Instantiate<GameObject>(this.glassSfx, base.transform.position, Quaternion.identity);
            this.glass.SetActive(true);
            this.glass.transform.parent = null;
            this.glass.transform.localScale = Vector3.one;
            Object.Destroy(base.gameObject);
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                movement.Slowmo(0.3f, 1f);
            }
            shaker.ShakeOnce(5f, 3.5f, 0.3f, 0.2f);
        }
    }
}