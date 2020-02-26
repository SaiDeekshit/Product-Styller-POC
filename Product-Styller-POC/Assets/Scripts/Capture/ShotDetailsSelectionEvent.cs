using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDetailsSelectionEvent
{
   public delegate void OnShotDetailSelected(RefrenceShotDetails shot);
   public static event OnShotDetailSelected onShotDetailSelected;

   public static void RaiseOnSelected(RefrenceShotDetails shot)
   {
       if(onShotDetailSelected != null)
       {
           onShotDetailSelected(shot);
       }
   }
}
