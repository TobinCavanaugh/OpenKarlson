using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	internal class PreferenceDictionary : ScriptableObject
	{
		[SerializeField]
		private List<string> m_Bool_keys;
		[SerializeField]
		private List<string> m_Int_keys;
		[SerializeField]
		private List<string> m_Float_keys;
		[SerializeField]
		private List<string> m_String_keys;
		[SerializeField]
		private List<string> m_Color_keys;
		[SerializeField]
		private List<string> m_Material_keys;
		[SerializeField]
		private List<bool> m_Bool_values;
		[SerializeField]
		private List<int> m_Int_values;
		[SerializeField]
		private List<float> m_Float_values;
		[SerializeField]
		private List<string> m_String_values;
		[SerializeField]
		private List<Color> m_Color_values;
		[SerializeField]
		private List<Material> m_Material_values;
	}
}
