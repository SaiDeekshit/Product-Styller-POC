using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryDetailsManager : MonoBehaviour
{
    private static CategoryDetailsManager _instance;
    public static CategoryDetailsManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("CategoryDetailsManager is null");
            }
            return _instance;
        }
    }
 
    void Awake()
    {
        _instance = this;
    }

    private RefrenceCategoryDetails currentCategory;
    public RefrenceCategoryDetails CurrentCategory
    {
        get
        {
            return currentCategory;
        }
        set
        {
            if(currentCategory != null)
            {
                currentCategory.OnDeSelection();
            }
            currentCategory = value;
            currentCategory.OnSelection();
        }
    }
    
    void OnEnable()
    {
        CategorySelectionEvent.onCategorySelectedDetails += SetCurrentCategory;
    }
    void OnDisable()
    {
        CategorySelectionEvent.onCategorySelectedDetails -= SetCurrentCategory;
    }
     //Method is used for load into delegate
   void SetCurrentCategory(RefrenceCategoryDetails category)
   { 
       CurrentCategory = category;
     
   }
}
