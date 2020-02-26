using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailsSelectionEvent 
{
     public delegate void OnItemDetailsSelected(RefrenceItemDetails item);
    public static event OnItemDetailsSelected onItemDetailsSelected;

    
    public static void RaiseOnSelected(RefrenceItemDetails item)
    {
        if(onItemDetailsSelected != null)
          onItemDetailsSelected(item);
    }
}
