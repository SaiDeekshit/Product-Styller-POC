using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductHandlerManager : MonoBehaviour
{

    private static ProductHandlerManager _instance;
    public static ProductHandlerManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("ProductHandler Manager is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public GameObject cameraObject;
    public GameObject product = null;
    public Vector3 intialPosition = new Vector3(0,0,10f);
    bool breakLoop;
    Bounds boundsForDifferentRotation;
    //For product
    GameObject parentObject;
    public string filePathForAssets;
   
    
    void Start() {
        parentObject = new GameObject("ProductParent");
    }

     public void DownloadProduct(string _productName)
    {
         StartCoroutine("DownloadAndCache",_productName);
    }
  
    IEnumerator DownloadAndCache (string _productName){
        // string fileName = "C:/Users/hobb/Desktop/Assets/" + _productName;
        // string fileName = "U:/SaiDeekshith/Assets/" + _productName;
        string fileName = filePathForAssets + "/" + _productName;
            var bundleLoadedRequest = AssetBundle.LoadFromFileAsync(fileName);
              yield return bundleLoadedRequest;
           
              var myLoadedAssetBundle = bundleLoadedRequest.assetBundle;

              if(myLoadedAssetBundle == null){
                  Debug.Log("Failed to load AssetBundle!");
              }
              var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>(_productName);
              yield return assetLoadRequest;


            
              product = assetLoadRequest.asset as GameObject;
            

                     ProductHandlerManager.Instance.SetUpProduct(product);
          
            myLoadedAssetBundle.Unload(false);
    }
    public void SetUpProduct(GameObject _product)
    {
        _product.AddComponent<RotateCam>();
        _product.GetComponent<RotateCam>().enabled = false;

        this.product = Instantiate(_product);
        //Adding box collider for size
       product.AddComponent<BoxCollider>();
       
        ManageOrbitRoam.Instance.ToSetOrbit();
        
        CreateShots.Instance.LoadPreviousShots(product.name);
       
    }
    public void BackButton()
    {
            Destroy(product);
            product = null;
            ManageOrbitRoam.Instance.rotateMode = false;
            ItemsCategoryUI.Instance.panelItemsCategory.SetActive(true);
            CreateShots.Instance.loadListOfShotDetails.Items.Clear();
            CreateShotUIManager.Instance.ShotButton();
            CreateShotUIManager.Instance.SiloCancelButton();
            CreateShotUIManager.Instance.New360CancelButton(); 
            CaptureTemplateUI.Instance.PanelDifferentRotations.SetActive(false);
            CaptureTemplateUI.Instance.allRotationButtons.SetActive(false);
            CaptureTemplateUI.Instance.differentRotationButton.SetActive(false);
            CaptureTemplateUI.Instance.captureButton.SetActive(false);
            ManageOrbitRoam.Instance.cameraObject.transform.position = Vector3.zero;
            ManageOrbitRoam.Instance.cameraObject.transform.eulerAngles = Vector3.zero;
            CaptureTemplateUI.Instance.panel360PlayerDisplayCount.SetActive(false);
            foreach (Transform child in CaptureTemplateUI.Instance.shotsContainer.transform) {
             GameObject.Destroy(child.gameObject);
             }
    }

    public void DifferentRotationButton()
    {
        CaptureTemplateUI.Instance.PanelDifferentRotations.SetActive(true);
    }
    float CentreOfGravityOfProduct()
    {
     return ProductHandlerManager.Instance.product.GetComponent<BoxCollider>().center.y;
    }
   
    //For different types of rotation button
    public void TopRotationButton()
    {
        ReCentreForDifferentRotation();
        
        ManageOrbitRoam.Instance.cameraObject.transform.position = new Vector3(0,RotateObject.Instance.bounds.center.y + RotateObject.distance ,0);
       
         ManageOrbitRoam.Instance.cameraObject.transform.eulerAngles = new Vector3(90f,0f,0f);

    }
    public void FrontRotationButton()
    {
        ReCentreForDifferentRotation();
        ManageOrbitRoam.Instance.cameraObject.transform.position = new Vector3(0,RotateObject.Instance.bounds.center.y,-RotateObject.distance);
        LookAtProduct();
    }
    public void BackRotationButton()
    {
       ReCentreForDifferentRotation();
       ManageOrbitRoam.Instance.cameraObject.transform.position = new Vector3(0,RotateObject.Instance.bounds.center.y,RotateObject.distance);
        LookAtProduct();
    }
    public void Left90RotationButton()
    {
       ReCentreForDifferentRotation();
       ManageOrbitRoam.Instance.cameraObject.transform.position = new Vector3(-RotateObject.distance,RotateObject.Instance.bounds.center.y,0);
       LookAtProduct();
    }
    public void Right90RotationButton()
    {
       ReCentreForDifferentRotation();
       ManageOrbitRoam.Instance.cameraObject.transform.position = new Vector3(RotateObject.distance,RotateObject.Instance.bounds.center.y,0);
       LookAtProduct();
    }
    public void Left45RotationButton()
    {
        ReCentreForDifferentRotation();
        ManageOrbitRoam.Instance.cameraObject.transform.position = new Vector3(-GetCornerValue(),RotateObject.Instance.bounds.center.y,-GetCornerValue());
        LookAtProduct();
    }
    public void Right45RotationButton()
    {
        ReCentreForDifferentRotation();
        ManageOrbitRoam.Instance.cameraObject.transform.position = new Vector3(GetCornerValue(),RotateObject.Instance.bounds.center.y,-GetCornerValue());
        LookAtProduct();
    }
    //For corner point at 45 degrees
    float GetCornerValue()
    {
        return (RotateObject.distance / Mathf.Sqrt(2));
    }
    void ReCentreForDifferentRotation()
    {
     Vector3 recentre = new Vector3((product.transform.position.x + product.GetComponent<BoxCollider>().center.x),(product.transform.position.y + product.GetComponent<BoxCollider>().center.y),(product.transform.position.z + product.GetComponent<BoxCollider>().center.z));
     RotateObject.distance  = Vector3.Distance(recentre, ManageOrbitRoam.Instance.cameraObject.transform.position);
    }
    void LookAtProduct(){
        Vector3 lookCentre = new Vector3((product.transform.position.x + product.GetComponent<BoxCollider>().center.x),(product.transform.position.y + product.GetComponent<BoxCollider>().center.y),(product.transform.position.z + product.GetComponent<BoxCollider>().center.z));
        ManageOrbitRoam.Instance.cameraObject.transform.LookAt(lookCentre);
    }
    
    //Method for rotation for capturing in between shots
    //This is also used for play shots
    public void RotateProductInFrames()
    {
        CreateShotUIManager.Instance.ShotButton();
        ShotButtonManager.CurrentRefrenceShotDetail.Player360SetUp();
        CaptureTemplateUI.Instance.geryedoutPanelsAtPlay.SetActive(true);
        CaptureTemplateUI.Instance.playButton.SetActive(false);
        StartCoroutine("RotateIt");
        breakLoop = false;
    }
    //for pause button to 360 shots player
    public void PauseButton()
    {
        ShotButtonManager.CurrentRefrenceShotDetail.DeSelectShot();
        ShotButtonManager.CurrentRefrenceShotDetail = ShotButtonManager.CurrentRefrenceShotDetail;
        CaptureTemplateUI.Instance.playButton.SetActive(true);
        breakLoop = true;
    }

    IEnumerator RotateIt()
    {
        for(int i = 0 ; i < ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.frameCount ; i ++) {  
                yield return new WaitForSeconds(1f);
                if(breakLoop){
                break;
                }
                if(ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.direction == Direction.RotationDirection.clockwise){
                ForwardMonent();
                }if(ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.direction == Direction.RotationDirection.counterClockwise){
                BackwardMonentForPlay();
                }
        }
         product.transform.SetParent(null);
    }
    //for forward moment in 360 shots player
    public void ForwardMonent()
    {
        if(int.Parse(CaptureTemplateUI.Instance.currentFrameNumber.text) <= int.Parse(CaptureTemplateUI.Instance.totalNumberOfFrames.text))
        {
            MakeRotationForward();
            product.transform.SetParent(null);
            if(int.Parse(CaptureTemplateUI.Instance.currentFrameNumber.text) < int.Parse(CaptureTemplateUI.Instance.totalNumberOfFrames.text)){
            int count = int.Parse(CaptureTemplateUI.Instance.currentFrameNumber.text) + 1;
            CaptureTemplateUI.Instance.currentFrameNumber.text = count.ToString();
            }else{
                CaptureTemplateUI.Instance.currentFrameNumber.text = "1";
            }
        }
    }
    //for backward moment in 360 shots player for capture or play
    public void BackwardMonentForPlay()
    {
        if(int.Parse(CaptureTemplateUI.Instance.currentFrameNumber.text) <= int.Parse(CaptureTemplateUI.Instance.totalNumberOfFrames.text))
        {
            MakeRotationBackward();
            product.transform.SetParent(null);
            if(int.Parse(CaptureTemplateUI.Instance.currentFrameNumber.text) < int.Parse(CaptureTemplateUI.Instance.totalNumberOfFrames.text)){
            int count = int.Parse(CaptureTemplateUI.Instance.currentFrameNumber.text) + 1;
            CaptureTemplateUI.Instance.currentFrameNumber.text = count.ToString();
            }else{
                CaptureTemplateUI.Instance.currentFrameNumber.text = "1";
            }
        }
    }
    
    public void MakeRotationForward()
    {
        parentObject.transform.position = product.transform.position;
        product.transform.SetParent(parentObject.transform);
        Vector3 loadYEulerAngle;
        loadYEulerAngle = parentObject.transform.eulerAngles;
        float angle = ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.degrees;
        parentObject.transform.eulerAngles = new Vector3(loadYEulerAngle.x,loadYEulerAngle.y + angle, loadYEulerAngle.z);
    }
    public void MakeRotationBackward()
    {
        parentObject.transform.position = product.transform.position;
        product.transform.SetParent(parentObject.transform);
        Vector3 loadYEulerAngle;
        loadYEulerAngle = parentObject.transform.eulerAngles;
        float angle = ShotButtonManager.CurrentRefrenceShotDetail.shotDetails.degrees;
        parentObject.transform.eulerAngles = new Vector3(loadYEulerAngle.x,loadYEulerAngle.y - angle, loadYEulerAngle.z);
    }

}
