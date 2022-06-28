using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seko;

public class GameManager : MonoBehaviour
{

    

    public List<GameObject> Characters;
    public List<GameObject> CreationEffects;
    public List<GameObject> ExtinctionEffects;
    public List<GameObject> CrushEffects;

    [Header("LEVEL DATAS")]
    public List<GameObject> Enemies; // enemy poolumuz i�in enemy listesi olu�turduk.
    public int EnemiesNumber; // her level sonunda farkl� say�da d��man istedi�imiz ayr� bir say� b�lmesi olu�turduk. Bu sayede istedi�imiz say�da enemyyi level sonuna ekleyebilirz
    public GameObject _MainCharacter;
    public bool GameOver;
    [SerializeField]
    public static int InstantCharCount = 1;

    void Start()
    {
        CreateEnemy();
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
    }

    void BattleSituation()
    {
        if(InstantCharCount == 1) 
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
            
            
        }
        else if(EnemiesNumber == 0) 
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
            Debug.Log("you win");
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

    public void CreateExtinctionEffect(Vector3 position,bool situation = false)
    {
        foreach (var effect in ExtinctionEffects)/*Extinction effectti tar�yoruz inaktif effect varsa efektin pozisyonunu fonksiyonun �al��t��� collider�n
         oldu�u yere e�itliyoruz.Particle sistemini �al��t�r�yoruz ve fonksiyon her �al��t���nda anl�k karakter say�s�n� 1 azalt�yoruz.*/
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                effect.transform.position = position;
                effect.GetComponent<ParticleSystem>().Play();
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
        foreach (var effect in CrushEffects)/*Extinction effectti tar�yoruz inaktif effect varsa efektin pozisyonunu fonksiyonun �al��t��� collider�n
         oldu�u yere e�itliyoruz.Particle sistemini �al��t�r�yoruz ve fonksiyon her �al��t���nda anl�k karakter say�s�n� 1 azalt�yoruz.*/
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
