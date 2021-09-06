using System;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	internal class SemVer
	{
		[SerializeField]
		private int m_Major;
		[SerializeField]
		private int m_Minor;
		[SerializeField]
		private int m_Patch;
		[SerializeField]
		private int m_Build;
		[SerializeField]
		private string m_Type;
		[SerializeField]
		private string m_Metadata;
		[SerializeField]
		private string m_Date;
	}
}
