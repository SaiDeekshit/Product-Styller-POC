using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Screenshot : MonoBehaviour
{
    int count = 0;
    public float width,height;
    // public GameObject screenShotButton;
   
   public string captureFilePath;
   string folderForScreenShots;
    public void TakeScreenshot()
    {
        if(CheckFolderIsCreated.ScreenShotFolderCreation(ProductHandlerManager.Instance.product.name)){
            // Debug.Log("File is created");
              StartCoroutine("CaptureIt");
        }
      
        
    }
    IEnumerator CaptureIt()
    {
        DisableUIStuff(false);


        //wait for graphics to render
        yield return new WaitForEndOfFrame();
       
        //create a texture to pass to encoding 
        Texture2D texture = new Texture2D(GetTextureWidth() ,GetTextureHeight() ,TextureFormat.RGB24,false);
        
        //put buffer into texture
        texture.ReadPixels(new Rect(GetStartingX() ,GetStartingY(),texture.width,texture.height),0,0);
        texture.Apply();

        // split the process up --ReadPixels() and the GetPixels() call inside of encoder are both prett
        yield return 0;

        byte[] bytes = texture.EncodeToPNG();
       
        //save our test image 
        File.WriteAllBytes(FolderCreation() +"/" + ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.shotName + ".png",bytes);
    
    
        captureFilePath = (FolderCreation() +"/" + ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.shotName + ".png");
    
         Debug.Log("Completed " + hasSaved((FolderCreation() +"/" + ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.shotName + ".png")));
         string fileNameOfScreenShot = (FolderCreation() +"/" + ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.shotName + ".png");
         UpdateScreenShotDetails.Instance.UpdateCameraPosition(ShotButtonManager.CurrentRefrenceShotDetail.shotDetails);
         UpdateScreenShotDetails.Instance.UpdateCameraEulerAngle(ShotButtonManager.CurrentRefrenceShotDetail.shotDetails);
         UpdateScreenShotDetails.Instance.UpdateIsCapture(ShotButtonManager.CurrentRefrenceShotDetail.shotDetails);
         UpdateScreenShotDetails.Instance.UpdateThumbNail(fileNameOfScreenShot,ShotButtonManager.CurrentRefrenceShotDetail.thumbNail);   
         ManageOrbitRoam.Instance.ToSetViewMode();
         if(ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.shotType == ShotType.Animation)
         {
 
            ProductHandlerManager.Instance.RotateProductInFrames();
            CaptureTemplateUI.Instance.panel360PlayerDisplayCount.SetActive(true);
            CaptureTemplateUI.Instance.playButton.SetActive(true);
         }
         yield return new WaitForEndOfFrame();
       
        EnableUIStuff(true);
    }
    //Create a folder for screenshots
    string FolderCreation()
    {
        folderForScreenShots = GetFileNames.Instance.ProductFolderName(ProductHandlerManager.Instance.product.name)  + "/screenshots";
        return folderForScreenShots;
    }

    
    //To load image into thumbnail
     bool hasSaved(string _path)
    {
        //it's not enough to just check that the file exists, since it doesn't mean it's finished saving 
        //we have to check if it can actually be opened
        Texture2D image;
        image = new Texture2D(Screen.width,Screen.height);
        // bool imageLoadSuccess = image.LoadImage(File.ReadAllBytes(_path));
        return image.LoadImage(File.ReadAllBytes(_path));
    }
    
    int GetTextureWidth()
    {
        int _width;
        if(width < height)
        {
            //Actual screen width 1520
            _width =  (int)(1920 * GetAspectRatio());
        }else
        {
            _width = 1920;
        }
        return _width;
    }
    int GetTextureHeight()
    {
        int _height;
        if(width > height)
        {
            _height =  (int)(1080 *(1/GetAspectRatio()));
        }else
        {
            _height = 1080;
        }
        return _height;
    }

    float GetStartingX()
    {
        return (float)((Screen.width - GetTextureWidth()) / 2);
        
    }
    float GetStartingY()
    {
        return (float)((Screen.height - GetTextureHeight()) / 2);
       
    }

    float GetAspectRatio()
    {
       return (width / height);
    }

    void DisableUIStuff(bool _active)
    {
           CaptureTemplateUI.Instance.captureButton.SetActive(_active);
           CaptureTemplateUI.Instance.differentRotationButton.SetActive(_active);
           CaptureTemplateUI.Instance.allRotationButtons.SetActive(_active);
           CaptureTemplateUI.Instance.canvasOfUI.SetActive(false);
    }

    void EnableUIStuff(bool _active)
    {
        CaptureTemplateUI.Instance.canvasOfUI.SetActive(true);
    }

}
