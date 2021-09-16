using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TransformModifier : MonoBehaviour
{
    public TMP_InputField[] positionInput;
    public TMP_InputField[] rotationInput;
    public TMP_InputField[] scaleInput;

    private Vector3 position;
    private Vector3 rotation;
    private Vector3 scale;
    public EditorManager manager;

    public void UpdateTransform(){
        manager.selectedObj.transform.position = new Vector3(float.Parse(positionInput[0].text), float.Parse(positionInput[1].text), float.Parse(positionInput[2].text));
        manager.selectedObj.transform.rotation = new Quaternion(float.Parse(rotationInput[0].text), float.Parse(rotationInput[1].text), float.Parse(rotationInput[2].text), 0);
        manager.selectedObj.transform.localScale = new Vector3(float.Parse(scaleInput[0].text), float.Parse(scaleInput[1].text), float.Parse(scaleInput[2].text));

        UpdateDisplay();
    }

    public void UpdateDisplay(){

        position = manager.selectedObj.transform.position;
        rotation = manager.selectedObj.transform.rotation.eulerAngles;
        scale = manager.selectedObj.transform.localScale;

        //Position
        positionInput[0].text = ("" + position.x);
        positionInput[1].text = ("" + position.y);
        positionInput[2].text = ("" + position.z);

        //Rotation
        rotationInput[0].text = ("" + rotation.x);
        rotationInput[1].text = ("" + rotation.y);
        rotationInput[2].text = ("" + rotation.z);

        //Scale
        scaleInput[0].text = ("" + scale.x);
        scaleInput[1].text = ("" + scale.y);
        scaleInput[2].text = ("" + scale.z);
    }
}
