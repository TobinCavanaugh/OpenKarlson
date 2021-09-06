using System;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public Bounce()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        MonoBehaviour.print("yeet");
        bool component = other.gameObject.GetComponent<Rigidbody>();
    }
}