using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seko
{
    public class MathematicalOperations : MonoBehaviour
    {
        public static void Multipication(int incomingData, List<GameObject> Characters, Transform position)
        {
            int loopCount = (GameManager.InstantCharCount * incomingData) - GameManager.InstantCharCount;// diyelim ki 5 tane adam�m�z var ve biz 6 ile 
            //�arpma i�lemi yap�caz 5 * 6 = 30 30 kere loopu d�nd�r�cek ilk parantezde ancak e�er ilk ba�ta sahip olldu�umuz karakter say�s�n� eksiltmezesek
            // 35 adam�m�z olur bu y�zden bir daha anl�k karakter say�s�n� ��akr�yoruz b�ylece d�ng� 25 kere d�n�yor ve totalde 30 adam�m�z oluyor.
            int count = 0;
            foreach (var item in Characters) // characters listesinin i�inde dola�
            {
                // count de�eri ile kodun loop count kadar yani ihyiac�m�z olan say�ya ula��cak say� kadar d�nmesini sa�l�yoruz
                if (count < loopCount)
                {
                    if (!item.activeInHierarchy) //e�er item active de�ilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra d�ng�y� kapa
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
            foreach (var item in Characters) // characters listesinin i�inde dola�
            {
                // count de�eri ile kodun an�k karakter say�s� kadar d�nmesini sa�l�yoruz
                if (count2 < incomingData)
                {
                    if (!item.activeInHierarchy) //e�er item active de�ilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra d�ng�y� kapa
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
                foreach (var item in Characters) // characters listesinin i�inde dola�
                {
                    // count de�eri ile kodun anl�k karakter say�s� kadar d�nmesini sa�l�yoruz
                    if (count3 != incomingData)
                    {
                        if (item.activeInHierarchy) //e�er item active de�ilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra d�ng�y� kapa
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
                foreach (var item in Characters) // characters listesinin i�inde dola�
                {
                    // count de�eri ile kodun anl�k karakter say�s� kadar d�nmesini sa�l�yoruz
                    if (count4 != divider)
                    {
                        if (item.activeInHierarchy) //e�er item active de�ilse itemin pozisyonunu spawn pointe ayarla ve aktif et sonra d�ng�y� kapa
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