using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

using LINQtoCSV;
using System.Linq;
public class ItemsCategoryUI : MonoBehaviour
{
    private static ItemsCategoryUI _instance;
    public static ItemsCategoryUI Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("ItemCategory is null");
            }
            return _instance;
        }
    }
 
    void Awake()
    {
        _instance = this;
    }
    public GameObject panelItemsCategory;
    public ListOfItemDetails listOfItemDetails;
    public GameObject itemPrefab;
    public GameObject itemContainer;

    public GameObject categoryPrefab;
    public GameObject categoryContainer;
    public List<string> listOfCategories;
    public List<string> listOfProductNames;
    public GameObject captureButtonGreyOut;
    public InputField findItem;

    public TextAsset sceneDescriptor;


    public Text productName,item,model,productFinish,dimension,doorHandling;
    void Start() {
        ItemsCategoryUI.Instance.captureButtonGreyOut.SetActive(true);


         CsvFileDescription inputFileDecription = new CsvFileDescription
        {
             
            SeparatorChar = ',',
            FirstLineHasColumnNames = true
        };
       
         using (var ms = new MemoryStream(sceneDescriptor.text.Length))
        {
            
          using (var txtWriter = new StreamWriter(ms))
            {
                  
                using (var txtReader = new StreamReader(ms))
                  {
                     
                      txtWriter.Write(sceneDescriptor.text);
                      txtWriter.Flush();
                      ms.Seek(0, SeekOrigin.Begin);
                      
                    //Read the data from the csv file
                    CsvContext cc = new CsvContext();
                 
                    cc.Read<CsvSceneObject>(txtReader, inputFileDecription)
                    
                       .ToList()
                       .ForEach(item => 
                            { 
                                 ItemDetails newItemDetails = new ItemDetails(item.categoryName,item.productName,item.item,item.model,item.productFinish,item.dimension,item.doorHandling,item.pathForModel,item.pathForThumbNail);
                                GameObject itemBox = Instantiate(itemPrefab);
                                itemBox.GetComponent<RefrenceItemDetails>().itemDetails = newItemDetails;
                                itemBox.transform.SetParent(itemContainer.transform);
                               
                                LoadThumbNailItem(itemBox.GetComponent<RefrenceItemDetails>().selectedImage.GetComponent<Image>(),item.pathForThumbNail);
                                LoadThumbNailItem(itemBox.GetComponent<RefrenceItemDetails>().unselectedImage.GetComponent<Image>(),item.pathForThumbNail);
                                listOfProductNames.Add(item.productName);
                                CreatingCategories(item.categoryName,newItemDetails);
                                itemBox.SetActive(false);
                                });
                            }
                        }
                  }
    
    }
    //To load image into thumbnail
     void LoadThumbNailItem(Image _image,string _path)
    {
        if(File.Exists(_path)){
            
        }
        //it's not enough to just check that the file exists, since it doesn't mean it's finished saving 
        //we have to check if it can actually be opened
        Texture2D image;
        image = new Texture2D(Screen.width,Screen.height);
        image.LoadImage(File.ReadAllBytes(_path));
        _image.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height),Vector2.zero);

    }

    void CreatingCategories(string _categoryName,ItemDetails _itemDetails)
    {

                if(CheckCategoryIs(_categoryName)){
                listOfCategories.Add(_categoryName);
                GameObject categoryBox = Instantiate(categoryPrefab);
                categoryBox.transform.parent = categoryContainer.transform;
                categoryBox.GetComponent<RefrenceCategoryDetails>().itemDetails = _itemDetails;
                // categoryBox.GetComponent<RefrenceCategoryDetails>().nameOfCategory.text = _categoryName;
                }
    }

    bool CheckCategoryIs(string _categoryName)
    {
        foreach(string name in listOfCategories)
        {
            if(name == _categoryName)
            {
               return false; 
            }
        }
        return true;
    }

    public void DownloadCurrentProduct()
    {
       
       ProductHandlerManager.Instance.DownloadProduct(ItemDetailsManager.Instance.CurrentItemDetails.itemDetails.pathForModel);
       panelItemsCategory.SetActive(false);
       ManageOrbitRoam.Instance.rotateMode = true;
    }
    public void FindItemInputFeild()
    {
        
        if(findItem.text != "")
        {
            DisableAllItems();
            EnableOrDisableItems(findItem.text);
            EnableOrDisableItems2(findItem.text);
        }else{
            EnableFirstCategoryItems();
        }
    }

    //For find item onValidate with inputfeild
    void EnableOrDisableItems(string _inputText)
      {
           foreach(Transform item in  itemContainer.transform)
          {

             if(item.gameObject.GetComponent<RefrenceItemDetails>().itemDetails.categoryName.Contains(_inputText.ToLower()) || item.gameObject.GetComponent<RefrenceItemDetails>().itemDetails.categoryName.Contains(_inputText.ToUpper()) )
            {
               Debug.Log("Find Items " + _inputText + " " +item.gameObject.GetComponent<RefrenceItemDetails>().itemDetails.categoryName);
                 item.gameObject.SetActive(true);
            }
            
        } 
      }
      //For find item onValidate with inputfeild
    void EnableOrDisableItems2(string _inputText)
      {
           foreach(Transform item in  itemContainer.transform)
          {
           
            if(item.gameObject.GetComponent<RefrenceItemDetails>().itemDetails.item.Contains(_inputText.ToLower()) || item.gameObject.GetComponent<RefrenceItemDetails>().itemDetails.item.Contains(_inputText.ToUpper()) )
            {
                 
                 item.gameObject.SetActive(true);
            }
        } 
      }
       void DisableAllItems()
      {
           foreach(Transform item in  itemContainer.transform)
          {
           item.gameObject.SetActive(false);
          } 
      }
       void EnableFirstCategoryItems()
      {
           foreach(Transform item in  itemContainer.transform)
          {
          if(listOfCategories[0] == item.GetComponent<RefrenceItemDetails>().itemDetails.categoryName){
           item.gameObject.SetActive(true);
          } else
          {
           item.gameObject.SetActive(false); 
          }
        }
      }
   
}


