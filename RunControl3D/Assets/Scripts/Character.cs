using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameManager _GameManager;
    public MainCamera _Camera;// cameranýn get componentýný almak yerine referans yöntemini kullandýk.
    public GameObject finalPosition;
    public bool finalBattle;

    private void FixedUpdate()
    {
        if (!finalBattle)
        {
            transform.Translate(Vector3.forward * 1f * Time.deltaTime);//final batle baþlamadýðý süürece karakteri ileri yöne koþturduk
        }
        
    }

    void Update()
    {
        if (finalBattle) 
        {
            transform.position = Vector3.Lerp(transform.position, finalPosition.transform.position, 0.005f);
        }
        else 
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {

                if (Input.GetAxis("Mouse X") < 0)// eðer mosue ekranýn solundaysa sola doðru 0.1f güçle çekilir
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.1f,
                        transform.position.y, transform.position.z), 0.3f);

                }

                if (Input.GetAxis("Mouse X") > 0)// eðer mosue ekranýn saðýndaysa saða doðru 0.1f güçle çekilir
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.1f,
                            transform.position.y, transform.position.z), 0.3f);
                }
            }
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Addition") || other.CompareTag("Substraction") || other.CompareTag("Multiplication") || other.CompareTag("Division"))
        {
            int number = int.Parse(other.name);//other.name objenin ismi yani string bir deðer olduðu için geen string deðeri integera çevirdik
            _GameManager.CharacterManagement(other.tag, number, other.transform);/* objenin tagýný ve objenin ismini yani aslýnda yapýlan iþlemdeki sayýyý verdik.*/
        }
        else if (other.CompareTag("FinalTrigger")) 
        {
            _Camera.finalBattle = true;
            _GameManager.triggerEnemies();
            finalBattle = true;
        }
    }
}
