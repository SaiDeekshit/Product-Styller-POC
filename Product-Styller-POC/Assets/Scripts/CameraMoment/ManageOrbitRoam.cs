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
    //To set current moment orientation in between orbit or roam
    private  string _currentMoment;
    public  string CurrentMoment
    {
        get
        {
            return _currentMoment;
        }
        set
        {
            if(_currentMoment == "")
            {
                _currentMoment = "orbit";
            }
            _currentMoment = value;
          
        }
    }
    public GameObject cameraObject;

    public Vector3 cameraIntialPosition;
    public Vector3 cameraIntialEulerAngles;
    
    // public Slider horizontalMomentSensitivity;
    [Range(0.1f,0.8f)]
    public float MomentSensitivity;
    //To restirct update function
    public bool rotateMode = false;

    public bool tiltMode = false;
  
    void Start()
    {
       CurrentMoment = "orbit";
    }
    //For orbit button
    public void ToSetOrbit()
    {
        CurrentMoment = "orbit";
        cameraObject.GetComponent<RotateObject>().FitToScreen();
        SetToOrbitMoment();
      
    }
    //For Roam Button
    public void ToSetRoam()
    {
        CurrentMoment = "roam";
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
}
