using System;
using UnityEngine;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	internal class ChangelogEntry
	{
		public ChangelogEntry(SemVer version, string releaseNotes)
		{
		}

		[SerializeField]
		private SemVer m_VersionInfo;
		[SerializeField]
		private string m_ReleaseNotes;
	}
}
