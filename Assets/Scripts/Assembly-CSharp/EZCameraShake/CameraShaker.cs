using System;
using System.Collections.Generic;
using UnityEngine;

namespace EZCameraShake
{
    [AddComponentMenu("EZ Camera Shake/Camera Shaker")]
    public class CameraShaker : MonoBehaviour
    {
        public static CameraShaker Instance;

        private static Dictionary<string, CameraShaker> instanceList;

        public Vector3 DefaultPosInfluence = new Vector3(0.15f, 0.15f, 0.15f);

        public Vector3 DefaultRotInfluence = new Vector3(1f, 1f, 1f);

        private Vector3 posAddShake;

        private Vector3 rotAddShake;

        private List<CameraShakeInstance> cameraShakeInstances = new List<CameraShakeInstance>();

        public List<CameraShakeInstance> ShakeInstances
        {
            get
            {
                return new List<CameraShakeInstance>(this.cameraShakeInstances);
            }
        }

        static CameraShaker()
        {
            CameraShaker.instanceList = new Dictionary<string, CameraShaker>();
        }

        public CameraShaker()
        {
        }

        private void Awake()
        {
            CameraShaker.Instance = this;
            CameraShaker.instanceList.Add(base.gameObject.name, this);
        }

        public static CameraShaker GetInstance(string name)
        {
            CameraShaker cameraShaker;
            if (CameraShaker.instanceList.TryGetValue(name, out cameraShaker))
            {
                return cameraShaker;
            }
            return null;
        }

        private void OnDestroy()
        {
            CameraShaker.instanceList.Remove(base.gameObject.name);
        }

        public CameraShakeInstance Shake(CameraShakeInstance shake)
        {
            this.cameraShakeInstances.Add(shake);
            return shake;
        }

        public CameraShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
        {
            if (!GameState.Instance)
            {
                return null;
            }
            if (!GameState.Instance.shake)
            {
                return null;
            }
            CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime)
            {
                PositionInfluence = this.DefaultPosInfluence,
                RotationInfluence = this.DefaultRotInfluence
            };
            this.cameraShakeInstances.Add(cameraShakeInstance);
            return cameraShakeInstance;
        }

        public CameraShakeInstance ShakeOnce(float magnitude, float roughness, float fadeInTime, float fadeOutTime, Vector3 posInfluence, Vector3 rotInfluence)
        {
            if (!GameState.Instance.shake)
            {
                return null;
            }
            CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(magnitude, roughness, fadeInTime, fadeOutTime)
            {
                PositionInfluence = posInfluence,
                RotationInfluence = rotInfluence
            };
            this.cameraShakeInstances.Add(cameraShakeInstance);
            return cameraShakeInstance;
        }

        public CameraShakeInstance StartShake(float magnitude, float roughness, float fadeInTime)
        {
            if (!GameState.Instance.shake)
            {
                return null;
            }
            CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(magnitude, roughness)
            {
                PositionInfluence = this.DefaultPosInfluence,
                RotationInfluence = this.DefaultRotInfluence
            };
            cameraShakeInstance.StartFadeIn(fadeInTime);
            this.cameraShakeInstances.Add(cameraShakeInstance);
            return cameraShakeInstance;
        }

        public CameraShakeInstance StartShake(float magnitude, float roughness, float fadeInTime, Vector3 posInfluence, Vector3 rotInfluence)
        {
            CameraShakeInstance cameraShakeInstance = new CameraShakeInstance(magnitude, roughness)
            {
                PositionInfluence = posInfluence,
                RotationInfluence = rotInfluence
            };
            cameraShakeInstance.StartFadeIn(fadeInTime);
            this.cameraShakeInstances.Add(cameraShakeInstance);
            return cameraShakeInstance;
        }

        private void Update()
        {
            this.posAddShake = Vector3.zero;
            this.rotAddShake = Vector3.zero;
            for (int i = 0; i < this.cameraShakeInstances.Count && i < this.cameraShakeInstances.Count; i++)
            {
                CameraShakeInstance item = this.cameraShakeInstances[i];
                if (item.CurrentState == CameraShakeState.Inactive && item.DeleteOnInactive)
                {
                    this.cameraShakeInstances.RemoveAt(i);
                    i--;
                }
                else if (item.CurrentState != CameraShakeState.Inactive)
                {
                    this.posAddShake += CameraUtilities.MultiplyVectors(item.UpdateShake(), item.PositionInfluence);
                    this.rotAddShake += CameraUtilities.MultiplyVectors(item.UpdateShake(), item.RotationInfluence);
                }
            }
            base.transform.localPosition = this.posAddShake;
            base.transform.localEulerAngles = this.rotAddShake;
        }
    }
}