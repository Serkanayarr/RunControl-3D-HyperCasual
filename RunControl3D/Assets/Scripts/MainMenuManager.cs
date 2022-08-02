using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Seko;    

public class MainMenuManager : MonoBehaviour
{
    MemoryManagement _MemoryManagement = new MemoryManagement();
    DataManagement _DataManagement = new DataManagement();
    public GameObject ExitPanel;
    public List<ItemDatas> _ItemDatas = new List<ItemDatas>();
    public AudioSource ButtonsSound;


    void Start()
    {
        _MemoryManagement.ControlAndDefine();
        _DataManagement.FirstInstallFileCreation(_ItemDatas);//diðer tüm itemler bitince aktifleþtir
        ButtonsSound.volume = _MemoryManagement.ReadData_float("MenuFX");
    }

    public void LoadScene(int ýndex) 
    {
        ButtonsSound.Play();
        SceneManager.LoadScene(ýndex);
    }

    public void Play() 
    {
        ButtonsSound.Play();
        SceneManager.LoadScene(_MemoryManagement.ReadData_int("LastLevel"));
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
