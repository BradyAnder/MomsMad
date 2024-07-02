using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPlayer : MonoBehaviour
{
    public float stunTime;
    private List<GetStunned> otherPlayersStunScripts;

    void Start()
    {
        otherPlayersStunScripts = new List<GetStunned>();

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObject in playerObjects)
        {
            if (playerObject != this.gameObject)
            {
                GetStunned stunComponent = playerObject.GetComponent<GetStunned>();
                if (stunComponent != null)
                {
                    otherPlayersStunScripts.Add(stunComponent);
                }
            }
        }

        if (otherPlayersStunScripts.Count == 0)
        {
            Debug.LogWarning("No other players with GetStunned found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetStunned collidedStun = other.GetComponent<GetStunned>();
            if (collidedStun != null && collidedStun.canBeStunned == true)
            {
                collidedStun.canBeStunned = false;
                Debug.Log("Stun");
                collidedStun.StunTest();
                Destroy(gameObject);
            }
        }
    }
}
