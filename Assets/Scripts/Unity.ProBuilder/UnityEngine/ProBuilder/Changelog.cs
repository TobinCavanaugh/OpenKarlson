using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	internal class Changelog
	{
		public Changelog(string log)
		{
		}

		[SerializeField]
		private List<ChangelogEntry> m_Entries;
	}
}
