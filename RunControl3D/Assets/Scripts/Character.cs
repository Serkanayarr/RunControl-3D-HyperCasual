using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameManager _GameManager;

    private void FixedUpdate()
    {
        //karakteri ileri yönü koþturduk
        transform.Translate(Vector3.forward * 1f * Time.deltaTime);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // eðer mosue ekranýn solundaysa sola doðru 0.1f güçle çekilir
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.1f,
                    transform.position.y, transform.position.z), 0.3f);

            }
            // eðer mosue ekranýn saðýndaysa saða doðru 0.1f güçle çekilir
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
            int number = int.Parse(other.name);//other.name objenin ismi yani string bir deðer olduðu için geen string deðeri integera çevirdik
            _GameManager.CharacterManagement(other.tag, number, other.transform);// objenin tagýný ve objenin ismini yani asýnda yapýan iþlemdeki
                                                                                 // sayýyý verdik.
        }
    }
}
