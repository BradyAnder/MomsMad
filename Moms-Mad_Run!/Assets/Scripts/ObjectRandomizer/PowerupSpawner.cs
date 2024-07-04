using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerupObjects;
    public GameObject[] powerupSpawnPoint;
    public float spawnInterval = 5;
    private float currCD = 0;
    public bool isPowerupInField = false;
    private int lastObjIndex = -1;
    private int lastPositionIndex = -1;
    private void Start()
    {
        if (powerupObjects == null || powerupObjects.Length == 0 || powerupSpawnPoint == null || powerupSpawnPoint.Length == 0) {
            this.enabled = false;
        }
    }

    private void Update()
    {
        currCD += Time.deltaTime;
        if (currCD >= spawnInterval)
        {
            if (!isPowerupInField) {
                SpawnPowerup();
                isPowerupInField = true;
            }
            currCD = 0;
        }
    }

    private void SpawnPowerup() {
        int objIndex;
        do
        {
            objIndex = Random.Range(0, powerupObjects.Length);
        } while (objIndex == lastObjIndex);
        lastObjIndex = objIndex;
        int positionIndex;
        do
        {
            positionIndex = Random.Range(0, powerupSpawnPoint.Length);
        } while (positionIndex == lastPositionIndex);
        lastPositionIndex = positionIndex;
        Instantiate(powerupObjects[objIndex], powerupSpawnPoint[positionIndex].transform.position, new Quaternion());
    }
}

