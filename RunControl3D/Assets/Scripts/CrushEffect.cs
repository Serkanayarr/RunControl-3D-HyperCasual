using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushEffect : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
