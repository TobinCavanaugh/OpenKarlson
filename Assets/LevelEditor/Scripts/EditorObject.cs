using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorObject : MonoBehaviour
{
    public bool selected;
    public int index;
    public Vector3 rotation = new Vector3(0, 0, 0);
    EditorManager manager;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("EditorManager").GetComponent<EditorManager>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void InstantiateObject(RaycastHit hit, Vector3 scale, float yAxis){
        Vector3 vec = new Vector3(0, camera.transform.rotation.y, 0);
        Vector3 cloneVec = new Vector3(0, 0, 0);

        var degrees = -123;
        vec.y = (vec.y % 90 + 90) % 90;
        
        Debug.Log(vec.y);

        GameObject block = Instantiate(manager.defaultObject, transform.position + hit.normal, new Quaternion(0, 0, 0, 0));
        block.transform.Rotate(new Vector3(0, yAxis, 0));
        Debug.Log("yAxis " + yAxis);
        block.transform.parent = manager.objectsParent.transform;
        
        if(block.GetComponent<Collider>() == null){
            block.AddComponent<BoxCollider>();
        }

        if(block.GetComponent<EditorObject>() == null){
            block.AddComponent<EditorObject>();
        }

        Debug.Log(hit.normal);
    }
}
