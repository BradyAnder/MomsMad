using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    Rigidbody rb;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = item.GetComponent<Rigidbody>();

    }

    public void SpawnDroppedItem()
    {
        Vector3 playerForward = player.forward;
        Vector3 spawnPosition = player.position + playerForward * 4f;
        Instantiate(item, spawnPosition, Quaternion.identity);
    }
}
