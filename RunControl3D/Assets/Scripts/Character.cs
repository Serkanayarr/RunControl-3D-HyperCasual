using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameManager _GameManager;

    private void FixedUpdate()
    {
        //karakteri ileri y�n� ko�turduk
        transform.Translate(Vector3.forward * 1f * Time.deltaTime);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // e�er mosue ekran�n solundaysa sola do�ru 0.1f g��le �ekilir
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.1f,
                    transform.position.y, transform.position.z), 0.3f);

            }
            // e�er mosue ekran�n sa��ndaysa sa�a do�ru 0.1f g��le �ekilir
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.1f,
                        transform.position.y, transform.position.z), 0.3f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Addition") || other.CompareTag("Substraction") || other.CompareTag("Multiplication") || other.CompareTag("Division"))
        {
            int number = int.Parse(other.name);//other.name objenin ismi yani string bir de�er oldu�u i�in geen string de�eri integera �evirdik
            _GameManager.CharacterManagement(other.tag, number, other.transform);// objenin tag�n� ve objenin ismini yani as�nda yap�an i�lemdeki
                                                                                 // say�y� verdik.
        }
    }
}
