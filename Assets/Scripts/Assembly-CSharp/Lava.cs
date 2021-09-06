using System;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public PlayerMovement movement;
    public Lava()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            movement.KillPlayer();
        }
    }
}