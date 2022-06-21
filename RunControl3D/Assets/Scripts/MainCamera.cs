using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    void Start()
    {
        // kamera ve karakter aras�ndaki bo�u�u tan�mlad�k
        targetOffset = transform.position - target.position;
    }


    void Update()
    {
        //target offseti koruyarak kameran�n pozisyonunu karakterden s�reki offsetkadar uzak hale getirdik
        //bu y�zden s�rekli karakteri bei mesafeden takip ediyo
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, 0.125f);
    }
}
