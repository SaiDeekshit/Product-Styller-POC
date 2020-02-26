using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For saving Product position
[System.Serializable]
public class ProductCameraPosition 
{

   public string productName;

   public Vector3 productPosition;
   
   public Vector3 productRotation;
   public Vector3 cameraPosition;
   public Vector3 cameraRotation;
   public float cameraFOV;


   public ProductCameraPosition(string _productName,Vector3 _productPosition,Vector3 _productRotation,Vector3 _cameraPosition,Vector3 _cameraRotation,float _cameraFOV)
   {
      this.productName = _productName;
       this.productPosition = _productPosition;
       this.productRotation = _productRotation;
       this.cameraPosition = _cameraPosition;
       this.cameraRotation = _cameraRotation;
       this.cameraFOV = _cameraFOV;
   }
}
[System.Serializable]
public class LoadedProductCameraList
{
   public List<ProductCameraPosition> Items;
}
