using System;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float time;

    public DestroyObject()
    {
    }

    private void DestroySelf()
    {
        Object.Destroy(base.gameObject);
    }

    private void Start()
    {
        base.Invoke("DestroySelf", this.time);
    }
}