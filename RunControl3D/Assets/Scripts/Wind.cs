using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        // karakterle oluþturduðumuz alan collid oludðunda force impulse efekti oluþturup karakteri ittir.
        if (other.CompareTag("SubCharacters")) 
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0), ForceMode.Impulse);
        }
    }
}
