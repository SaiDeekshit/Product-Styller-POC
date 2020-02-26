using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefrenceCategoryDetails : MonoBehaviour
{   
     public Text nameOfCategory;
     public ItemDetails itemDetails;

     void Start() {
         nameOfCategory.text = itemDetails.categoryName;
        this.GetComponent<Button>().onClick.AddListener(ClickOnCategory);
      }

      void ClickOnCategory()
      {
        CategorySelectionEvent.RaiseOnCategorySelected(this);

      }
      public void OnSelection() {
        EnableOrDisableItems(true);
      }
       public void OnDeSelection() {
           EnableOrDisableItems(false);
      }

      void EnableOrDisableItems(bool _active)
      {
           foreach(Transform item in  ItemsCategoryUI.Instance.itemContainer.transform)
            {
            if(item.gameObject.GetComponent<RefrenceItemDetails>().itemDetails.categoryName  == this.itemDetails.categoryName){
              item.gameObject.SetActive(_active);
            }
        } 
      }
   
}
