using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        // karakterle olu�turdu�umuz alan collid olud�unda force impulse efekti olu�turup karakteri ittir.
        if (other.CompareTag("SubCharacters")) 
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0), ForceMode.Impulse);
        }
    }
}
