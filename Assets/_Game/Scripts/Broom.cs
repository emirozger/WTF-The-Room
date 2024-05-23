using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Broom : MonoBehaviour
{
    //[SerializeField] private InputActionReference lookAction;
    private float holdTime = 0f;
    private bool rotating = false;
    private bool hasPlayedSound = false;
    [SerializeField] private Collider level1DustKeyCollider;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Dust"))
        {
           // Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
            
            if (Input.GetMouseButton(0))
            {
             //   if (lookInput == Vector2.zero) return;

                if (hasPlayedSound == false)
                {
                    AudioManager.Instance.PlayOneShot("Broom_Sound");
                    DOVirtual.DelayedCall(1f, () => AudioManager.Instance.PlayOneShot("Broom_Sound"));
                    DOVirtual.DelayedCall(2f, () => AudioManager.Instance.PlayOneShot("Broom_Sound"));
                    hasPlayedSound = true;
                }
                holdTime += Time.deltaTime;
              
              
                if (holdTime >= 3f && !rotating)
                {
                    rotating = false;
                    holdTime = 0f;
                    Destroy(other.gameObject);
                    if (level1DustKeyCollider != null)
                    {
                        level1DustKeyCollider.enabled = true;
                    }
                }
            }
            else
            {
                hasPlayedSound = false;
                holdTime = 0f;
            }
        }
    }
    
}