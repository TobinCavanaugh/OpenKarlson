using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	internal class PolyShape : MonoBehaviour
	{
		public enum PolyEditMode
		{
			None = 0,
			Path = 1,
			Height = 2,
			Edit = 3,
		}

		[SerializeField]
		internal List<Vector3> m_Points;
		[SerializeField]
		private float m_Extrude;
		[SerializeField]
		private PolyEditMode m_EditMode;
		[SerializeField]
		private bool m_FlipNormals;
		[SerializeField]
		internal bool isOnGrid;
	}
}
