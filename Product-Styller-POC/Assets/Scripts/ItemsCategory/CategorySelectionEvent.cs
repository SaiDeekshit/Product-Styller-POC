using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelectionEvent 
{
   //Creating delegate for selection of category
   public delegate void OnCategorySelectedDetails(RefrenceCategoryDetails category);
   //Event for selection of category delegate
   public static OnCategorySelectedDetails onCategorySelectedDetails;

    //Raise on event
   public static void RaiseOnCategorySelected(RefrenceCategoryDetails category)
   {
       if(onCategorySelectedDetails != null)
        onCategorySelectedDetails(category);
   }
}
