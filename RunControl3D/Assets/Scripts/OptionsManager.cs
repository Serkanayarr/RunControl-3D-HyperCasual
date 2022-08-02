using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Seko
{
    public class OptionsManager : MonoBehaviour
    {
        public AudioSource ButtonsSound;
        public Slider[] Sounds;
        MemoryManagement _MemoryManagement = new MemoryManagement();
        void Start()
        {
            ButtonsSound.volume = _MemoryManagement.ReadData_float("MenuFX");
            Sounds[0].value = _MemoryManagement.ReadData_float("MenuMusic");
            Sounds[1].value = _MemoryManagement.ReadData_float("MenuFX");
            Sounds[2].value = _MemoryManagement.ReadData_float("GameMusic");
            Sounds[3].value = _MemoryManagement.ReadData_float("GameFX");
        }
        void Update()
        {

        }

        public void SetSound(string Options)
        {
            switch (Options)
            {
                case "MenuMusic":
                    Debug.Log("Menü SES:" + Sounds[0].value);
                    _MemoryManagement.SaveData_float("MenuMusic", Sounds[0].value);
                    break;
                case "MenuFX":
                    Debug.Log("Menü fx:" + Sounds[1].value);
                    _MemoryManagement.SaveData_float("MenuFX", Sounds[1].value);
                    break;
                case "GameMusic":
                    Debug.Log("GAme musix:" + Sounds[2].value);
                    _MemoryManagement.SaveData_float("GameMusic", Sounds[2].value);
                    break;
                case "GameFX":
                    Debug.Log("gamefx" + Sounds[3].value);
                    _MemoryManagement.SaveData_float("GameFX", Sounds[3].value);
                    break;
            }
        }

        public void GoBack()
        {
            ButtonsSound.Play();
            SceneManager.LoadScene(0);
        }

        public void Changelanguage()
        {
            ButtonsSound.Play();
        }
    }
}

