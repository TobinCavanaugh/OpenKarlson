using EZCameraShake;
using System;
using UnityEngine;

public class ShakeOnKeyPress : MonoBehaviour
{
    public float Magnitude = 2f;

    public float Roughness = 10f;

    public float FadeOutTime = 5f;

    public ShakeOnKeyPress()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CameraShaker.Instance.ShakeOnce(this.Magnitude, this.Roughness, 0f, this.FadeOutTime);
        }
    }
}