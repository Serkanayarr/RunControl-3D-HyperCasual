using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SubCharacter : MonoBehaviour
{
    GameObject Target;
    NavMeshAgent _Navmesh;
    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();// _navmeshi kullanabilmek i�in nevmash component�na e�itliyoruz.
        Target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestinationPoint;/*Targeti destination pointe yani ana karakterin
        tam arkas�na e�itledik b�ylece olu�an yapay zekalar�n takip etti�i target ana karakterimiz olucak.*/
    }
    void LateUpdate()
    {
        _Navmesh.SetDestination(Target.transform.position);//yapay zekaya destination pointe git komutunu verir
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBox")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            Vector3 mainPos = new Vector3(transform.position.x, 0f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateExtinctionEffect(mainPos);
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Saw")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            Vector3 mainPos = new Vector3(transform.position.x, 0f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateExtinctionEffect(mainPos);
            gameObject.SetActive(false);
        }

        if (other.CompareTag("PropellersPin")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            Vector3 mainPos = new Vector3(transform.position.x, 0f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateExtinctionEffect(mainPos);
            gameObject.SetActive(false);
        }

        if (other.CompareTag("Sledge")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            Vector3 mainPos = new Vector3(transform.position.x, 0.005f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateCrushEffect(mainPos);
            gameObject.SetActive(false);
        }
    }
}
