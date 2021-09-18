using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LevelSaver : MonoBehaviour
{

    public string saveData;
    public GameObject objectParent;
    private string txtDocumentName;
    void Start() {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/CustomLevelSaves/");
        txtDocumentName = Application.streamingAssetsPath + "/CustomLevelSaves/" + name + ".lff";
    }

    public void CreateFile(string name){
        File.WriteAllText(txtDocumentName, "");
        GameObject[] sceneObjects = new GameObject[objectParent.transform.childCount];

        for(int i = 0; i < objectParent.transform.childCount; i++){
            sceneObjects[i] = objectParent.transform.GetChild(i).gameObject;
        }
        for(int i = 0; i < sceneObjects.Length; i++){
            File.AppendAllText(txtDocumentName,sceneObjects[i].transform.GetChild(0).GetComponent<MeshFilter>().mesh + "\n");
            Debug.Log(sceneObjects[i].name);
        }

        Debug.Log("Saved files:");
    }   
}

