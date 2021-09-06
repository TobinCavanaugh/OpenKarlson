using System;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	public struct AutoUnwrapSettings
	{
		public enum Fill
		{
			Fit = 0,
			Tile = 1,
			Stretch = 2,
		}

		public enum Anchor
		{
			UpperLeft = 0,
			UpperCenter = 1,
			UpperRight = 2,
			MiddleLeft = 3,
			MiddleCenter = 4,
			MiddleRight = 5,
			LowerLeft = 6,
			LowerCenter = 7,
			LowerRight = 8,
			None = 9,
		}

		public AutoUnwrapSettings(AutoUnwrapSettings unwrapSettings) : this()
		{
		}

		[SerializeField]
		private bool m_UseWorldSpace;
		[SerializeField]
		private bool m_FlipU;
		[SerializeField]
		private bool m_FlipV;
		[SerializeField]
		private bool m_SwapUV;
		[SerializeField]
		private Fill m_Fill;
		[SerializeField]
		private Vector2 m_Scale;
		[SerializeField]
		private Vector2 m_Offset;
		[SerializeField]
		private float m_Rotation;
		[SerializeField]
		private Anchor m_Anchor;
	}
}
