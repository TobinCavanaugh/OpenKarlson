using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	internal class FaceRebuildData
	{
		public Face face;
		public List<Vertex> vertices;
		public List<int> sharedIndexes;
		public List<int> sharedIndexesUV;
	}
}
