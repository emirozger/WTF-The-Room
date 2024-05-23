using System;
using DG.Tweening;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCheckpoint : MonoBehaviour
{
    private Vector3 respawnPosition;
    [SerializeField] private Ease easeType = Ease.InOutBack;
    [SerializeField] private TextMeshProUGUI skipText;
    private Tween respawnTween;

    void Start()
    {
        respawnPosition = transform.position; // Oyuncunun başlangıç pozisyonu
    }

    void Update()
    {
        // Check if spacebar is pressed
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SkipRespawn();
        }
    }

    public void Respawn()
    {
        var controller = GetComponent<CharacterController>();
        var firstPersonController = GetComponent<FirstPersonController>();
        var playerInput = GetComponent<PlayerInput>();
        var col = GetComponentInChildren<CapsuleCollider>();

        controller.enabled = false;
        firstPersonController.enabled = false;
        playerInput.enabled = false;
        col.enabled = false;
        skipText.gameObject.SetActive(true);

        respawnTween = transform.DOMove(respawnPosition, 10f).SetEase(easeType)
            .OnComplete(() =>
        {
            transform.DOLocalRotate(Vector3.zero, 3f).SetEase(Ease.OutBack).SetDelay(1f)
                .OnComplete((() =>
                {
                    controller.enabled = true;
                    firstPersonController.enabled = true;
                    playerInput.enabled = true;
                    col.enabled = true;
                    skipText.gameObject.SetActive(false);
                }));
        });
    }

    private void SkipRespawn()
    {
        if (respawnTween != null && respawnTween.IsActive())
        {
            respawnTween.Complete();
            skipText.gameObject.SetActive(false);
        }
    }

    public void SetRespawnPosition(Vector3 position)
    {
        respawnPosition = position;
    }
}