using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seko;

public class GameManager : MonoBehaviour
{

    public GameObject DestinationPoint;
    public static int InstantCharCount = 1;

    public List<GameObject> Characters;
    public List<GameObject> CreationEffects;
    public List<GameObject> ExtinctionEffects;
    public List<GameObject> CrushEffects;
    void Start()
    {

    }

    void Update()
    {

    }

    public void CharacterManagement(string operationType, int incomingData, Transform position)
    {
        switch (operationType)
        {
            case "Multiplication":
                MathematicalOperations.Multipication(incomingData, Characters, position, CreationEffects);

                break;

            case "Addition":
                MathematicalOperations.Addition(incomingData, Characters, position, CreationEffects);
                break;

            case "Substraction":
                MathematicalOperations.Substraction(incomingData, Characters, ExtinctionEffects);
                break;

            case "Division":
                MathematicalOperations.Division(incomingData, Characters, ExtinctionEffects);
                break;
        }
    }

    public void CreateExtinctionEffect(Vector3 position)
    {
        foreach (var effect in ExtinctionEffects)/*Extinction effectti tarýyoruz inaktif effect varsa efektin pozisyonunu fonksiyonun çalýþtýðý colliderýn
         olduðu yere eþitliyoruz.Particle sistemini çalýþtýrýyoruz ve fonksiyon her çalýþtýðýnda anlýk karakter sayýsýný 1 azaltýyoruz.*/
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                effect.transform.position = position;
                effect.GetComponent<ParticleSystem>().Play();
                GameManager.InstantCharCount--;
                break;

            }
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
}
