using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	public class ProBuilderMesh : MonoBehaviour
	{
		[SerializeField]
		private int m_MeshFormatVersion;
		[SerializeField]
		private Face[] m_Faces;
		[SerializeField]
		private SharedVertex[] m_SharedVertices;
		[SerializeField]
		private SharedVertex[] m_SharedTextures;
		[SerializeField]
		private Vector3[] m_Positions;
		[SerializeField]
		private Vector2[] m_Textures0;
		[SerializeField]
		private List<Vector4> m_Textures2;
		[SerializeField]
		private List<Vector4> m_Textures3;
		[SerializeField]
		private Vector4[] m_Tangents;
		[SerializeField]
		private Color[] m_Colors;
		[SerializeField]
		private UnwrapParameters m_UnwrapParameters;
		[SerializeField]
		private bool m_PreserveMeshAssetOnDestroy;
		[SerializeField]
		internal string assetGuid;
		[SerializeField]
		private bool m_IsSelectable;
		[SerializeField]
		private int[] m_SelectedFaces;
		[SerializeField]
		private Edge[] m_SelectedEdges;
		[SerializeField]
		private int[] m_SelectedVertices;
	}
}
