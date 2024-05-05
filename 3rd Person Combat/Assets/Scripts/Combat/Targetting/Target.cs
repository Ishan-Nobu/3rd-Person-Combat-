using System;
using UnityEngine;

public class Target : MonoBehaviour
{
   public event Action<Target> DestroyEvent;

   private void OnDestroyed()
   {    
        DestroyEvent?.Invoke(this);
   }
}
