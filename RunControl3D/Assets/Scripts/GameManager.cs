using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Seko;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    

    public List<GameObject> Characters;
    public List<GameObject> CreationEffects;
    public List<GameObject> ExtinctionEffects;
    public List<GameObject> CrushEffects;

    [Header("LEVEL DATAS")]
    public List<GameObject> Enemies; // enemy poolumuz için enemy listesi oluþturduk.
    public int EnemiesNumber; // her level sonunda farklý sayýda düþman istediðimiz ayrý bir sayý bölmesi oluþturduk. Bu sayede istediðimiz sayýda enemyyi level sonuna ekleyebilirz
    public GameObject _MainCharacter;
    public bool GameOver;
    bool finalBattle;
    [Header("CAPS")]
    public GameObject[] Caps;
    [Header("STICKS")]
    public GameObject[] Sticks;
    [Header("MATERIALS")]
    public Material[] Materials;

    public SkinnedMeshRenderer _Renderer;
    public Material DefaultTheme;
    public GameObject[] Panels;
    public Slider[] SoundsSettings;

    public List<LanguageDatasMainObject> _LanguageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> _LanguageReadDatas = new List<LanguageDatasMainObject>();
    public TextMeshProUGUI[] TextObjects;


    MathematicalOperations _MathematicalOperations = new MathematicalOperations();
    MemoryManagement _MemoryManagement = new MemoryManagement();
    DataManagement _DataManagement = new DataManagement();

    Scene _Scene;
    public AudioSource GameMusic;
    public AudioSource[] GameFX;

    public GameObject LoadingScene;
    public Slider LoadingSlider;

    [SerializeField]
    public static int InstantCharCount = 1;

    private void Awake()
    {
        GameMusic.volume = _MemoryManagement.ReadData_float("GameMusic");
        SoundsSettings[0].value = _MemoryManagement.ReadData_float("GameMusic");
        SoundsSettings[1].value = _MemoryManagement.ReadData_float("GameFX");
        GameFX[0].volume = _MemoryManagement.ReadData_float("GameFX");
        GameFX[1].volume = _MemoryManagement.ReadData_float("GameFX");
        Destroy(GameObject.FindWithTag("MenuMusic"));
        ControlTheItems();

        _DataManagement.LanguageLoad();
        _LanguageReadDatas = _DataManagement.TransferLanguageList();
        _LanguageDatasMainObject.Add(_LanguageReadDatas[5]);
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
    void Start()
    {
        CreateEnemy();
        _Scene = SceneManager.GetActiveScene();
    }
    public void CreateEnemy() 
    {
        for(int i = 0; i < EnemiesNumber; i++) 
        {
            Enemies[i].SetActive(true);
        }    
    }
    public void triggerEnemies() 
    {
        foreach (var enemy in Enemies)
        {
            if (enemy.activeInHierarchy) 
            {
                enemy.GetComponent<Enemy>().TriggerAnimation();
            }
        }
        finalBattle = true;
        BattleSituation();
    }
    void BattleSituation()
    {
        if (finalBattle) 
        {

            if (InstantCharCount == 1)
            {
                GameOver = true;
                foreach (var enemy in Enemies)
                {
                    if (enemy.activeInHierarchy)
                    {
                        enemy.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }
                _MainCharacter.GetComponent<Animator>().SetBool("Attack", false);
                Debug.Log("you loose");
                Panels[3].SetActive(true);


            }
            else if (EnemiesNumber == 0)
            {
                GameOver = true;
                foreach (var character in Characters)
                {
                    if (character.activeInHierarchy)
                    {
                        character.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }
                _MainCharacter.GetComponent<Animator>().SetBool("Attack", false);
                _MemoryManagement.SaveData_int("Point", _MemoryManagement.ReadData_int("Point") + 600);
                if(_Scene.buildIndex == _MemoryManagement.ReadData_int("LastLevel"))
                    _MemoryManagement.SaveData_int("LastLevel", _MemoryManagement.ReadData_int("LastLevel") + 1);

                Debug.Log("you win");
                Panels[2].SetActive(true);
            }

        }
    }
    void Update()
    {

    }
    public void CharacterManagement(string operationType, int incomingData, Transform position)
    {
        switch (operationType)
        {
            case "Multiplication":
                _MathematicalOperations.Multipication(incomingData, Characters, position, CreationEffects);
                GameFX[1].Play();
                break;

            case "Addition":
                _MathematicalOperations.Addition(incomingData, Characters, position, CreationEffects);
                GameFX[1].Play();
                break;

            case "Substraction":
                _MathematicalOperations.Substraction(incomingData, Characters, ExtinctionEffects);
                GameFX[0].Play();
                break;

            case "Division":
                _MathematicalOperations.Division(incomingData, Characters, ExtinctionEffects);
                GameFX[0].Play();
                break;
        }
    }
    public void CreateExtinctionEffect(Vector3 position,bool situation = false)
    {
        foreach (var effect in ExtinctionEffects)/*Extinction effectti tarýyoruz inaktif effect varsa efektin pozisyonunu fonksiyonun çalýþtýðý colliderýn
         olduðu yere eþitliyoruz.Particle sistemini çalýþtýrýyoruz ve fonksiyon her çalýþtýðýnda anlýk karakter sayýsýný 1 azaltýyoruz.*/
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                effect.transform.position = position;
                effect.GetComponent<ParticleSystem>().Play();
                GameFX[0].Play();
                if (!situation)
                    GameManager.InstantCharCount--;
                else
                    EnemiesNumber--;
                break;

            }
        }

        if (!GameOver) 
        {
            BattleSituation();
        }
          
    }
    public void CreateCrushEffect(Vector3 position)
    {
        foreach (var effect in CrushEffects)/*Extinction effectti tarýyoruz inaktif effect varsa efektin pozisyonunu fonksiyonun çalýþtýðý colliderýn
         olduðu yere eþitliyoruz.Particle sistemini çalýþtýrýyoruz ve fonksiyon her çalýþtýðýnda anlýk karakter sayýsýný 1 azaltýyoruz.*/
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                effect.transform.position = position;
                GameManager.InstantCharCount--;
                break;

            }
        }
    }
    public void ControlTheItems()
    {
        if(_MemoryManagement.ReadData_int("ActiveCap") != -1)
            Caps[_MemoryManagement.ReadData_int("ActiveCap")].SetActive(true);
        if (_MemoryManagement.ReadData_int("ActiveStick") != -1)
            Sticks[_MemoryManagement.ReadData_int("ActiveStick")].SetActive(true);

        if(_MemoryManagement.ReadData_int("ActiveTheme") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Materials[_MemoryManagement.ReadData_int("ActiveTheme")];
            _Renderer.materials = mats;
        }
        else 
        {
            Material[] mats = _Renderer.materials;
            mats[0] = DefaultTheme;
            _Renderer.materials = mats;
        }
        
    }
    public void ButtonsAnswer(string situation)
    {
        GameFX[2].Play();
        if (situation == "Exit")
        {
            Time.timeScale = 0;
            Panels[0].SetActive(true);
        }
        if(situation == "Settings")
        {
            Time.timeScale = 0;
            Panels[1].SetActive(true);
        }

    }
    public void AreYouSure(string answer)
    {
        switch (answer) 
        {
            case "Yes":
                SceneManager.LoadScene(0);
                break;
            case "No":
                Panels[0].SetActive(false);
                Time.timeScale = 1;
                break;
            case "Repeat":
                SceneManager.LoadScene(_Scene.buildIndex);
                Time.timeScale = 1;
                break;
            case "MainMenu":
                SceneManager.LoadScene(0);
                break;
            case "NextLevel":
                StartCoroutine(LoadAsync(_Scene.buildIndex + 1));
                break;

        }
            
    }
    public void Settings(string answer)
    {
        if(answer == "BackToGame")
        {
                Panels[1].SetActive(false);
                Time.timeScale = 1;
        }
        else
        {
            SetSound(0);
            SetSound(1);
        }
    }
    public void SetSound(int index)
    {
        if(index == 0)
        {
            _MemoryManagement.SaveData_float("GameMusic", SoundsSettings[0].value);
            GameMusic.volume = SoundsSettings[0].value;
        }
        else
        {
            _MemoryManagement.SaveData_float("GameFX", SoundsSettings[1].value);
            _MemoryManagement.SaveData_float("GameFX", SoundsSettings[1].value);

            GameFX[0].volume = SoundsSettings[1].value;
            GameFX[1].volume = SoundsSettings[1].value;
        }
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
}
