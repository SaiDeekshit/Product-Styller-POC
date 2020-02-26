using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureTemplateUI : MonoBehaviour
{
    private static CaptureTemplateUI _instance;
    public static CaptureTemplateUI Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("Capture Template is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public GameObject newSiloShotPanel;
    public GameObject new360ShotPanel;
    public InputField nameOfSiloShot,widthSilo,heightSilo;
    public InputField nameOf360Shot,width360,height360;
    public Dropdown betweenShotsDropDown;
    public GameObject clockwise,counterClockwise;
    public GameObject ShotButtonPrefab;
    public GameObject shotsContainer;
    public GameObject captureButton;
    public GameObject differentRotationButton;
    public GameObject allRotationButtons;
    
    //For Disabling UI stuff when capture the screenshot
    public GameObject canvasOfUI;
    //To enable different rotation buttons
    public GameObject PanelDifferentRotations;
    //To enable 360 shots animation play panel - DisplayCount
    public GameObject panel360PlayerDisplayCount;
    public GameObject playButton;
    public GameObject panel360PlayerButtons;
    public GameObject geryedoutPanelsAtPlay;
    public Text totalNumberOfFrames;
    public Text currentFrameNumber;
    public Animator shotListView;
    public bool openShotList;
    public void ForSiloButton()
    {
        newSiloShotPanel.SetActive(true);
    }
    public void For360Button()
    {
        new360ShotPanel.SetActive(true);
    }
    public void ClearInputFeilds()
    {
        nameOfSiloShot.text = "";
        widthSilo.text = "";
        heightSilo.text = "";
        nameOf360Shot.text = "";
        width360.text = "";
        height360.text = "";
    }
}
