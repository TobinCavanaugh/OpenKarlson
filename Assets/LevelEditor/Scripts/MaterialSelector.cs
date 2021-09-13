using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MaterialSelector : MonoBehaviour
{
    public Material[] materials;
    public GameObject editorObject;
    public TMP_Dropdown dropdown;

    public void HandleInputData(int val){
        Debug.Log("selected # " + val + " material");
        if(val < 0 || val >= materials.Length){
            return;
        }
        if(editorObject.GetComponent<MeshRenderer>() != null){
            editorObject.GetComponent<MeshRenderer>().material = materials[val];
        } else{
            editorObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = materials[val];
        }
    }

    public void ChangeSelection(int selectedMat){
        dropdown.ClearOptions();
    }
}
