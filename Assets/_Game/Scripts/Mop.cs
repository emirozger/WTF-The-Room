using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mop : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private GameObject afterCleanNumberText;
    private bool isPlayingSound = false;

    private void Start()
    {
        if (afterCleanNumberText != null)
            afterCleanNumberText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Vileda"))
        {
            timer += Time.deltaTime;
            if (!isPlayingSound)
            {
                AudioManager.Instance.Play("Mop_SFX");
                isPlayingSound = true;
            }
            
            if (timer > 2f)
            {
                Debug.Log(timer);
                if (afterCleanNumberText != null)
                {
                    afterCleanNumberText.SetActive(true);
                }
                Destroy(other.gameObject);
                timer = 0;
                StopSound();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Vileda"))
        {
            timer = 0;
            StopSound();
        }
    }

    private void StopSound()
    {
        if (isPlayingSound)
        {
            AudioManager.Instance.Stop("Mop_SFX");
            isPlayingSound = false;
        }
    }
}