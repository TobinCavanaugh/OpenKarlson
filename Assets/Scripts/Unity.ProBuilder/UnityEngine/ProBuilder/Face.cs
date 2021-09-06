using System;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	public class Face
	{
		[SerializeField]
		private int[] m_Indexes;
		[SerializeField]
		private int m_SmoothingGroup;
		[SerializeField]
		private AutoUnwrapSettings m_Uv;
		[SerializeField]
		private Material m_Material;
		[SerializeField]
		private int m_SubmeshIndex;
		[SerializeField]
		private bool m_ManualUV;
		[SerializeField]
		internal int elementGroup;
		[SerializeField]
		private int m_TextureGroup;
	}
}
