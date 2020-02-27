using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageOrbitRoam : MonoBehaviour
{
    private static ManageOrbitRoam _instance;
    public static ManageOrbitRoam Instance{
        get{
            if(_instance == null)
            {
                Debug.Log("Manage Orbit Roam is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    
    private MomentMode _currentMomentMode;
    public MomentMode CurrentMomentMode{
        get{
            return _currentMomentMode;
        }
        set{
            _currentMomentMode = value;
        }
    }

    public GameObject cameraObject;

    public Vector3 cameraIntialPosition;
    public Vector3 cameraIntialEulerAngles;
    
    
    [Range(0.01f,0.3f)]
    public float MomentSensitivity;
    //To restirct update function
    public bool rotateMode = false;

    public bool tiltMode = false;
  
    void Start()
    {
     CurrentMomentMode = MomentMode.Orbit;
    }
    //For orbit button
    public void ToSetOrbit()
    {
        CurrentMomentMode = MomentMode.Orbit;
        cameraObject.GetComponent<RotateObject>().FitToScreen();
        SetToOrbitMoment();
      
    }
    //For Roam Button
    public void ToSetRoam()
    {
        CurrentMomentMode = MomentMode.Roam;
       SetToRoamMoment();
    }

   //To restrict moment of camera
   public void ToSetViewMode()
   {
       cameraObject.GetComponent<RotateObject>().enabled = false;
        ProductHandlerManager.Instance.product.GetComponent<RotateCam>().enabled = false;
   }
   //Back to orbit moment from capture shots
   public void SetToOrbitMoment()
   {
       cameraObject.GetComponent<RotateObject>().enabled = true;
        ProductHandlerManager.Instance.product.GetComponent<RotateCam>().enabled = false;
   }
   //Back to roam moment from capture shots
   public void SetToRoamMoment()
   {
       cameraObject.GetComponent<RotateObject>().enabled = false;
        ProductHandlerManager.Instance.product.GetComponent<RotateCam>().enabled = true;
   }
   //To set previous moment after unselectshot or delete
   public void GetBackPreviousMoment(){
        ManageOrbitRoam.Instance.tiltMode = false;
        if(CurrentMomentMode == MomentMode.Orbit)
            {
                 
                ManageOrbitRoam.Instance.SetToOrbitMoment();
            }else if(CurrentMomentMode == MomentMode.Roam)
            {
                ManageOrbitRoam.Instance.SetToRoamMoment();
            }
   }
}
public enum MomentMode
{
    Orbit,Roam
}