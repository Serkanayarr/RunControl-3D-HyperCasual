using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Seko
{
    public class MathematicalOperations
    {       
        public void Multipication(int incomingData, List<GameObject> Characters, Transform position, List<GameObject> CreationEffects)
        {
            int loopCount = (GameManager.InstantCharCount * incomingData) - GameManager.InstantCharCount;
            /* Diyelim ki 5 tane adam�m�z var ve biz 6 ile �arpma i�lemi yap�caz 5 * 6 = 30. �lk parantezde 30 kere loopu d�nd�r�cek ancak e�er ilk
             * ba�ta sahip olldu�umuz karakter say�s�n� total karakterden ��karmazsak 35 adam�m�z olur. Bu y�zden anl�k karakter say�s�n� ��kar�yoruz.
             * B�ylece d�ng� 25 kere d�n�yor ve totalde 30 adam�m�z oluyor.*/
            int count = 0;
            foreach (var item in Characters) // Characters listesini tarar
            {
                if (count < loopCount)// count de�eri ile kodun loop count kadar yani, ihtiyac�m�z olan say�ya ula��cak say� kadar d�nmesini sa�l�yoruz.
                {
                    if (!item.activeInHierarchy) /* E�er item aktif de�ilse itemin pozisyonunu spawn pointe ayarla ve  itemi aktif et sonra d�ng�y� kapa.
                     Burada item diye bahsedilen obje subCharacter objesidir.*/
                    {
                        foreach (var effect in CreationEffects)/* Bu for each d�ng�s� ile CreationEffects listesinde effectleri tar�yoruz, e�er effect 
                         objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu olay�n ger�ekle�ti�i yere e�itliyoruz ve particle effect sistemini 
                         �al��t�r�yoruz.*/
                        {
                            if (!effect.activeInHierarchy)
                            {
                                effect.SetActive(true);
                                effect.transform.position = position.position;
                                effect.GetComponent<ParticleSystem>().Play();
                                //GameManager.GameFX[1].Play();
                                //effect.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }

                        item.transform.position = position.position;/* �temin pozisyonunu fonksiyonun �al��t��� pozisyona e�itler, itemi aktif eder.*/
                        item.SetActive(true);
                        count++;
                    }
                }
                else
                {
                    count = 0;
                    break;
                }
                GameManager.InstantCharCount *= incomingData;
            }
        }

        public void Addition(int incomingData, List<GameObject> Characters, Transform position, List<GameObject> CreationEffects)
        {
            int count2 = 0;
            foreach (var item in Characters) // characters listesinin i�inde dola�
            {
                if (count2 < incomingData)
                {
                    if (!item.activeInHierarchy)//E�er item active de�ilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra d�ng�y� kapa
                    {
                        foreach (var effect in CreationEffects)/* Bu for each d�ng�s� ile CreationEffects listesinde effectleri tar�yoruz, e�er effect 
                         objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu olay�n ger�ekle�ti�i yere e�itliyoruz ve particle effect sistemini 
                         �al��t�r�yoruz.*/
                        {
                            if (!effect.activeInHierarchy)
                            {
                                effect.SetActive(true);
                                effect.transform.position = position.position;
                                effect.GetComponent<ParticleSystem>().Play();
                                //_GameManager.GameFX[1].Play();
                                //effect.GetComponent <AudioSource>().Play();
                                break;
                            }
                        }

                        item.transform.position = position.position;
                        item.SetActive(true);
                        count2++;
                    }
                }
                else
                {
                    count2 = 0;
                    break;
                }



            }
            GameManager.InstantCharCount += incomingData;

        }

        public void Substraction(int incomingData, List<GameObject> Characters, List<GameObject> ExtinctionEffects)
        {
            if (GameManager.InstantCharCount < incomingData)/*E�er anl�k karakter say�s� ��karma i�lemi yap�ca��m�z say�dan d���k ise eksi de�erde 
             objemiz olam�y�ca�� i�in anl�k karakter say�s�n� 1'e e�itler.*/
            {
                foreach (var item in Characters)
                {
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.InstantCharCount = 1;
            }
            else
            {
                int count3 = 0;
                foreach (var item in Characters) // characters listesinin i�inde dola�
                {
                    foreach (var effect in ExtinctionEffects)/* Bu for each d�ng�s� ile ExtinctionEffects listesinde effectleri tar�yoruz, e�er effect 
                    objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu iteme yani subCharacterin pozisyonuna e�itliyoruz ve particle effect sistemini 
                    �al��t�r�yoruz. B�ylece eksilme efekti eksilen karakterin oldu�u yerde olu�uyor.*/
                    {
                        if (!effect.activeInHierarchy)
                        {
                            effect.SetActive(true);
                            effect.transform.position = item.transform.position;
                            effect.GetComponent<ParticleSystem>().Play();
                            //_GameManager.GameFX[0].Play();
                            //effect.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }

                    if (count3 != incomingData)// count de�eri ile kodun anl�k karakter say�s� kadar d�nmesini sa�l�yoruz
                    {
                        if (item.activeInHierarchy) //e�er item active de�ilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra d�ng�y� kapa
                        {
                            foreach (var effect in ExtinctionEffects)/* Bu for each d�ng�s� ile ExtinctionEffects listesinde effectleri tar�yoruz, e�er effect 
                            objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu iteme yani subCharacterin pozisyonuna e�itliyoruz ve particle effect sistemini 
                            �al��t�r�yoruz. B�ylece eksilme efekti eksilen karakterin oldu�u yerde olu�uyor.*/
                            {
                                if (!effect.activeInHierarchy)
                                {
                                    effect.SetActive(true);
                                    effect.transform.position = item.transform.position;
                                    effect.GetComponent<ParticleSystem>().Play();
                                    //_GameManager.GameFX[0].Play();
                                    //effect.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }

                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            count3++;
                        }
                    }
                    else
                    {
                        count3 = 0;
                        break;
                    }
                }
                GameManager.InstantCharCount -= incomingData;
            }
        }

        public void Division(int incomingData, List<GameObject> Characters, List<GameObject> ExtinctionEffects)
        {

            if (GameManager.InstantCharCount <= incomingData)/*E�er anl�k karakter say�m�z o say�y� b�l�ce�imiz say�dan az ise anll�k karakter say�s�n� direkt
            1'e e�itliyoruz.*/
            {
                foreach (var item in Characters)
                {
                    foreach (var effect in ExtinctionEffects)/* Bu for each d�ng�s� ile ExtinctionEffects listesinde effectleri tar�yoruz, e�er effect 
                    objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu iteme yani subCharacterin pozisyonuna e�itliyoruz ve particle effect sistemini 
                    �al��t�r�yoruz. B�ylece eksilme efekti eksilen karakterin oldu�u yerde olu�uyor.*/
                    {
                        if (!effect.activeInHierarchy)
                        {
                            effect.SetActive(true);
                            effect.transform.position = item.transform.position;
                            effect.GetComponent<ParticleSystem>().Play();
                            //_GameManager.GameFX[0].Play();
                            //effect.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }

                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.InstantCharCount = 1;
            }
            else
            {
                int divider = GameManager.InstantCharCount / incomingData;

                int count4 = 0;
                foreach (var item in Characters) // characters listesinin i�inde dola�
                {

                    if (count4 != divider)// count de�eri ile kodun anl�k karakter say�s� kadar d�nmesini sa�l�yoruz
                    {
                        if (item.activeInHierarchy) //e�er item active de�ilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra d�ng�y� kapa
                        {
                            foreach (var effect in ExtinctionEffects)/* Bu for each d�ng�s� ile ExtinctionEffects listesinde effectleri tar�yoruz, e�er effect 
                            objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu iteme yani subCharacterin pozisyonuna e�itliyoruz ve particle effect sistemini 
                            �al��t�r�yoruz. B�ylece eksilme efekti eksilen karakterin oldu�u yerde olu�uyor.*/
                            {
                                if (!effect.activeInHierarchy)
                                {
                                    effect.SetActive(true);
                                    effect.transform.position = item.transform.position;
                                    effect.GetComponent<ParticleSystem>().Play();
                                    //_GameManager.GameFX[0].Play();
                                    //effect.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }

                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            count4++;
                        }
                    }
                    else
                    {
                        count4 = 0;
                        break;
                    }
                }
                /*Burada say�lar�n modunu al�p kalan say�s�na g�re anl�k karakter say�s�n� belirliyoruz yani 11 i 2 ye b�ld�k say� 5 oluyor ancak 5.5 6 ya
                 yuvarland��� i�in oyunda 6 karakter yarat�yoruz bu y�zden anl�k karakter say�s�n�nda 5 de�il 6 g�z�kmesi i�in say�ya 1 ekliyoruz veya 11'i
                 3'e b�ld���m�z� kalan 2 oluca�� i�in 2 ekliyoruz.*/
                if (GameManager.InstantCharCount % incomingData == 0)
                {
                    GameManager.InstantCharCount /= incomingData;
                }
                else if (GameManager.InstantCharCount % incomingData == 1)
                {
                    GameManager.InstantCharCount /= incomingData;
                    GameManager.InstantCharCount++;
                }
                else if (GameManager.InstantCharCount % incomingData == 2)
                {
                    GameManager.InstantCharCount /= incomingData;
                    GameManager.InstantCharCount += 2;
                }

            }

        }
    }
    public class MemoryManagement 
    {
        public void SaveData_string(string Key, string value) 
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
        public void SaveData_int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }
        public void SaveData_float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public string ReadData_string(string Key) 
        {
            return PlayerPrefs.GetString(Key);
        }
        public int ReadData_int(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float ReadData_float(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        public void ControlAndDefine() 
        {
            //PlayerPrefs.SetInt("LastLevel", 5);//halledicez

            if (!PlayerPrefs.HasKey("LastLevel")) 
            {
                PlayerPrefs.SetInt("LastLevel", 5);
                PlayerPrefs.SetInt("ActiveCap", -1);
                PlayerPrefs.SetInt("ActiveStick", -1);
                PlayerPrefs.SetInt("ActiveTheme", -1);
                PlayerPrefs.SetFloat("MenuMusic", 1);
                PlayerPrefs.SetFloat("MenuFX", 1);
                PlayerPrefs.SetFloat("GameMusic", 1);
                PlayerPrefs.SetFloat("GameFX", 1);
            }
        }
    }
    [Serializable]
    public class ItemDatas 
    {
        public int GroupIndex;
        public int ItemIndex;
        public string ItemName;
        public int Point;
        public bool BuyingSituation;
    }
    public class DataManagement 
    {
        public void Save(List<ItemDatas> _ItemDatas )
        {
            _ItemDatas[1].BuyingSituation = true;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemDatas.gd"); //ilk �nce file�n�n yarat�l�ca��u pathi sonra da hangi isimle yarat�l�ca��n� girdik. PS:.gd dosya uzant�s� ismi.
            bf.Serialize(file, _ItemDatas);//�tem datas listesi i�indeki infolar� file dosyas�na yazd�r�yoruz(?).
            file.Close();// en son i�imiz bitti�inde close diyerek dosyay� kapat�yoruz
        }

        List<ItemDatas> _ItemInsideList;

        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemDatas.gd")) // Dosyan�n silinmesi veya ba�ka yere ta��nmas� halinde problem olmamas� i�in �nce dosyay� check ediyoruz
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemDatas.gd", FileMode.Open);// Dosyam�z� a��yoruz
                _ItemInsideList = (List<ItemDatas>)bf.Deserialize(file);// ba��na (List<ItemDatas>) koyduk ��nk� ��z���ce�in verilerin t�r� itemdatas s�n�f�nda benim vermi� oldu�um verilerdir dedim
                file.Close();
            }
        }

        public List<ItemDatas> TransferList() 
        {
            return _ItemInsideList;
        }

        public void FirstInstallFileCreation(List<ItemDatas> _ItemDatas)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemDatas.gd"))
            {
                _ItemDatas[1].BuyingSituation = true;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemDatas.gd"); //ilk �nce file�n�n yarat�l�ca��u pathi sonra da hangi isimle yarat�l�ca��n� girdik. PS:.gd dosya uzant�s� ismi.
                bf.Serialize(file, _ItemDatas);//�tem datas listesi i�indeki infolar� file dosyas�na yazd�r�yoruz(?).
                file.Close();// en son i�imiz bitti�inde close diyerek dosyay� kapat�yoruz
            }
        }

    }
}