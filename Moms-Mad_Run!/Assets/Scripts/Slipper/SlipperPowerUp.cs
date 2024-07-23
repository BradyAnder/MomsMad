using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpMode { SMG = 0, Bouncy = 1, FanShape = 2}

public class SlipperPowerUp : MonoBehaviour
{
    public PowerUpMode powerUpMode;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Mom")) { return; }
        SlipperThrow slipperThrow = other.GetComponent<SlipperThrow>();
        if (slipperThrow == null) { Debug.Log("SlipperPowerUp->OnTriggerEnter:Mom missing slipper throw component."); return; }
        switch (powerUpMode) {
            case (PowerUpMode.SMG):
                slipperThrow.powerUpTime = 4f;
                break;
            case (PowerUpMode.Bouncy):
                slipperThrow.powerUpTime = 6f;
                break;
            case (PowerUpMode.FanShape):
                slipperThrow.powerUpTime = 6f;
                break;
        }
        slipperThrow.GetPowerUp(powerUpMode);
        PowerupSpawner spawner = FindObjectOfType<PowerupSpawner>();
        spawner.isPowerupInField = false;
        Destroy(gameObject);
    }
}
