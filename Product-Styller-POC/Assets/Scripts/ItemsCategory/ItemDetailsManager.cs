using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailsManager : MonoBehaviour
{
    private static ItemDetailsManager _instance;
    public static ItemDetailsManager Instance
    {
        get
        {
            if(_instance ==null)
            {
                Debug.Log("ItemDetailManager is null");
            }
            return _instance;
        }
    }
  
    void Awake()
    {
        _instance = this;
    }
    private RefrenceItemDetails currentItemDetails;
    public RefrenceItemDetails CurrentItemDetails
    {
        get
        {
            return currentItemDetails;
        }
        set
        {
            if(currentItemDetails != null)
            {
                currentItemDetails.OnDeSelection();
            }
            currentItemDetails = value;
            currentItemDetails.OnSelection();
            ItemsCategoryUI.Instance.captureButtonGreyOut.SetActive(false);
        }
    }
    void OnEnable()
    {
        ItemDetailsSelectionEvent.onItemDetailsSelected += SetCurrentItem;
    }
    void OnDisable()
    {
        ItemDetailsSelectionEvent.onItemDetailsSelected -= SetCurrentItem;
    }
     //Method is used for load into delegate
   void SetCurrentItem(RefrenceItemDetails item)
   { 
       CurrentItemDetails = item;
       DisplayItemInfo();
   }

   void DisplayItemInfo()
   {
       ItemsCategoryUI.Instance.productName.text = currentItemDetails.itemDetails.productName;
       ItemsCategoryUI.Instance.item.text = currentItemDetails.itemDetails.item;
       ItemsCategoryUI.Instance.model.text = currentItemDetails.itemDetails.model;
       ItemsCategoryUI.Instance.productFinish.text = currentItemDetails.itemDetails.productFinish;
       ItemsCategoryUI.Instance.dimension.text = currentItemDetails.itemDetails.dimension;
       ItemsCategoryUI.Instance.doorHandling.text = currentItemDetails.itemDetails.doorHandling;
   }
}
