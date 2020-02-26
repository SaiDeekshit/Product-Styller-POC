﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefrenceShotDetails : MonoBehaviour
{
    public GameObject selectionImage;
    public Image thumbNail;
    public Text shotName;
    public Text resolution;
    public Text frameCount;
    public ShotDetails shotDetails;
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        this.shotName.text = shotDetails.shotName;
        this.resolution.text =   shotDetails.renderWidth.ToString() + " x " + shotDetails.renderHeight.ToString();
    }

    void TaskOnClick()
    {
        ShotDetailsSelectionEvent.RaiseOnSelected(this);
    }

    public void SelectShot()
    {
        selectionImage.SetActive(true);
        ShotIsCapture();
    }
    public void DeSelectShot()
    {
        selectionImage.SetActive(false);
        CaptureTemplateUI.Instance.captureButton.SetActive(false);
        CaptureTemplateUI.Instance.differentRotationButton.SetActive(false);
        CaptureTemplateUI.Instance.panel360PlayerDisplayCount.SetActive(false);
        CaptureTemplateUI.Instance.panel360PlayerButtons.SetActive(false);
        CaptureTemplateUI.Instance.geryedoutPanelsAtPlay.SetActive(false);
        ManageOrbitRoam.Instance.tiltMode = false;
            if(ManageOrbitRoam.Instance.CurrentMoment == "orbit")
            {
                ManageOrbitRoam.Instance.SetToOrbitMoment();
            }else if(ManageOrbitRoam.Instance.CurrentMoment == "roam")
            {
                ManageOrbitRoam.Instance.SetToRoamMoment();
            }
    }
    

    void ShotIsCapture()
    {
        if(shotDetails.isCaptured)
        {
            ManageOrbitRoam.Instance.ToSetViewMode();
            ProductHandlerManager.Instance.cameraObject.transform.position = shotDetails.cameraPosition;
            RotateObject.Instance.OldPos = ProductHandlerManager.Instance.cameraObject.transform.position;
            ProductHandlerManager.Instance.cameraObject.transform.eulerAngles = new Vector3(shotDetails.cameraRotationX,shotDetails.cameraRotationY,0);
            if(shotDetails.shotType == "Silo")
            {
            //  Debug.Log("Silo Captured");
            }
             if(shotDetails.shotType == "New360")
            {
                Player360SetUp();
                CaptureTemplateUI.Instance.panel360PlayerButtons.SetActive(true);
            }
            
        }else{
            CaptureTemplateUI.Instance.captureButton.SetActive(true);
            if(shotDetails.shotType == "Silo")
            {
              CaptureTemplateUI.Instance.differentRotationButton.SetActive(true);
            }
             if(shotDetails.shotType == "New360")
            {
              ProductHandlerManager.Instance.FrontRotationButton();
              ManageOrbitRoam.Instance.tiltMode = true;
            }
        }
    } 
    public void Player360SetUp()
    {
        CaptureTemplateUI.Instance.panel360PlayerDisplayCount.SetActive(true);
        CaptureTemplateUI.Instance.totalNumberOfFrames.text = (shotDetails.frameCount).ToString();
        CaptureTemplateUI.Instance.currentFrameNumber.text = "1";
    }
}