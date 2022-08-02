using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SubCharacter : MonoBehaviour
{
    public GameObject Target;
    NavMeshAgent _Navmesh;
    public GameManager _GameManager;//objeyi taginden aramak yerine hierarchyden referean veriyoruz oyun bi t�k ge� y�kleniyo ama daha h�zl� �al���yo denebilir
    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();// _navmeshi kullanabilmek i�in nevmash component�na e�itliyoruz.
        /*Targeti destination pointe yani ana karakterin
        tam arkas�na e�itledik b�ylece olu�an yapay zekalar�n takip etti�i target ana karakterimiz olucak.// targeti uzun uzun yazmak yerine referans y�ntemini
        kullan�yoruz t�m alt karakterlere destination pointi hieararchyden at�yoruz.*/
        /*Her seferinde Gamemanager scriptine ula�mak i�in
        * uzun uzun yazmak yerine _Gamemanagera e�itledik // daha da h�zland�rmak i�in onu da silip referans y�ntemini kulland�k */
    }
    void LateUpdate()
    {
        _Navmesh.SetDestination(Target.transform.position);//yapay zekaya destination pointe git komutunu verir
    }

    Vector3 MainPosition()
    {
        return new Vector3(transform.position.x, 0f, transform.position.z);/*Her efekt olu�ma ko�ulunda tek tek vector3 pozisyon ayarlamak yerine 
        fonksiyona tan�ml�yoruz ve fonksiyonlardaki poziyon parametresi yerine bu fonksiyonu yaz�yoruz (_GameManager.CreateExtinctionEffect(MainPosition());
        yerine) b�ylece kod kalabal���ndan kurtulup daha okunabilir ve optimize �ekilde yaz�yoruz.*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBox")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateExtinctionEffect(MainPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Saw")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateExtinctionEffect(MainPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("PropellersPin")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateExtinctionEffect(MainPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Sledge")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateCrushEffect(MainPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Enemy")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateExtinctionEffect(MainPosition(), false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("EmptyCharacter"))
        {
            _GameManager.Characters.Add(other.gameObject);// e�er serseri karakterle �arp���rsa serseri karakteri akarkterler listesine ekliyoruz.
        }
    }
}
