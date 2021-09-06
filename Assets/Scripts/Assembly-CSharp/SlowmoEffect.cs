using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SlowmoEffect : MonoBehaviour
{
    public Image blackFx;

    public PostProcessProfile pp;

    private ColorGrading cg;

    private float frequency;

    private float vel;

    private float hue;

    private float hueVel;

    private AudioDistortionFilter af;

    private AudioLowPassFilter lf;

    public SlowmoEffect slowmoEffect;


    public SlowmoEffect()
    {
    }

    public void NewScene(AudioLowPassFilter l, AudioDistortionFilter d)
    {
        this.lf = l;
        this.af = d;
    }

    private void Start()
    {
        this.cg = this.pp.GetSetting<ColorGrading>();
        slowmoEffect = this;
    }

    private void Update()
    {
        if (!this.af || !this.lf)
        {
            return;
        }
        if (!Game.Instance.playing || !Camera.main)
        {
            if (this.cg.hueShift.@value != 0f)
            {
                this.cg.hueShift.@value = 0f;
            }
            return;
        }
        float single = Time.timeScale;
        float single1 = (1f - single) * 2f;
        if ((double)single1 > 0.7)
        {
            single1 = 0.7f;
        }
        this.blackFx.color = new Color(1f, 1f, 1f, single1);
        float actionMeter = PlayerMovement.Instance.GetActionMeter();
        float single2 = 0f;
        if (single < 0.9f)
        {
            actionMeter = 400f;
            single2 = -20f;
        }
        this.frequency = Mathf.SmoothDamp(this.frequency, actionMeter, ref this.vel, 0.1f);
        this.hue = Mathf.SmoothDamp(this.hue, single2, ref this.hueVel, 0.2f);
        if (this.af)
        {
            this.af.distortionLevel = single1 * 0.2f;
        }
        if (this.lf)
        {
            this.lf.cutoffFrequency = this.frequency;
        }
        if (this.cg)
        {
            this.cg.hueShift.@value = this.hue;
        }
        if (!Game.Instance.playing)
        {
            this.cg.hueShift.@value = 0f;
        }
    }
}