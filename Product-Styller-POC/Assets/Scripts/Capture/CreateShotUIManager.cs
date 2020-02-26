using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShotUIManager : MonoBehaviour
{
    private static CreateShotUIManager _instance;
    public static CreateShotUIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("CreateShotManager is null");
            }
            return _instance;
        }
    }
    void Awake()
    {
     _instance = this;   
    }
      //To Enable Create Silo Panel
    public void EnableCreateSiloPanel()
    {
        CaptureTemplateUI.Instance.newSiloShotPanel.SetActive(true);
    }

    //To Enable Create 360 Panel
    public void EnableCreateNew360Panel()
    {
        CaptureTemplateUI.Instance.new360ShotPanel.SetActive(true);
    }

    //For Silo panel cancel button
    public void SiloCancelButton()
    {
         CaptureTemplateUI.Instance.newSiloShotPanel.SetActive(false);
         ClearAllInputFeilds();
    }
    //For Silo panel cancel button
    public void New360CancelButton()
    {
          CaptureTemplateUI.Instance.new360ShotPanel.SetActive(false);
          CaptureTemplateUI.Instance.betweenShotsDropDown.value = 0;
          ForCounterClockWiseDirection();
          ClearAllInputFeilds();
    }

    //For Toggle 360 rotation direction
    public void ForClockWiseDirection()
    {
        CaptureTemplateUI.Instance.clockwise.SetActive(false);
    }
     //For Toggle 360 rotation direction
    public void ForCounterClockWiseDirection()
    {
        CaptureTemplateUI.Instance.clockwise.SetActive(true);
    }
    //For closing 360 shots player
    public void ForCloseShotsPlayer()
    {
        if(ShotButtonManager.CurrentRefrenceShotDetail != null ){
        ShotButtonManager.CurrentRefrenceShotDetail = ShotButtonManager.CurrentRefrenceShotDetail;
        }
    }
    public void ClearAllInputFeilds()
    {
        CaptureTemplateUI.Instance.nameOf360Shot.text = "";
        CaptureTemplateUI.Instance.nameOfSiloShot.text = "";
        CaptureTemplateUI.Instance.width360.text = "";
        CaptureTemplateUI.Instance.widthSilo.text = "";
        CaptureTemplateUI.Instance.height360.text = "";
        CaptureTemplateUI.Instance.heightSilo.text = "";
    }
    //To view shot shot list 
    public void ShotButton()
    {
        
        CaptureTemplateUI.Instance.openShotList = !CaptureTemplateUI.Instance.openShotList;
        CaptureTemplateUI.Instance.shotListView.SetBool("Shotslist",CaptureTemplateUI.Instance.openShotList);
    }
}
