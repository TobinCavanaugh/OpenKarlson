using UnityEngine;

namespace UnityEngine.ProBuilder
{
	internal class RaycastHit
	{
		public RaycastHit(float distance, Vector3 point, Vector3 normal, int face)
		{
		}

		public float distance;
		public Vector3 point;
		public Vector3 normal;
		public int face;
	}
}
