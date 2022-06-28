using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public GameObject finalPosition;
    public bool finalBattle;
    void Start()
    {
        // kamera ve karakter arasýndaki boþuðu tanýmladýk
        targetOffset = transform.position - target.position;
    }


    void LateUpdate()
    {
        /*Eðer final battle sahnesi baþlamadýysa target offseti koruyarak kameranýn pozisyonunu karakterden süreki offsetkadar uzak hale getirdik, bu yüzden sürekli karakteri 
          belli mesafeden takip ediyor ama final battle sahnesi baþladýysa kamera istenmilen posizyona geçicek*/
        
        if (!finalBattle)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, 0.125f);
        }
        else 
        {
            transform.position = Vector3.Lerp(transform.position, finalPosition.transform.position, 0.015f);
        }
        
    }
}
