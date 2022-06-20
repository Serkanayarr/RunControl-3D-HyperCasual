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
            if(Input.GetAxis("Mouse X") < 0) 
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
        if(other.name == "X2" || other.name == "+3" || other.name == "-4" || other.name == "/2") 
        {
            _GameManager.CharacterManagement(other.name,other.transform);// character management fonksiyonuna gerekli parametreleri verdik.
        }
    }
}
