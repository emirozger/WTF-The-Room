using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera mainCamera;
    private const float interactDistance = 2f;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask tvLayer;
    private RaycastHit hit;
    public IInteractable interactable;
    public bool inHand = false;
    public GameObject inHandObject;
    private float targetRotationY = 0f;
    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float rotationSmoothing = 5f;
    private float currentRotationY = 0f;
    private RaycastHit newHit;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Debug.Log(interactable);
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * interactDistance, Color.red);

        if (newHit.collider != null)
        {
            newHit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
        }

        if (hit.collider != null)
        {
            interactable?.GetHighlight.ToggleHighlight(false);
            UIManager.Instance.HideInteractUI();
            interactable = null;
        }

        if (inHand)
        {
            return;
        }

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out newHit,
                interactDistance, tvLayer))
        {
            Debug.Log("ray");
            if (newHit.collider != null)
            {
                Debug.Log("first null");
                var tv = newHit.collider.GetComponent<TvRemote>();
                if (tv != null)
                {
                    tv.GetHighlight.ToggleHighlight(true);
                    Debug.Log("tv");
                    if (Input.GetMouseButtonDown(0))
                    {
                        tv.OnInteract();
                    }
                }
            }
        }

        Interact();
    }

    private void Interact()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactDistance,
                interactableLayer))
        {
            Debug.Log(hit.transform.gameObject.name);
            hit.collider.TryGetComponent(out interactable);
            interactable?.GetHighlight.ToggleHighlight(true);
            UIManager.Instance.SetupInteractUI(interactable.GetInteractText);
            if (inHand == true) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (interactable != null)
                {
                    inHandObject = interactable.GetCollider.gameObject;
                    interactable.OnInteract();
                    inHand = true;
                }
            }
        }
    }
}