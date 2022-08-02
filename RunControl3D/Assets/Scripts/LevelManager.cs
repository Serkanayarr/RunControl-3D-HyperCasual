using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Seko;

public class LevelManager : MonoBehaviour
{
    public Button[] Buttons;
    public int Level;
    public Sprite LockButton;
    public AudioSource ButtonsSound;

    MemoryManagement _MemoryManagement = new MemoryManagement();
    void Start()
    {
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
    
    public void LoadScene(int Index) 
    {
        ButtonsSound.Play();
        SceneManager.LoadScene(Index);
    }
    
    public void ComeBack() 
    {
        ButtonsSound.Play();
        SceneManager.LoadScene(0);
    }
}
