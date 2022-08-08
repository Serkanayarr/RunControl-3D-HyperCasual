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
        int currentLevel = _MemoryManagement.ReadData_int("LastLevel") - 4;/*Oyuncunun kaçýncý levelde kaldýðýný anlamak için last leveli
        currentLevel parametresine eþitliyoruz böylece en son hangi levelda olunduðunun bilgisine sahip oluyoruz ancak bizim ilk levelýmýz
        5. indexte olduðu için -4 yaparak doðru sayýya eriþiyoruz.*/
        int Index = 1;
        for (int i = 0; i < Buttons.Length; i++)/* eðer i + 1 deðeri bulunduðumuz levelden küçük ise buttona o levelýn deðerini yani 
                                                i + 1 i yazdýrýrýz ancak i + 1 deðeri bulunduðumuz leveli geçtiðinde büyük olduðu deðer-
                                                ler için kilitli resmi kullanýr*/
        {
            if(Index <= currentLevel) 
            {
                Buttons[i].GetComponentInChildren<Text>().text = (Index).ToString();//text buttonun alt objesi olduðu için child compenentine eriþtik
                int sceneIndex = Index + 4; 
                Buttons[i].onClick.AddListener(delegate { LoadScene(sceneIndex); });
            }
            else 
            {
                Buttons[i].GetComponent<Image>().sprite = LockButton;//image buttonun ana componentinde var olduðu için direk component dedik
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
    public void ComeBack() 
    {
        ButtonsSound.Play();
        SceneManager.LoadScene(0);
    }
}
