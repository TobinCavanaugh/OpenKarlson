using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	internal class BezierShape : MonoBehaviour
	{
		public List<BezierPoint> points;
		public bool closeLoop;
		public float radius;
		public int rows;
		public int columns;
		public bool smooth;
		[SerializeField]
		private bool m_IsEditing;
	}
}
