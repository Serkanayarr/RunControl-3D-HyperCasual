using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SubCharacter : MonoBehaviour
{
    public GameObject Target;
    NavMeshAgent _Navmesh;
    public GameManager _GameManager;//objeyi taginden aramak yerine hierarchyden referean veriyoruz oyun bi týk geç yükleniyo ama daha hýzlý çalýþýyo denebilir
    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();// _navmeshi kullanabilmek için nevmash componentýna eþitliyoruz.
        /*Targeti destination pointe yani ana karakterin
        tam arkasýna eþitledik böylece oluþan yapay zekalarýn takip ettiði target ana karakterimiz olucak.// targeti uzun uzun yazmak yerine referans yöntemini
        kullanýyoruz tüm alt karakterlere destination pointi hieararchyden atýyoruz.*/
        /*Her seferinde Gamemanager scriptine ulaþmak için
        * uzun uzun yazmak yerine _Gamemanagera eþitledik // daha da hýzlandýrmak için onu da silip referans yöntemini kullandýk */
    }
    void LateUpdate()
    {
        _Navmesh.SetDestination(Target.transform.position);//yapay zekaya destination pointe git komutunu verir
    }

    Vector3 MainPosition()
    {
        return new Vector3(transform.position.x, 0f, transform.position.z);/*Her efekt oluþma koþulunda tek tek vector3 pozisyon ayarlamak yerine 
        fonksiyona tanýmlýyoruz ve fonksiyonlardaki poziyon parametresi yerine bu fonksiyonu yazýyoruz (_GameManager.CreateExtinctionEffect(MainPosition());
        yerine) böylece kod kalabalýðýndan kurtulup daha okunabilir ve optimize þekilde yazýyoruz.*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBox")) /* eðer karakter iðneli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateExtinctionEffect(MainPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Saw")) /* eðer karakter iðneli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateExtinctionEffect(MainPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("PropellersPin")) /* eðer karakter iðneli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateExtinctionEffect(MainPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Sledge")) /* eðer karakter iðneli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateCrushEffect(MainPosition());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Enemy")) /* eðer karakter iðneli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            _GameManager.CreateExtinctionEffect(MainPosition(), false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("EmptyCharacter"))
        {
            _GameManager.Characters.Add(other.gameObject);// eðer serseri karakterle çarpýþýrsa serseri karakteri akarkterler listesine ekliyoruz.
        }
    }
}
