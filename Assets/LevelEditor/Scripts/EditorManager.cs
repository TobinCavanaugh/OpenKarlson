using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public GameObject currentObject;
    public GameObject objectsParent;
    public GameObject selectedObj;
    public Animator animator;

    [Header("Bools")]
    public bool adjustMode = false;
    public bool buildMode = true;

    [Header("Switching Types")]
    public int selected = 0;
    public GameObject[] levelObjects;

    public void ChangeObject(GameObject switchObj){
        currentObject = switchObj;
    }
    public void AdjustMode(){
        adjustMode = true;
        buildMode = false;
    }

    public void BuildMode(){
        adjustMode = false;
        buildMode = true;
    }
}
