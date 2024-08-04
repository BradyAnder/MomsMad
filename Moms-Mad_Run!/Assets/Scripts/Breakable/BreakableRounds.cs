using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableRounds : MonoBehaviour
{
    public GameObject[] roundOne;
    public GameObject[] roundTwo;
    public GameObject[] roundThree;
    public GameObject[] roundFour;
    public GameObject[] roundFive;

    private List<GameObject[]> rounds;
    private GameObject[] currentRound;

    public BreakableObject breakableObject;

    // Start is called before the first frame update
    void Start()
    {
        rounds = new List<GameObject[]>
        {
            roundOne,
            roundTwo,
            roundThree,
            roundFour,
            roundFive
        };

        selectRandomRound();
    }

    private void Update()
    {
        if(currentRound != null && IsRoundEmpty(currentRound))
        {
            selectRandomRound();
        }    
    }

    private void selectRandomRound()
    {
        if (rounds.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, rounds.Count);
        currentRound = rounds[randomIndex];
        EnableRound(currentRound);
        rounds.RemoveAt(randomIndex);

    }

    private bool IsRoundEmpty(GameObject[] round)
    {
        foreach (GameObject obj in round)
        {
            if (obj != null)
            {
                return false;
            }
        }
        return true;
    }

    private void EnableRound(GameObject[] round)
    {
        foreach (GameObject obj in round)
        {
            BreakableObject breakable = obj.GetComponent<BreakableObject>();
            breakable.isBreakable = true;
            
        }   
    }
}
