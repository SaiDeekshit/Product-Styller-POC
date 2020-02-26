using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateShots : MonoBehaviour
{
    private static CreateShots _instance;
    public static CreateShots Instance{
        get
        {
            if(_instance == null)
            {
                Debug.Log("CreatedShots is null");
            }
            return _instance;
        }
    }
    
    void Awake()
    {
        _instance = this;
    }
    public ListOfShotDetails loadListOfShotDetails;

    public Direction.RotationDirection direction;

    public void LoadPreviousShots(string _productName)
    {
        if(File.Exists(GetFileNames.Instance.ProductJsonFileName(_productName))){
                 string loadJson = File.ReadAllText(GetFileNames.Instance.ProductJsonFileName(_productName));
                loadListOfShotDetails  = JsonUtility.FromJson<ListOfShotDetails>(loadJson); 
                foreach(ShotDetails shots in loadListOfShotDetails.Items)
                {
                  if(shots.productName == ProductHandlerManager.Instance.product.name){
                    GameObject newShotButton = GameObject.Instantiate(CaptureTemplateUI.Instance.ShotButtonPrefab);
                    newShotButton.GetComponent<RefrenceShotDetails>().shotDetails = shots;
                    newShotButton.transform.SetParent(CaptureTemplateUI.Instance.shotsContainer.transform);
                    if(shots.shotType == "New360")
                    {
                        newShotButton.GetComponent<RefrenceShotDetails>().frameCount.text = "# of shots : " + shots.frameCount;
                    }
                    if(shots.isCaptured)
                    {
                      string fileNameOfScreenShot = GetFileNames.Instance.ProductFolderName(ProductHandlerManager.Instance.product.name)  + "/screenshots";
                       fileNameOfScreenShot += "/" + shots.shotName + ".png";
                      UpdateScreenShotDetails.Instance.UpdateThumbNail(fileNameOfScreenShot,newShotButton.GetComponent<RefrenceShotDetails>().thumbNail);
                    }
                  }
                }
        }
    }
   

    //To create silo shot
    public void CreateSilo()
   {
      if(!CheckSiloInputFeildIsNull())
      {
          if(IsNameOfShot(loadListOfShotDetails,CaptureTemplateUI.Instance.nameOfSiloShot.text)){
          ShotDetails newShot = new ShotDetails(ProductHandlerManager.Instance.product.name,CaptureTemplateUI.Instance.nameOfSiloShot.text,int.Parse(CaptureTemplateUI.Instance.widthSilo.text),int.Parse(CaptureTemplateUI.Instance.heightSilo.text));
          if(CheckFolderIsCreated.JsonFolderCreation(ProductHandlerManager.Instance.product.name))
          {
              CreateJsonFile(newShot);
              GameObject newShotButton = GameObject.Instantiate(CaptureTemplateUI.Instance.ShotButtonPrefab);
              newShotButton.GetComponent<RefrenceShotDetails>().shotDetails = newShot;
              newShotButton.transform.SetParent(CaptureTemplateUI.Instance.shotsContainer.transform);
              CreateShotUIManager.Instance.SiloCancelButton();
              //To open shots panel after shot creation
              CreateShotUIManager.Instance.ShotButton();
           }
         }
      }
   }

   //To Create 360 shot
   public void CreateNew360Shot()
   {
       if(!Check360InputFeildIsNull())
      {
       if(IsNameOfShot(loadListOfShotDetails,CaptureTemplateUI.Instance.nameOf360Shot.text)){
       int loadBetweenShots = int.Parse(CaptureTemplateUI.Instance.betweenShotsDropDown.options[CaptureTemplateUI.Instance.betweenShotsDropDown.value].text);
    
        if(CaptureTemplateUI.Instance.clockwise.activeInHierarchy)
        {
            direction = Direction.RotationDirection.clockwise;
        }
        else{
            direction = Direction.RotationDirection.counterClockwise;
        }
       ShotDetails newShot = new ShotDetails(ProductHandlerManager.Instance.product.name,CaptureTemplateUI.Instance.nameOf360Shot.text,int.Parse(CaptureTemplateUI.Instance.width360.text),int.Parse(CaptureTemplateUI.Instance.height360.text),loadBetweenShots,GetBetweenShots(loadBetweenShots),direction);
          if(CheckFolderIsCreated.JsonFolderCreation(ProductHandlerManager.Instance.product.name))
          {
              CreateJsonFile(newShot);
              GameObject newShotButton = GameObject.Instantiate(CaptureTemplateUI.Instance.ShotButtonPrefab);
              newShotButton.GetComponent<RefrenceShotDetails>().shotDetails = newShot;
              newShotButton.transform.SetParent(CaptureTemplateUI.Instance.shotsContainer.transform);
              newShotButton.GetComponent<RefrenceShotDetails>().frameCount.text = "# of shots : " + newShot.frameCount;
              CreateShotUIManager.Instance.New360CancelButton();
              //To open shots panel after shot creation
              CreateShotUIManager.Instance.ShotButton();
          }
       }
      }
   }
    //Check wether input feilds are not null at silo 
    bool CheckSiloInputFeildIsNull()
     {
      if((CaptureTemplateUI.Instance.nameOfSiloShot.text != "") && (CaptureTemplateUI.Instance.widthSilo.text != "") && (CaptureTemplateUI.Instance.heightSilo.text != ""))
      {
          return false;
      }
      return true;
     }

    //Check wether input feilds are not null at silo 
    bool Check360InputFeildIsNull()
     {
      if((CaptureTemplateUI.Instance.nameOf360Shot.text != "") && (CaptureTemplateUI.Instance.width360.text != "") && (CaptureTemplateUI.Instance.height360.text != ""))
      {
          return false;
      }
      return true;
     }
     //Json file for saving shots list
   public void CreateJsonFile(ShotDetails _shotDetails)
    {
     loadListOfShotDetails.Items.Add(_shotDetails);
     string jsonToSave = JsonHelper.ToJson(loadListOfShotDetails.Items.ToArray());
     string fileName = GetFileNames.Instance.ProductJsonFileName(ProductHandlerManager.Instance.product.name);
     File.WriteAllText(fileName,jsonToSave);
    }
    //To get number of frames for new360 shots
    int GetBetweenShots(int _loadBetweenShots)
   {
       return ((360 / _loadBetweenShots));
   }
   
   //Check shotname is there are not
   bool IsNameOfShot(ListOfShotDetails _listOfShots,string _newName)
   {
       if(_listOfShots.Items.Count == 0){
           return true;
       }
       foreach(ShotDetails shot in _listOfShots.Items)
       {
           if(shot.shotName == _newName)
           {
               return false;
           }
       }
       return true;
   }
   //Delete Shot from list
   public void DeleteShot()
   {
       if(ShotButtonManager.CurrentRefrenceShotDetail != null){
                foreach (Transform item in CaptureTemplateUI.Instance.shotsContainer.transform)
                {
                    if(item.gameObject.GetComponent<RefrenceShotDetails>().shotName.text == ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.shotName)
                    {
                           
                            Destroy(item.gameObject);
                            loadListOfShotDetails.Items.Remove(ShotButtonManager.CurrentRefrenceShotDetail.shotDetails);
                            CaptureTemplateUI.Instance.SetActiveCaptureButtons();
                            ManageOrbitRoam.Instance.GetBackPreviousMoment();
                            UpdateScreenShotDetails.Instance.RewriteJsonFile();
                            break;
                        
                    }
                }
       }
     }

   

}
