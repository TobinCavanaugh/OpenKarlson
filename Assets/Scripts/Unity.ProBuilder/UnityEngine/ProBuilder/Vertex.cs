using System;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	public class Vertex
	{
		[SerializeField]
		private Vector3 m_Position;
		[SerializeField]
		private Color m_Color;
		[SerializeField]
		private Vector3 m_Normal;
		[SerializeField]
		private Vector4 m_Tangent;
		[SerializeField]
		private Vector2 m_UV0;
		[SerializeField]
		private Vector2 m_UV2;
		[SerializeField]
		private Vector4 m_UV3;
		[SerializeField]
		private Vector4 m_UV4;
		[SerializeField]
		private MeshArrays m_Attributes;
	}
}
