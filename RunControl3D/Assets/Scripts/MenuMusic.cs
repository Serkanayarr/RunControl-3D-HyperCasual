using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private static GameObject instance;

    public AudioSource Sound;
    void Start()
    {
        Sound.volume = PlayerPrefs.GetFloat("MenuMusic"); //we'll comeback here
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Sound.volume = PlayerPrefs.GetFloat("MenuMusic");
    }
}
