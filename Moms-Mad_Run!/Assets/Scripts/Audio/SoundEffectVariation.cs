using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectVariation : MonoBehaviour
{
    public AudioClip[] clipArray;
    public AudioSource effectSource;
    private int clipIndex;

    // Start is called before the first frame update
    void Start()
    {
        PlayRandom();
    }

    void PlayRandom()
    {
        clipIndex = Random.Range(0, clipArray.Length);
        effectSource.PlayOneShot(clipArray[clipIndex]);
    }
}
