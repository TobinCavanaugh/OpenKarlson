using System;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public RotateObject()
    {
    }

    private void Update()
    {
        base.transform.Rotate(Vector3.right, 40f * Time.deltaTime);
    }
}