using System;
using UnityEngine;

[ExecuteInEditMode]
public class TextureScaling : MonoBehaviour
{
    private Vector3 _currentScale;

    public float size = 1f;

    public TextureScaling()
    {
    }

    public void Calculate()
    {
        if (this._currentScale == base.transform.localScale)
        {
            return;
        }
        if (this.CheckForDefaultSize())
        {
            return;
        }
        this._currentScale = base.transform.localScale;
        Mesh mesh = this.GetMesh();
        mesh.uv = this.SetupUvMap(mesh.uv);
        mesh.name = "Cube Instance";
        if (base.GetComponent<Renderer>().sharedMaterial.mainTexture.wrapMode != TextureWrapMode.Repeat)
        {
            base.GetComponent<Renderer>().sharedMaterial.mainTexture.wrapMode = TextureWrapMode.Repeat;
        }
    }

    private bool CheckForDefaultSize()
    {
        if (this._currentScale != Vector3.one)
        {
            return false;
        }
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Object.DestroyImmediate(base.GetComponent<MeshFilter>());
        base.gameObject.AddComponent<MeshFilter>();
        base.GetComponent<MeshFilter>().sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        Object.DestroyImmediate(gameObject);
        return true;
    }

    private Mesh GetMesh()
    {
        return base.GetComponent<MeshFilter>().mesh;
    }

    private Vector2[] SetupUvMap(Vector2[] meshUVs)
    {
        float single = this._currentScale.x * this.size;
        float single1 = this._currentScale.z * this.size;
        float single2 = this._currentScale.y * this.size;
        meshUVs[2] = new Vector2(0f, single2);
        meshUVs[3] = new Vector2(single, single2);
        meshUVs[0] = new Vector2(0f, 0f);
        meshUVs[1] = new Vector2(single, 0f);
        meshUVs[7] = new Vector2(0f, 0f);
        meshUVs[6] = new Vector2(single, 0f);
        meshUVs[11] = new Vector2(0f, single2);
        meshUVs[10] = new Vector2(single, single2);
        meshUVs[19] = new Vector2(single1, 0f);
        meshUVs[17] = new Vector2(0f, single2);
        meshUVs[16] = new Vector2(0f, 0f);
        meshUVs[18] = new Vector2(single1, single2);
        meshUVs[23] = new Vector2(single1, 0f);
        meshUVs[21] = new Vector2(0f, single2);
        meshUVs[20] = new Vector2(0f, 0f);
        meshUVs[22] = new Vector2(single1, single2);
        meshUVs[4] = new Vector2(single, 0f);
        meshUVs[5] = new Vector2(0f, 0f);
        meshUVs[8] = new Vector2(single, single1);
        meshUVs[9] = new Vector2(0f, single1);
        meshUVs[13] = new Vector2(single, 0f);
        meshUVs[14] = new Vector2(0f, 0f);
        meshUVs[12] = new Vector2(single, single1);
        meshUVs[15] = new Vector2(0f, single1);
        return meshUVs;
    }

    private void Start()
    {
        this.Calculate();
    }

    private void Update()
    {
        this.Calculate();
    }
}