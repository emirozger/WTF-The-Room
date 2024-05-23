using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour
{
    public Light lightSource;
    public AudioSource audioSource;
    public float minOnTime = 5.0f;
    public float maxOnTime = 10.0f;
    public float minOffTime = 0.1f;
    public float maxOffTime = 0.5f;

    private void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            lightSource.enabled = true;
            float onTime = Random.Range(minOnTime, maxOnTime);
            yield return new WaitForSeconds(onTime);
            lightSource.enabled = false;
            audioSource.Play();
            float offTime = Random.Range(minOffTime, maxOffTime);
            yield return new WaitForSeconds(offTime);
        }
    }
}