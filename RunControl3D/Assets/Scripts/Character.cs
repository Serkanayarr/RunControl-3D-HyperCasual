using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameManager _GameManager;
    public MainCamera _Camera;// cameran�n get component�n� almak yerine referans y�ntemini kulland�k.
    public GameObject finalPosition;
    public bool finalBattle;

    private void FixedUpdate()
    {
        if (!finalBattle)
        {
            transform.Translate(Vector3.forward * 1f * Time.deltaTime);//final batle ba�lamad��� s��rece karakteri ileri y�ne ko�turduk
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

                if (Input.GetAxis("Mouse X") < 0)// e�er mosue ekran�n solundaysa sola do�ru 0.1f g��le �ekilir
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.1f,
                        transform.position.y, transform.position.z), 0.3f);

                }

                if (Input.GetAxis("Mouse X") > 0)// e�er mosue ekran�n sa��ndaysa sa�a do�ru 0.1f g��le �ekilir
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
            int number = int.Parse(other.name);//other.name objenin ismi yani string bir de�er oldu�u i�in geen string de�eri integera �evirdik
            _GameManager.CharacterManagement(other.tag, number, other.transform);/* objenin tag�n� ve objenin ismini yani asl�nda yap�lan i�lemdeki say�y� verdik.*/
        }
        else if (other.CompareTag("FinalTrigger")) 
        {
            _Camera.finalBattle = true;
            _GameManager.triggerEnemies();
            finalBattle = true;
        }
    }
}
