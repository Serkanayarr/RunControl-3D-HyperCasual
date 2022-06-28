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
        // kamera ve karakter aras�ndaki bo�u�u tan�mlad�k
        targetOffset = transform.position - target.position;
    }


    void LateUpdate()
    {
        /*E�er final battle sahnesi ba�lamad�ysa target offseti koruyarak kameran�n pozisyonunu karakterden s�reki offsetkadar uzak hale getirdik, bu y�zden s�rekli karakteri 
          belli mesafeden takip ediyor ama final battle sahnesi ba�lad�ysa kamera istenmilen posizyona ge�icek*/
        
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
