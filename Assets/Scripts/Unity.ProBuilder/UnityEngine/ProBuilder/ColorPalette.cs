using System;
using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	[Serializable]
	internal class ColorPalette : ScriptableObject
	{
		[SerializeField]
		private List<Color> m_Colors;
	}
}
