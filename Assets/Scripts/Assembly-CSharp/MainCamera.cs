using System;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public SlowmoEffect slowmoEffect;
    public MainCamera()
    {
    }

    private void Awake()
    {
        if (!slowmoEffect)
        {
            return;
        }
        slowmoEffect.NewScene(base.GetComponent<AudioLowPassFilter>(), base.GetComponent<AudioDistortionFilter>());
    }
}