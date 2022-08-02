using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAnimator : MonoBehaviour
{

    public Animator _SavedAnimator;
   public void Deactivate() 
   {

        _SavedAnimator.SetBool("ok", false);

   }
}
