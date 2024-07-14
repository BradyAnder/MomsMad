using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public float DestroyTime = 3f;
    public Vector3 Offset = new Vector3(0, 1, 0);
    public Vector3 RandomizeIntensity = new Vector3(1f, 0, 0);
    private Transform transformCam;

    // Start is called before the first frame update
    void Start()
    {
        transformCam = GameObject.FindObjectOfType<Camera>().GetComponent<Transform>();
        Destroy(gameObject, DestroyTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
    }

    private void Update()
    {
        transform.LookAt(transformCam);
        transform.Rotate(0, 180, 0);
    }
}
