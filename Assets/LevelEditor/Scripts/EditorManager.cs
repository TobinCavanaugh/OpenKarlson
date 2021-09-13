using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public GameObject currentObject;
    public GameObject objectsParent;
    public Animator animator;

    [Header("Bools")]
    public bool adjustMode = false;
    public bool buildMode = true;

    [Header("Switching Types")]
    public int selected = 0;
    public GameObject[] levelObjects;




    public void ChangeSelected(int delta){
        if(selected + delta < 0 || selected + delta > levelObjects.Length - 1){
            return;
        }
        selected += delta;
        Debug.Log(selected);
        currentObject = levelObjects[selected];
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
