using System;

namespace UnityEngine.ProBuilder.Poly2Tri
{
	internal class PointOnEdgeException : NotImplementedException
	{
		public PointOnEdgeException(string message, TriangulationPoint a, TriangulationPoint b, TriangulationPoint c)
		{
		}

	}
}
