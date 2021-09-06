using EZCameraShake;
using System;
using UnityEngine;

public class ShakeOnTrigger : MonoBehaviour
{
    private CameraShakeInstance _shakeInstance;

    public ShakeOnTrigger()
    {
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            this._shakeInstance.StartFadeIn(1f);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            this._shakeInstance.StartFadeOut(3f);
        }
    }

    private void Start()
    {
        this._shakeInstance = CameraShaker.Instance.StartShake(2f, 15f, 2f);
        this._shakeInstance.StartFadeOut(0f);
        this._shakeInstance.DeleteOnInactive = true;
    }
}