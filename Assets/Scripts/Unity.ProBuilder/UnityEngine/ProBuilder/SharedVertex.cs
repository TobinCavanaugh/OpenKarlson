using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	public class SharedVertex
	{
		public SharedVertex(IEnumerable<int> indexes)
		{
		}

		[SerializeField]
		private int[] m_Vertices;
	}
}
