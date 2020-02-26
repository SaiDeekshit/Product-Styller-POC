using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFileNames : MonoBehaviour
{
    private static GetFileNames _instance;
    public static GetFileNames Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("Get FileName is null");
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    public string driveFilePathForProduct;
   
   
    public string ProductFolderName(string _productName)
    {
         return driveFilePathForProduct + "/" + _productName;
    }
    public string ProductJsonFileName(string _productName)
    {
        return ProductFolderName(_productName) + "/json/shotDetails.json";
    }
}
