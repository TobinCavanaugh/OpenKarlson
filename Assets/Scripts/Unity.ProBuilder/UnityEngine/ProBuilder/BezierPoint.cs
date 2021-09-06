using System;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	internal struct BezierPoint
	{
		public BezierPoint(Vector3 position, Vector3 tangentIn, Vector3 tangentOut, Quaternion rotation) : this()
		{
		}

		public Vector3 position;
		public Vector3 tangentIn;
		public Vector3 tangentOut;
		public Quaternion rotation;
	}
}
