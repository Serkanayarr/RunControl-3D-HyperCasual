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
        return new Vector3(transform.position.x, 0f, transform.position.z);/*Her efekt oluþma koþulunda tek tek vector3 pozisyon ayarlamak yerine 
        fonksiyona tanýmlýyoruz ve fonksiyonlardaki poziyon parametresi yerine bu fonksiyonu yazýyoruz (_GameManager.CreateExtinctionEffect(MainPosition());
        yerine) böylece kod kalabalýðýndan kurtulup daha okunabilir ve optimize þekilde yazýyoruz.*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SubCharacters")|| other.CompareTag("Character")) /* eðer bizim serseri karakterlerimiz alt karakter ya da karakterimizle
        triggerlanýrsa ve tagý hala EmptyCharacterse ChangeMaterialAndTriggerAnimation() animasyonunu baþlat ve isTriggerý trueya set et. isTrigger
        true olduðu zaman yapay zekalar destinaton pointi yani karakterimizi takip etmeye baþlýyýcak */
        {
            if (gameObject.CompareTag("EmptyCharacter"))
            {
                ChangeMaterialAndTriggerAnimation();
                isTrigger = true;
                _GameManager.GameFX[1].Play();
                //GetComponent<AudioSource>().Play();
            }
            
        }

        else if (other.CompareTag("PinBox")) /* eðer karakter iðneli kutu engeline temas ederse, CreateExtinctionEffect() fonksiyonunu aktif et
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

        else if (other.CompareTag("Enemy")) 
        {
            _GameManager.CreateExtinctionEffect(MainPosition(), false);
            gameObject.SetActive(false);
        }
    }

    void ChangeMaterialAndTriggerAnimation() /* Material arrayi oluþturduk bu sayede gerekli zamanlarda otomatik þekilde gerekli materialý verebiliriz 
    ancak þuan bizim deðiþtiriceðimiz sadece bir tane material olduðu için mats[0] ý GivenMateriala eþitliyoruz ve rendereý materialsý bir daha 
    mats a eþitliyoruz çünkü materiali deðiþtridik bu sayede deðiþtirdiðimiz haldeki materialý renderer materiala eþitledik. Daha sonra anlýk karakter 
    sayýsýný yeeni karakter eklediðimiz için 1 arttýtýp serseri karakteri artýk alt karaktere çeiviriyoruz ve animasyonunu attack modune çeiviriyoruz.*/
    {
        Material[] mats = _Renderer.materials;
        mats[0] = GivenMaterial;
        _Renderer.materials = mats;
        GameManager.InstantCharCount++;
        gameObject.tag = "SubCharacters";
        _Animator.SetBool("Attack", true);
        
    }
}
