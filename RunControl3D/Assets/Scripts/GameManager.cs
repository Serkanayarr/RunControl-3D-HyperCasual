using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Seko;

public class GameManager : MonoBehaviour
{

    public GameObject DestinationPoint;
    public static int InstantCharCount = 1;

    public List<GameObject> Characters;
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
                MathematicalOperations.Multipication(incomingData, Characters, position);

                break;

            case "Addition":
                MathematicalOperations.Addition(incomingData, Characters, position);
                break;

            case "Substraction":
                MathematicalOperations.Substraction(incomingData, Characters);
                break;

            case "Division":
                MathematicalOperations.Division(incomingData, Characters);
                break;
        }
    }
}
