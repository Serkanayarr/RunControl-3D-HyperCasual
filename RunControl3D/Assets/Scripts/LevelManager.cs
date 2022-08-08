using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Seko;

public class LevelManager : MonoBehaviour
{
    public Button[] Buttons;
    public int Level;
    public Sprite LockButton;
    public AudioSource ButtonsSound;

    public GameObject LoadingScene;
    public Slider LoadingSlider;

    MemoryManagement _MemoryManagement = new MemoryManagement();
    DataManagement _DataManagement = new DataManagement();

    public List<LanguageDatasMainObject> _LanguageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> _LanguageReadDatas = new List<LanguageDatasMainObject>();
    public TextMeshProUGUI[] TextObjects;

    void Start()
    {
        //_MemoryManagement.SaveData_string("Language", "TR");
        _DataManagement.LanguageLoad();
        _LanguageReadDatas = _DataManagement.TransferLanguageList();
        _LanguageDatasMainObject.Add(_LanguageReadDatas[2]);
        LanguagePreferManagement();

        ButtonsSound.volume = _MemoryManagement.ReadData_float("MenuFX");
        int currentLevel = _MemoryManagement.ReadData_int("LastLevel") - 4;/*Oyuncunun ka��nc� levelde kald���n� anlamak i�in last leveli
        currentLevel parametresine e�itliyoruz b�ylece en son hangi levelda olundu�unun bilgisine sahip oluyoruz ancak bizim ilk level�m�z
        5. indexte oldu�u i�in -4 yaparak do�ru say�ya eri�iyoruz.*/
        int Index = 1;
        for (int i = 0; i < Buttons.Length; i++)/* e�er i + 1 de�eri bulundu�umuz levelden k���k ise buttona o level�n de�erini yani 
                                                i + 1 i yazd�r�r�z ancak i + 1 de�eri bulundu�umuz leveli ge�ti�inde b�y�k oldu�u de�er-
                                                ler i�in kilitli resmi kullan�r*/
        {
            if(Index <= currentLevel) 
            {
                Buttons[i].GetComponentInChildren<Text>().text = (Index).ToString();//text buttonun alt objesi oldu�u i�in child compenentine eri�tik
                int sceneIndex = Index + 4; 
                Buttons[i].onClick.AddListener(delegate { LoadScene(sceneIndex); });
            }
            else 
            {
                Buttons[i].GetComponent<Image>().sprite = LockButton;//image buttonun ana componentinde var oldu�u i�in direk component dedik
                Buttons[i].enabled = false;
            }
            Index++;
        }
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
    public void LoadScene(int Index) 
    {
        ButtonsSound.Play();
        StartCoroutine(LoadAsync(Index));
    }
    IEnumerator LoadAsync(int SceneLoadIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneLoadIndex);//bana verdi�im indexteki sahnenin y�kleme oran�n� vericek
        LoadingScene.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);//clamp01 verilen float de�erlerini 0 ve 1 aras�nda tutar eksiyse 0a 1den b�y�kse 1e yuvarlar. bu �ekilde de�eri
            // daha da k���ltt���m�z i�in slider� sonuna kadar s�zm�� oluyoruz.
            LoadingSlider.value = progress;//slider�n de�erini s�rekli g�ncelliyoruz
            yield return null;
        }
    }
    public void ComeBack() 
    {
        ButtonsSound.Play();
        SceneManager.LoadScene(0);
    }
}
