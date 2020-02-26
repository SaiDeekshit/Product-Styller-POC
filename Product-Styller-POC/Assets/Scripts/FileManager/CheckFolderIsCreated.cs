using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CheckFolderIsCreated : MonoBehaviour
{
    
    //Create a folder for product to save json 
    public static bool JsonFolderCreation(string _productName)
    {
        string folderPath = GetFileNames.Instance.ProductFolderName(_productName)  + "/json";
       
        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); 
        }
        return true;
    }
     //Create a folder for product to save screenshot 
    public static bool ScreenShotFolderCreation(string _productName)
    {
        string folderPath = GetFileNames.Instance.ProductFolderName(_productName)  + "/screenshots";
       
        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); 
        }
        return true;
    }
}
