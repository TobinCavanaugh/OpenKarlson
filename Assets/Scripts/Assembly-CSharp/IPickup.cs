using System;
using UnityEngine;

public interface IPickup
{
    bool IsPickedUp();

    void Use(Vector3 attackDirection);
}