using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Seko;    

public class MainMenuManager : MonoBehaviour
{
    MemoryManagement _MemoryManagement = new MemoryManagement();
    DataManagement _DataManagement = new DataManagement();
    AdManagement _AdManagement = new AdManagement();
    public GameObject ExitPanel;
    public List<ItemDatas> _DefaultItemDatas = new List<ItemDatas>();
    public List<LanguageDatasMainObject> _DefaultLanguageDatas = new List<LanguageDatasMainObject>();
    public AudioSource ButtonsSound;

    public List<LanguageDatasMainObject> _LanguageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> _LanguageReadDatas = new List<LanguageDatasMainObject>();
    public TextMeshProUGUI[] TextObjects;
    public GameObject LoadingScene;
    public Slider LoadingSlider;

    void Start()
    {
        _MemoryManagement.ControlAndDefine();
        _DataManagement.FirstInstallFileCreation(_DefaultItemDatas , _DefaultLanguageDatas);//diðer tüm itemler bitince aktifleþtir
        ButtonsSound.volume = _MemoryManagement.ReadData_float("MenuFX");
        //Debug.Log(_LanguageDatasMainObject[0].languageDatas_TR[4].Text);
        //_MemoryManagement.SaveData_string("Language", "EN");

        _DataManagement.LanguageLoad();
        _LanguageReadDatas = _DataManagement.TransferLanguageList();
        _LanguageDatasMainObject.Add(_LanguageReadDatas[0]);
        LanguagePreferManagement();

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
    public void LoadScene(int ýndex) 
    {
        ButtonsSound.Play();
        SceneManager.LoadScene(ýndex);
    }
    public void Play() 
    {
        ButtonsSound.Play();
        //SceneManager.LoadScene(_MemoryManagement.ReadData_int("LastLevel"));
        StartCoroutine(LoadAsync(_MemoryManagement.ReadData_int("LastLevel")));
    }
    IEnumerator LoadAsync(int SceneLoadIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoadIndex);//bana verdiðim indexteki sahnenin yükleme oranýný vericek
        LoadingScene.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);//clamp01 verilen float deðerlerini 0 ve 1 arasýnda tutar eksiyse 0a 1den bðyðkse 1e yuvarlar. bu þekilde deðeri
            // daha da küçülttüðümüz için sliderý sonuna kadar süzmüþ oluyoruz.
            LoadingSlider.value = progress;//sliderýn deðerini sürekli güncelliyoruz
            yield return null;
        }
    }
    public void QuitButtonAnswer(string situation) 
    {
        ButtonsSound.Play();
        if(situation == "yes") 
        {
            Application.Quit();
        }
        else if(situation == "quit")
        {
            ExitPanel.SetActive(true);
        }
        else 
        {
            ExitPanel.SetActive(false);
        }

    }
}
