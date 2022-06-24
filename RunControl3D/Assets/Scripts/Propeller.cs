using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public Animator _Animator;
    public float waitingTime;
    public BoxCollider _wind;

    

    public void AnimationSituation(string situation) 
    {

        if(situation == "true")
        {
            _Animator.SetBool("Run", true);
            _wind.enabled = true;
        }
        else 
        {
            _Animator.SetBool("Run", false);
            StartCoroutine(AnimationTrigger());
            _wind.enabled = false;
        }

        IEnumerator AnimationTrigger() 
        {
            yield return new WaitForSeconds(waitingTime);
            AnimationSituation("true");
        }
    }
    
}
