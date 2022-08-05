using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Seko
{
    public class OptionsManager : MonoBehaviour
    {
        public AudioSource ButtonsSound;
        public Slider[] Sounds;
        MemoryManagement _MemoryManagement = new MemoryManagement();
        DataManagement _DataManagement = new DataManagement();

        public List<LanguageDatasMainObject> _LanguageDatasMainObject = new List<LanguageDatasMainObject>();
        List<LanguageDatasMainObject> _LanguageReadDatas = new List<LanguageDatasMainObject>();
        public TextMeshProUGUI[] TextObjects;

        [Header("LANGUAGE PREF OBJECTS")]
        public TextMeshProUGUI[] DilText;
        public Text LanguagePrefText;
        public Button[] LanguageButtons;
        int ActiveLanguageIndex;
        void Start()
        {
            ButtonsSound.volume = _MemoryManagement.ReadData_float("MenuFX");
            Sounds[0].value = _MemoryManagement.ReadData_float("MenuMusic");
            Sounds[1].value = _MemoryManagement.ReadData_float("MenuFX");
            Sounds[2].value = _MemoryManagement.ReadData_float("GameMusic");
            Sounds[3].value = _MemoryManagement.ReadData_float("GameFX");

            _DataManagement.LanguageLoad();
            _LanguageReadDatas = _DataManagement.TransferLanguageList();
            _LanguageDatasMainObject.Add(_LanguageReadDatas[4]);
            LanguagePreferManagement();
            ControlLanguageSituation();

        }
        public void LanguagePreferManagement()
        {
            if (_MemoryManagement.ReadData_string("Language") == "TR")
            {
                for (int i = 0; i < TextObjects.Length; i++)
                {
                    TextObjects[i].text = _LanguageDatasMainObject[0].languageDatas_TR[i].Text;
                }
            }
            else
            {
                for (int i = 0; i < TextObjects.Length; i++)
                {
                    TextObjects[i].text = _LanguageDatasMainObject[0].languageDatas_EN[i].Text;
                }
            }
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

        public void ControlLanguageSituation()
        {
           if(_MemoryManagement.ReadData_string("Language") == "EN")
           {
               ActiveLanguageIndex = 0;
               LanguagePrefText.text = "ENGLISH";
               LanguageButtons[0].interactable = false;
           }
           else
           {
                ActiveLanguageIndex = 1;
                LanguagePrefText.text = "TÜRKCE";
                LanguageButtons[1].interactable = false;
            }
        }
        public void Changelanguage(string Way)
        {
            if(Way == "Forward")
            {
                ActiveLanguageIndex = 1;
                LanguagePrefText.text = "TÜRKCE";
                LanguageButtons[1].interactable = false;
                LanguageButtons[0].interactable = true;
                _MemoryManagement.SaveData_string("Language", "TR");
                LanguagePreferManagement();
            }
            else
            {
                ActiveLanguageIndex = 0;
                LanguagePrefText.text = "ENGLISH";
                LanguageButtons[0].interactable = false;
                LanguageButtons[1].interactable = true;
                _MemoryManagement.SaveData_string("Language", "EN");
                LanguagePreferManagement();
            }
            ButtonsSound.Play();
        }
    }
}

