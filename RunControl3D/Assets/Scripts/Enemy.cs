using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject AttackTarget;// Yapay zekalarýn saldýrýcaðý hedefi ayarladýk.
    public NavMeshAgent _NavMesh;// Ayný þekilde get componentla uðraþmamak için hieararchyden referans yöntemini kullanýyoruz.
    public Animator _Animator;// hierararchyden referans yöntemi kullanýcaz
    public GameManager _GameManager;// hierararchyden referans yöntemi kullanýcaz
    private bool isAttackStart;// Saldýrýnýn baþlayýp baþlamayýcaðýný aýrmak için bi rboolean parametresi oluþturduk
    void Start()
    {
        //_NavMesh'i kullanabilmek için getcomponentýný alýyoruz.// Referans yönetmini kullandðýmýz için bu kodu sildik.
    }
    
    //Animatörde oluþturduðumuz saldýr komutunu triggerlamasý için bir fonksiyon oluþturuyoruz.Fonksiyon çalýþtýðýnda attack parametresi true'ya set ediliyor
    public void TriggerAnimation() 
    {
        _Animator.SetBool("Attack", true);
        isAttackStart = true;// trigger animasyonu çalýþtýðý zaman attack'da baþlýyýcak bu sayede karakterler attacktarger noýkatasýna doðru harekete baþlayacak
    
    }
    // Update is called once per frame
    void Update()
    {
        if (isAttackStart) 
        {
            _NavMesh.SetDestination(AttackTarget.transform.position);//yapay zekalarýn takip ediceði pozisyonu Attack targete setledik.
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SubCharacters")) /* eðer karakter iðneli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
        ve karakteri inaktif et*/
        {
            Vector3 mainPos = new Vector3(transform.position.x, 0f, transform.position.z);
            _GameManager.CreateExtinctionEffect(mainPos,true);
            gameObject.SetActive(false);
        }
    }
}
