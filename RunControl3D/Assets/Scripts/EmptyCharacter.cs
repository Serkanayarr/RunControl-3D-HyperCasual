using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmptyCharacter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer;
    public Material GivenMaterial;
    public NavMeshAgent _Navmesh;
    public Animator _Animator;
    public GameObject Target;
    public GameManager _GameManager;
    bool isTrigger;

    private void LateUpdate()
    {
        if (isTrigger)
        {
            _Navmesh.SetDestination(Target.transform.position);
        }    
    }

    Vector3 MainPosition()
    {
        return new Vector3(transform.position.x, 0f, transform.position.z);/*Her efekt olu�ma ko�ulunda tek tek vector3 pozisyon ayarlamak yerine 
        fonksiyona tan�ml�yoruz ve fonksiyonlardaki poziyon parametresi yerine bu fonksiyonu yaz�yoruz (_GameManager.CreateExtinctionEffect(MainPosition());
        yerine) b�ylece kod kalabal���ndan kurtulup daha okunabilir ve optimize �ekilde yaz�yoruz.*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SubCharacters")|| other.CompareTag("Character")) /* e�er bizim serseri karakterlerimiz alt karakter ya da karakterimizle
        triggerlan�rsa ve tag� hala EmptyCharacterse ChangeMaterialAndTriggerAnimation() animasyonunu ba�lat ve isTrigger� trueya set et. isTrigger
        true oldu�u zaman yapay zekalar destinaton pointi yani karakterimizi takip etmeye ba�l�y�cak */
        {
            if (gameObject.CompareTag("EmptyCharacter"))
            {
                ChangeMaterialAndTriggerAnimation();
                isTrigger = true;
                _GameManager.GameFX[1].Play();
                //GetComponent<AudioSource>().Play();
            }
            
        }

        else if (other.CompareTag("PinBox")) /* e�er karakter i�neli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
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

        else if (other.CompareTag("Enemy")) 
        {
            _GameManager.CreateExtinctionEffect(MainPosition(), false);
            gameObject.SetActive(false);
        }
    }

    void ChangeMaterialAndTriggerAnimation() /* Material arrayi olu�turduk bu sayede gerekli zamanlarda otomatik �ekilde gerekli material� verebiliriz 
    ancak �uan bizim de�i�tirice�imiz sadece bir tane material oldu�u i�in mats[0] � GivenMateriala e�itliyoruz ve rendere� materials� bir daha 
    mats a e�itliyoruz ��nk� materiali de�i�tridik bu sayede de�i�tirdi�imiz haldeki material� renderer materiala e�itledik. Daha sonra anl�k karakter 
    say�s�n� yeeni karakter ekledi�imiz i�in 1 artt�t�p serseri karakteri art�k alt karaktere �eiviriyoruz ve animasyonunu attack modune �eiviriyoruz.*/
    {
        Material[] mats = _Renderer.materials;
        mats[0] = GivenMaterial;
        _Renderer.materials = mats;
        GameManager.InstantCharCount++;
        gameObject.tag = "SubCharacters";
        _Animator.SetBool("Attack", true);
        
    }
}
