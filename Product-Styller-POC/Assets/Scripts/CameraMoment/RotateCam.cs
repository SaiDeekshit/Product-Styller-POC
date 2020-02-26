
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script is attached to product or model
public class RotateCam : MonoBehaviour
{
   
    //To make camera to looking at object
    public Transform cameraObject;
    // public Transform product;

    public float xRotations,yRotations;
   
     //For Clamping angles 
    public const float  X_ANGLE_MIN = 0f;
    public const float  X_ANGLE_MAX = 65f;
    public bool cameraRotationX,cameraRotationY;
    public Vector3 intialPose;
    public Vector3 eulerAngleOfCam;
    
    void Start()
    {
        cameraObject = Camera.main.transform;
        // product = transform;
        intialPose = cameraObject.transform.position;
        eulerAngleOfCam = cameraObject.transform.localEulerAngles;
    }

     void Update()
    {
        if(ManageOrbitRoam.Instance.rotateMode){
            DragInUpdate();
        }
    }
   void LateUpdate() {
        if(ManageOrbitRoam.Instance.rotateMode){
                if(cameraRotationX || cameraRotationY)
                {
                    cameraObject.transform.localEulerAngles = new Vector3(xRotations,yRotations,0);
                }
        }
   }
   void DragInUpdate()
   {
        //For Input arrow keys in forward direction (like zooming)
        if(Input.GetAxis("Vertical") != 0)
        {
            cameraObject.transform.Translate( new Vector3(0,0,Input.GetAxis("Vertical") * ManageOrbitRoam.Instance.MomentSensitivity) , Space.Self);
        }

        if(Input.GetAxis("Horizontal") != 0)
        {
            cameraObject.transform.Translate( new Vector3((Input.GetAxis("Horizontal") * ManageOrbitRoam.Instance.MomentSensitivity),0,0) , Space.Self);
        }
        //After right click Mouse moment and scroll moment
        if(Input.GetMouseButton(1)){
                xRotations = cameraObject.rotation.eulerAngles.x;
                yRotations = cameraObject.rotation.eulerAngles.y;
                xRotations -= (Input.GetAxis("Mouse Y"));
                
               
               
               xRotations = Mathf.Clamp(xRotations,X_ANGLE_MIN,X_ANGLE_MAX);
               yRotations += (Input.GetAxis("Mouse X"));
                if( Input.GetAxis("Mouse X") != 0 )
                {
                cameraRotationX = true;
                }
                 if( Input.GetAxis("Mouse Y") != 0 )
                {

                cameraRotationY = true;
                }
        }
        
        if(Input.GetMouseButtonUp(1)){
          
            cameraRotationX = false;
            cameraRotationY = false;
        
         }
         if(Input.GetKeyDown(KeyCode.F))
         {
            cameraObject.transform.position = ManageOrbitRoam.Instance.cameraIntialPosition;
            float eulerX = ManageOrbitRoam.Instance.cameraIntialEulerAngles.x;
            float eulerY = ManageOrbitRoam.Instance.cameraIntialEulerAngles.y;
            float eulerZ = ManageOrbitRoam.Instance.cameraIntialEulerAngles.z;
            cameraObject.transform.eulerAngles = new Vector3(eulerX,eulerY,eulerZ);
         }
   }
}
