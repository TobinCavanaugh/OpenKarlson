using System;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	public class UnwrapParameters
	{
		[SerializeField]
		private float m_HardAngle;
		[SerializeField]
		private float m_PackMargin;
		[SerializeField]
		private float m_AngleError;
		[SerializeField]
		private float m_AreaError;
	}
}
