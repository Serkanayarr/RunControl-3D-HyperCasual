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
            /* Diyelim ki 5 tane adamýmýz var ve biz 6 ile çarpma iþlemi yapýcaz 5 * 6 = 30. Ýlk parantezde 30 kere loopu döndürücek ancak eðer ilk
             * baþta sahip ollduðumuz karakter sayýsýný total karakterden çýkarmazsak 35 adamýmýz olur. Bu yüzden anlýk karakter sayýsýný çýkarýyoruz.
             * Böylece döngü 25 kere dönüyor ve totalde 30 adamýmýz oluyor.*/
            int count = 0;
            foreach (var item in Characters) // Characters listesini tarar
            {
                if (count < loopCount)// count deðeri ile kodun loop count kadar yani, ihtiyacýmýz olan sayýya ulaþýcak sayý kadar dönmesini saðlýyoruz.
                {
                    if (!item.activeInHierarchy) /* Eðer item aktif deðilse itemin pozisyonunu spawn pointe ayarla ve  itemi aktif et sonra döngüyü kapa.
                     Burada item diye bahsedilen obje subCharacter objesidir.*/
                    {
                        foreach (var effect in CreationEffects)/* Bu for each döngüsü ile CreationEffects listesinde effectleri tarýyoruz, eðer effect 
                         objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu olayýn gerçekleþtiði yere eþitliyoruz ve particle effect sistemini 
                         çalýþtýrýyoruz.*/
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

                        item.transform.position = position.position;/* Ýtemin pozisyonunu fonksiyonun çalýþtýðý pozisyona eþitler, itemi aktif eder.*/
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
            foreach (var item in Characters) // characters listesinin içinde dolaþ
            {
                if (count2 < incomingData)
                {
                    if (!item.activeInHierarchy)//Eðer item active deðilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra döngüyü kapa
                    {
                        foreach (var effect in CreationEffects)/* Bu for each döngüsü ile CreationEffects listesinde effectleri tarýyoruz, eðer effect 
                         objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu olayýn gerçekleþtiði yere eþitliyoruz ve particle effect sistemini 
                         çalýþtýrýyoruz.*/
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
            if (GameManager.InstantCharCount < incomingData)/*Eðer anlýk karakter sayýsý çýkarma iþlemi yapýcaðýmýz sayýdan düþük ise eksi deðerde 
             objemiz olamýyýcaðý için anlýk karakter sayýsýný 1'e eþitler.*/
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
                foreach (var item in Characters) // characters listesinin içinde dolaþ
                {
                    foreach (var effect in ExtinctionEffects)/* Bu for each döngüsü ile ExtinctionEffects listesinde effectleri tarýyoruz, eðer effect 
                    objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu iteme yani subCharacterin pozisyonuna eþitliyoruz ve particle effect sistemini 
                    çalýþtýrýyoruz. Böylece eksilme efekti eksilen karakterin olduðu yerde oluþuyor.*/
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

                    if (count3 != incomingData)// count deðeri ile kodun anlýk karakter sayýsý kadar dönmesini saðlýyoruz
                    {
                        if (item.activeInHierarchy) //eðer item active deðilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra döngüyü kapa
                        {
                            foreach (var effect in ExtinctionEffects)/* Bu for each döngüsü ile ExtinctionEffects listesinde effectleri tarýyoruz, eðer effect 
                            objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu iteme yani subCharacterin pozisyonuna eþitliyoruz ve particle effect sistemini 
                            çalýþtýrýyoruz. Böylece eksilme efekti eksilen karakterin olduðu yerde oluþuyor.*/
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

            if (GameManager.InstantCharCount <= incomingData)/*Eðer anlýk karakter sayýmýz o sayýyý bölüceðimiz sayýdan az ise anllýk karakter sayýsýný direkt
            1'e eþitliyoruz.*/
            {
                foreach (var item in Characters)
                {
                    foreach (var effect in ExtinctionEffects)/* Bu for each döngüsü ile ExtinctionEffects listesinde effectleri tarýyoruz, eðer effect 
                    objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu iteme yani subCharacterin pozisyonuna eþitliyoruz ve particle effect sistemini 
                    çalýþtýrýyoruz. Böylece eksilme efekti eksilen karakterin olduðu yerde oluþuyor.*/
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
                foreach (var item in Characters) // characters listesinin içinde dolaþ
                {

                    if (count4 != divider)// count deðeri ile kodun anlýk karakter sayýsý kadar dönmesini saðlýyoruz
                    {
                        if (item.activeInHierarchy) //eðer item active deðilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra döngüyü kapa
                        {
                            foreach (var effect in ExtinctionEffects)/* Bu for each döngüsü ile ExtinctionEffects listesinde effectleri tarýyoruz, eðer effect 
                            objesi inaktifse objeyi aktif ediyoruz. Pozisyonunu iteme yani subCharacterin pozisyonuna eþitliyoruz ve particle effect sistemini 
                            çalýþtýrýyoruz. Böylece eksilme efekti eksilen karakterin olduðu yerde oluþuyor.*/
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
                /*Burada sayýlarýn modunu alýp kalan sayýsýna göre anlýk karakter sayýsýný belirliyoruz yani 11 i 2 ye böldük sayý 5 oluyor ancak 5.5 6 ya
                 yuvarlandýðý için oyunda 6 karakter yaratýyoruz bu yüzden anlýk karakter sayýsýnýnda 5 deðil 6 gözükmesi için sayýya 1 ekliyoruz veya 11'i
                 3'e böldüðümüzü kalan 2 olucaðý için 2 ekliyoruz.*/
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
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemDatas.gd"); //ilk önce fileýnýn yaratýlýcaðýu pathi sonra da hangi isimle yaratýlýcaðýný girdik. PS:.gd dosya uzantýsý ismi.
            bf.Serialize(file, _ItemDatas);//ýtem datas listesi içindeki infolarý file dosyasýna yazdýrýyoruz(?).
            file.Close();// en son iþimiz bittiðinde close diyerek dosyayý kapatýyoruz
        }

        List<ItemDatas> _ItemInsideList;

        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemDatas.gd")) // Dosyanýn silinmesi veya baþka yere taþýnmasý halinde problem olmamasý için önce dosyayý check ediyoruz
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemDatas.gd", FileMode.Open);// Dosyamýzý açýyoruz
                _ItemInsideList = (List<ItemDatas>)bf.Deserialize(file);// baþýna (List<ItemDatas>) koyduk çünkü çözüðüceðin verilerin türü itemdatas sýnýfýnda benim vermiþ olduðum verilerdir dedim
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
                FileStream file = File.Create(Application.persistentDataPath + "/ItemDatas.gd"); //ilk önce fileýnýn yaratýlýcaðýu pathi sonra da hangi isimle yaratýlýcaðýný girdik. PS:.gd dosya uzantýsý ismi.
                bf.Serialize(file, _ItemDatas);//ýtem datas listesi içindeki infolarý file dosyasýna yazdýrýyoruz(?).
                file.Close();// en son iþimiz bittiðinde close diyerek dosyayý kapatýyoruz
            }
        }

    }
}