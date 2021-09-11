using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public GameObject defaultObject;
    public GameObject objectsParent;
    public Animator animator;

    [Header("Bools")]
    public bool scaleMode = false;
    public bool rotateMode = false;
    //public bool selectMode = false;
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
        defaultObject = levelObjects[selected];
    }

    public void ScaleMode(){
        scaleMode = true;
        rotateMode = false;
    //    selectMode = false;
        buildMode = false;
    }

    public void RotateMode(){
        scaleMode = false;
        rotateMode = true;
    //    selectMode = false;
        buildMode = false;
    }

    //public void SelectMode(){
    //    scaleMode = false;
    //    rotateMode = false;
    //    selectMode = true;
    //    buildMode = false;
    //}

    public void BuildMode(){
        scaleMode = false;
        rotateMode = false;
    //    selectMode = false;
        buildMode = true;
    }
}
