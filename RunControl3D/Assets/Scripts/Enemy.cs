using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject AttackTarget;// Yapay zekalar�n sald�r�ca�� hedefi ayarlad�k.
    public NavMeshAgent _NavMesh;// Ayn� �ekilde get componentla u�ra�mamak i�in hieararchyden referans y�ntemini kullan�yoruz.
    public Animator _Animator;// hierararchyden referans y�ntemi kullan�caz
    public GameManager _GameManager;// hierararchyden referans y�ntemi kullan�caz
    private bool isAttackStart;// Sald�r�n�n ba�lay�p ba�lamay�ca��n� a�rmak i�in bi rboolean parametresi olu�turduk
    void Start()
    {
        //_NavMesh'i kullanabilmek i�in getcomponent�n� al�yoruz.// Referans y�netmini kulland��m�z i�in bu kodu sildik.
    }
    
    //Animat�rde olu�turdu�umuz sald�r komutunu triggerlamas� i�in bir fonksiyon olu�turuyoruz.Fonksiyon �al��t���nda attack parametresi true'ya set ediliyor
    public void TriggerAnimation() 
    {
        _Animator.SetBool("Attack", true);
        isAttackStart = true;// trigger animasyonu �al��t��� zaman attack'da ba�l�y�cak bu sayede karakterler attacktarger no�katas�na do�ru harekete ba�layacak
    
    }
    // Update is called once per frame
    void Update()
    {
        if (isAttackStart) 
        {
            _NavMesh.SetDestination(AttackTarget.transform.position);//yapay zekalar�n takip edice�i pozisyonu Attack targete setledik.
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SubCharacters")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            Vector3 mainPos = new Vector3(transform.position.x, 0f, transform.position.z);
            _GameManager.CreateExtinctionEffect(mainPos,true);
            gameObject.SetActive(false);
        }
    }
}
