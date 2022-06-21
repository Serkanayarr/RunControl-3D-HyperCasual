using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seko
{
    public class MathematicalOperations : MonoBehaviour
    {
        public static void Multipication(int incomingData, List<GameObject> Characters, Transform position)
        {
            int loopCount = (GameManager.InstantCharCount * incomingData) - GameManager.InstantCharCount;// diyelim ki 5 tane adamýmýz var ve biz 6 ile 
            //çarpma iþlemi yapýcaz 5 * 6 = 30 30 kere loopu döndürücek ilk parantezde ancak eðer ilk baþta sahip ollduðumuz karakter sayýsýný eksiltmezesek
            // 35 adamýmýz olur bu yüzden bir daha anlýk karakter sayýsýný çýakrýyoruz böylece döngü 25 kere dönüyor ve totalde 30 adamýmýz oluyor.
            int count = 0;
            foreach (var item in Characters) // characters listesinin içinde dolaþ
            {
                // count deðeri ile kodun loop count kadar yani ihyiacýmýz olan sayýya ulaþýcak sayý kadar dönmesini saðlýyoruz
                if (count < loopCount)
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
                GameManager.InstantCharCount *= incomingData;
            }
        }

        public static void Addition(int incomingData, List<GameObject> Characters, Transform position)
        {
            int count2 = 0;
            foreach (var item in Characters) // characters listesinin içinde dolaþ
            {
                // count deðeri ile kodun anýk karakter sayýsý kadar dönmesini saðlýyoruz
                if (count2 < incomingData)
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
            GameManager.InstantCharCount += incomingData;

        }

        public static void Substraction(int incomingData, List<GameObject> Characters)
        {
            if (GameManager.InstantCharCount < incomingData)
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
                    // count deðeri ile kodun anlýk karakter sayýsý kadar dönmesini saðlýyoruz
                    if (count3 != incomingData)
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
                GameManager.InstantCharCount -= incomingData;
            }
        }

        public static void Division(int incomingData, List<GameObject> Characters)
        {

            if (GameManager.InstantCharCount <= incomingData)
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
                int divider = GameManager.InstantCharCount / incomingData;

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
}