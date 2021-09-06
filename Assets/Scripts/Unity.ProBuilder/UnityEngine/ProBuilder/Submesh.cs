using System;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	public class Submesh
	{
		public Submesh(Mesh mesh, int subMeshIndex)
		{
		}

		[SerializeField]
		internal int[] m_Indexes;
		[SerializeField]
		internal MeshTopology m_Topology;
		[SerializeField]
		internal int m_SubmeshIndex;
	}
}
