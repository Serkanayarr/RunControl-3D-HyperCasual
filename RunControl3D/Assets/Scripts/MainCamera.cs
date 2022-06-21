using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    void Start()
    {
        // kamera ve karakter arasýndaki boþuðu tanýmladýk
        targetOffset = transform.position - target.position;
    }


    void Update()
    {
        //target offseti koruyarak kameranýn pozisyonunu karakterden süreki offsetkadar uzak hale getirdik
        //bu yüzden sürekli karakteri bei mesafeden takip ediyo
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, 0.125f);
    }
}
