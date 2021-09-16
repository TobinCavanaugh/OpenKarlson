using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private float X;
    private float Y;
    public float Sensitivity = 1f;
    public float moveSpeed = 100f;
    public Camera camera;
    public EditorManager editorManager;
    public LayerMask buildMask;
    public LayerMask camBoxMask;
    public float zAxisRot = 0;
    [SerializeField]
    private int buildDist = 100;
    public TransformModifier transformModifier;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        X = euler.x;
        Y = euler.y;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, buildDist, camBoxMask)){
            zAxisRot = hit.transform.gameObject.GetComponent<CameraBoxCollider>().rotation;
        }
    }

    private void HandleInput(){
        
        //Mouse look
        if(Input.GetMouseButton(1)){
            MouseLook();
        }

        //Adding objects
        if (Input.GetMouseButtonDown(0)){ // if left button pressed...
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, buildDist, buildMask)){            
                if(editorManager.buildMode){
                    Debug.Log(hit.transform.gameObject.GetComponent<EditorObject>());
                    InstantiateObject(hit);
                } else if(editorManager.adjustMode){                    
                    editorManager.selectedObj = hit.transform.gameObject;
                    Debug.Log(hit.transform.gameObject);
                    transformModifier.UpdateDisplay();
                }
            }
        }   

        //Removing objects
        if(Input.GetMouseButtonDown(2)){
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, buildDist, buildMask)){
                Destroy(hit.transform.gameObject);
            }
        }

    }

    void InstantiateObject(RaycastHit hit){

        GameObject block = Instantiate(editorManager.currentObject, hit.transform.position + hit.normal, new Quaternion(0, 0, 0, 0));
        block.transform.Rotate(new Vector3(0, zAxisRot, 0));
        block.transform.parent = editorManager.objectsParent.transform;
        
        if(block.GetComponent<Collider>() == null){
            block.AddComponent<BoxCollider>();
        }

        if(block.GetComponent<EditorObject>() == null){
            block.AddComponent<EditorObject>();
        }
    } 

    /// <summary>
    ///  Returns a number
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>

    private void MouseLook(){
        const float MIN_X = 0.0f;
        const float MAX_X = 360.0f;
        const float MIN_Y = -90.0f;
        const float MAX_Y = 90.0f;
    
        X += Input.GetAxis("Mouse X") * (Sensitivity * Time.deltaTime);
        if (X < MIN_X) X += MAX_X;
        else if (X > MAX_X) X -= MAX_X;
        Y -= Input.GetAxis("Mouse Y") * (Sensitivity * Time.deltaTime);
        if (Y < MIN_Y) Y = MIN_Y;
        else if (Y > MAX_Y) Y = MAX_Y;
    
        transform.rotation = Quaternion.Euler(Y, X, 0.0f);        

        //XYZ Movement
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);
        transform.Translate(Vector3.up * Time.deltaTime * Input.GetAxis("Upizontal") * moveSpeed);
    }
}   
