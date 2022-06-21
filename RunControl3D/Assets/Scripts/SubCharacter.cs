using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SubCharacter : MonoBehaviour
{
    GameObject Target;
    NavMeshAgent _Navmesh;
    void Start()
    {// targetý destination pointe eþitledik
        _Navmesh = GetComponent<NavMeshAgent>();
        Target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestinationPoint;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //yapay zekaya destination pointe git komutunu verir
        _Navmesh.SetDestination(Target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBox")) // eðer karakter iðnei kutu engeine temas ederse karakteri inaktif et
        {
            GameManager.InstantCharCount--;
            gameObject.SetActive(false);
        }
    }
}
