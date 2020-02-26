using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UpdateScreenShotDetails : MonoBehaviour
{
    private static UpdateScreenShotDetails _instance;
    public static UpdateScreenShotDetails Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("UpdateScreenshotDetails is null");
            }
            return _instance;
        }
    }
    void Awake()
    {
     _instance = this;
    }

    public void UpdateCameraPosition(ShotDetails _shotDetail)
    {
        _shotDetail.cameraPosition = ProductHandlerManager.Instance.cameraObject.transform.position;
      RewriteJsonFile();
       
    }
    public void UpdateCameraEulerAngle(ShotDetails _shotDetail)
    {
        _shotDetail.cameraRotationX = ProductHandlerManager.Instance.cameraObject.transform.eulerAngles.x;
        _shotDetail.cameraRotationY = ProductHandlerManager.Instance.cameraObject.transform.eulerAngles.y;
        RewriteJsonFile();
       
    }
    public void UpdateIsCapture(ShotDetails _shotDetail)
    {
        _shotDetail.isCaptured = true;
       RewriteJsonFile();
    }

    public void RewriteJsonFile()
    {
      
        string jsonToSave = JsonHelper.ToJson(CreateShots.Instance.loadListOfShotDetails.Items.ToArray());
        string fileName = GetFileNames.Instance.ProductJsonFileName(ProductHandlerManager.Instance.product.name);
        File.WriteAllText(fileName,jsonToSave);
       
    }
    
    public void UpdateThumbNail(string _fileScreenShot,Image _image)
    {
        //it's not enough to just check that the file exists, since it doesn't mean it's finished saving 
        //we have to check if it can actually be opened
        Texture2D image;
        image = new Texture2D(Screen.width,Screen.height);
        image.LoadImage(File.ReadAllBytes(_fileScreenShot));
        _image.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height),Vector2.zero);

    }
    
}
