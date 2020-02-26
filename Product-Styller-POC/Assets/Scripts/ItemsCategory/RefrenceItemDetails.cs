using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefrenceItemDetails : MonoBehaviour
{
    public Text nameItemSelected;
    public Text nameItemUnSelected;
    public Image selectedImage;
    public Image unselectedImage;
    public GameObject unselectedPanel;
    

    public ItemDetails itemDetails;

    void Start() {
        nameItemSelected.text = "item# - " + itemDetails.item;
        nameItemUnSelected.text = "item# - " + itemDetails.item;
        this.GetComponent<Button>().onClick.AddListener(ClickOnItem);
    }

      void ClickOnItem()
      {
       
        ItemDetailsSelectionEvent.RaiseOnSelected(this);
        
      }
      public void OnSelection() {
        unselectedPanel.SetActive(false);
      }
       public void OnDeSelection() {
         unselectedPanel.SetActive(true);
      }
}
