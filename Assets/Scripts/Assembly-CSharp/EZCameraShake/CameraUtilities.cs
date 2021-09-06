using System;
using UnityEngine;

namespace EZCameraShake
{
    public class CameraUtilities
    {
        public CameraUtilities()
        {
        }

        public static Vector3 MultiplyVectors(Vector3 v, Vector3 w)
        {
            v.x *= w.x;
            v.y *= w.y;
            v.z *= w.z;
            return v;
        }

        public static Vector3 SmoothDampEuler(Vector3 current, Vector3 target, ref Vector3 velocity, float smoothTime)
        {
            Vector3 vector3 = new Vector3();
            vector3.x = Mathf.SmoothDampAngle(current.x, target.x, ref velocity.x, smoothTime);
            vector3.y = Mathf.SmoothDampAngle(current.y, target.y, ref velocity.y, smoothTime);
            vector3.z = Mathf.SmoothDampAngle(current.z, target.z, ref velocity.z, smoothTime);
            return vector3;
        }
    }
}