using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject DestinationPoint;
    public int InstantCharCount = 1;

    public List<GameObject> Characters;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void CharacterManagement(string data,Transform position) 
    {
        switch (data) 
        {
            case "X2":
                int count = 0;
                foreach (var item in Characters) // characters listesinin içinde dolaþ
                {
                    // count deðeri ile kodun anýk karakter sayýsý kadar dönmesini saðlýyoruz
                    if(count < InstantCharCount)
                    {
                        if (!item.activeInHierarchy) //eðer item active deðilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra döngüyü kapa
                        {
                            item.transform.position = position.position;
                            item.SetActive(true);
                            count++;
                        }
                    }
                    else
                    {
                        count = 0;
                        break;
                    }


                   
                } 
                InstantCharCount *= 2;
                break;

            case "+3":
                int count2 = 0;
                foreach (var item in Characters) // characters listesinin içinde dolaþ
                {
                    // count deðeri ile kodun anýk karakter sayýsý kadar dönmesini saðlýyoruz
                    if (count2 < 3)
                    {
                        if (!item.activeInHierarchy) //eðer item active deðilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra döngüyü kapa
                        {
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
                InstantCharCount += 3;
                break;

            case "-4":

                if (InstantCharCount < 4) 
                {
                    foreach(var item in Characters) 
                    {
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                    InstantCharCount = 1;
                }
                else 
                {
                    int count3 = 0;
                    foreach (var item in Characters) // characters listesinin içinde dolaþ
                    {
                        // count deðeri ile kodun anlýk karakter sayýsý kadar dönmesini saðlýyoruz
                        if (count3 != 4)
                        {
                            if (item.activeInHierarchy) //eðer item active deðilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra döngüyü kapa
                            {
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
                    InstantCharCount -= 4;
                }
              
                break;

            case "/2":

                if (InstantCharCount <= 2)
                {
                    foreach (var item in Characters)
                    {
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                    InstantCharCount = 1;
                }
                else
                {
                    int divider = InstantCharCount / 2;

                    int count4 = 0;
                    foreach (var item in Characters) // characters listesinin içinde dolaþ
                    {
                        // count deðeri ile kodun anlýk karakter sayýsý kadar dönmesini saðlýyoruz
                        if (count4 != divider)
                        {
                            if (item.activeInHierarchy) //eðer item active deðilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra döngüyü kapa
                            {
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
                    if(InstantCharCount % 2 == 0) 
                    {
                        InstantCharCount /= 2;
                    }
                    else
                    {
                        InstantCharCount /= 2;
                        InstantCharCount++;
                    }
                }

                break;
        }
    }
}
