using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTouchKey : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform keyParent;
    [SerializeField] private Transform objectGrabPointTransform;

    private void OnEnable()
    {
        if (playerInteract == null)
        {
            playerInteract= FindObjectOfType<PlayerInteract>();
        }

       SetRigidbodyGrab();
    }

    private void SetRigidbodyGrab()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void SetRigidbodyDrop()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerInteract.inHand)
            {
                playerInteract.inHand = false;
                SetRigidbodyDrop();
                keyParent.GetChild(0).gameObject.SetActive(true); //0. index default key
                this.gameObject.SetActive(false); //first touch key
                return;
            }
        }
    }
    private void FixedUpdate()
    {
        if (this.objectGrabPointTransform != null)
        {
            if (playerInteract.inHand == false)
                return;

            Vector3 newPos = Vector3.Lerp(this.transform.position, objectGrabPointTransform.position,
                Time.fixedDeltaTime * 10);
            this.rb.MovePosition(newPos);
        }
        
    }
}
