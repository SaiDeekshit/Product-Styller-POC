using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemDetails
{
    public string categoryName;
    public string productName;
    public string item;
    public string model;
    public string productFinish;
    public string dimension;
    public string doorHandling;
    public string pathForModel;
    public string pathForThumbNail;

    public ItemDetails(string _categoryName,string _productName,string _item,string _model,string _productFinish,string _dimension,string _doorHandling,string _pathForModel,string _pathForThumbNail)
    {
        this.categoryName = _categoryName;
        this.productName = _productName;
        this.item = _item;
        this.model = _model;
        this.productFinish = _productFinish;
        this.dimension = _dimension;
        this.doorHandling = _doorHandling;
        this.pathForModel = _pathForModel;
        this.pathForThumbNail = _pathForThumbNail;
    }
    
}
[System.Serializable]
public class ListOfItemDetails
{
    public List<ItemDetails> listOfProducts;
}