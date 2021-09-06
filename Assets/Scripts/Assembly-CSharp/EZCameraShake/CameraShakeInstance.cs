using System;
using UnityEngine;

namespace EZCameraShake
{
    public class CameraShakeInstance
    {
        public float Magnitude;

        public float Roughness;

        public Vector3 PositionInfluence;

        public Vector3 RotationInfluence;

        public bool DeleteOnInactive = true;

        private float roughMod = 1f;

        private float magnMod = 1f;

        private float fadeOutDuration;

        private float fadeInDuration;

        private bool sustain;

        private float currentFadeTime;

        private float tick;

        private Vector3 amt;

        public CameraShakeState CurrentState
        {
            get
            {
                if (this.IsFadingIn)
                {
                    return CameraShakeState.FadingIn;
                }
                if (this.IsFadingOut)
                {
                    return CameraShakeState.FadingOut;
                }
                if (this.IsShaking)
                {
                    return CameraShakeState.Sustained;
                }
                return CameraShakeState.Inactive;
            }
        }

        private bool IsFadingIn
        {
            get
            {
                if (this.currentFadeTime >= 1f || !this.sustain)
                {
                    return false;
                }
                return this.fadeInDuration > 0f;
            }
        }

        private bool IsFadingOut
        {
            get
            {
                if (this.sustain)
                {
                    return false;
                }
                return this.currentFadeTime > 0f;
            }
        }

        private bool IsShaking
        {
            get
            {
                if (this.currentFadeTime > 0f)
                {
                    return true;
                }
                return this.sustain;
            }
        }

        public float NormalizedFadeTime
        {
            get
            {
                return this.currentFadeTime;
            }
        }

        public float ScaleMagnitude
        {
            get
            {
                return this.magnMod;
            }
            set
            {
                this.magnMod = value;
            }
        }

        public float ScaleRoughness
        {
            get
            {
                return this.roughMod;
            }
            set
            {
                this.roughMod = value;
            }
        }

        public CameraShakeInstance(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
        {
            this.Magnitude = magnitude;
            this.fadeOutDuration = fadeOutTime;
            this.fadeInDuration = fadeInTime;
            this.Roughness = roughness;
            if (fadeInTime <= 0f)
            {
                this.sustain = false;
                this.currentFadeTime = 1f;
            }
            else
            {
                this.sustain = true;
                this.currentFadeTime = 0f;
            }
            this.tick = (float)UnityEngine.Random.Range(-100, 100);
        }

        public CameraShakeInstance(float magnitude, float roughness)
        {
            this.Magnitude = magnitude;
            this.Roughness = roughness;
            this.sustain = true;
            this.tick = (float)UnityEngine.Random.Range(-100, 100);
        }

        public void StartFadeIn(float fadeInTime)
        {
            if (fadeInTime == 0f)
            {
                this.currentFadeTime = 1f;
            }
            this.fadeInDuration = fadeInTime;
            this.fadeOutDuration = 0f;
            this.sustain = true;
        }

        public void StartFadeOut(float fadeOutTime)
        {
            if (fadeOutTime == 0f)
            {
                this.currentFadeTime = 0f;
            }
            this.fadeOutDuration = fadeOutTime;
            this.fadeInDuration = 0f;
            this.sustain = false;
        }

        public Vector3 UpdateShake()
        {
            this.amt.x = Mathf.PerlinNoise(this.tick, 0f) - 0.5f;
            this.amt.y = Mathf.PerlinNoise(0f, this.tick) - 0.5f;
            this.amt.z = Mathf.PerlinNoise(this.tick, this.tick) - 0.5f;
            if (this.fadeInDuration > 0f && this.sustain)
            {
                if (this.currentFadeTime < 1f)
                {
                    this.currentFadeTime = this.currentFadeTime + Time.deltaTime / this.fadeInDuration;
                }
                else if (this.fadeOutDuration > 0f)
                {
                    this.sustain = false;
                }
            }
            if (!this.sustain)
            {
                this.currentFadeTime = this.currentFadeTime - Time.deltaTime / this.fadeOutDuration;
            }
            if (!this.sustain)
            {
                this.tick = this.tick + Time.deltaTime * this.Roughness * this.roughMod * this.currentFadeTime;
            }
            else
            {
                this.tick = this.tick + Time.deltaTime * this.Roughness * this.roughMod;
            }
            return ((this.amt * this.Magnitude) * this.magnMod) * this.currentFadeTime;
        }
    }
}