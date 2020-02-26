using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotButtonManager : MonoBehaviour
{
    private static ShotButtonManager _instance;
    public static ShotButtonManager Instance
    {
        get{
            if(_instance == null)
            {
                Debug.Log("ShotButtonManager is null");
            }
            return _instance;
        }
    }
 
    void Awake()
    {
        _instance = this;
    }

    private static RefrenceShotDetails _currentRefrenceShotDetail;
    public static RefrenceShotDetails CurrentRefrenceShotDetail
    {
        get
        {
            return _currentRefrenceShotDetail;
        }
        set
        {
                if(_currentRefrenceShotDetail != null)
                {
                    if(_currentRefrenceShotDetail != value)
                    {
                        _currentRefrenceShotDetail.DeSelectShot();
                        _currentRefrenceShotDetail = value;
                        _currentRefrenceShotDetail.SelectShot();
                    }else
                    {
                        _currentRefrenceShotDetail.DeSelectShot();
                        _currentRefrenceShotDetail = null;
                    }
                }
                else{
                    _currentRefrenceShotDetail = value;
                    _currentRefrenceShotDetail.SelectShot();
                }
           }
            
        }
    

    void OnEnable() {
         
      ShotDetailsSelectionEvent.onShotDetailSelected += SetRefernceShot;    
    }
    void OnDisable() {
        ShotDetailsSelectionEvent.onShotDetailSelected -= SetRefernceShot; 
    }
    void SetRefernceShot(RefrenceShotDetails shot)
    {
        CurrentRefrenceShotDetail = shot;
    }
  
}
