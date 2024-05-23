using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractablesParent : MonoBehaviour, IInteractable
{
    public string GetInteractText => interactHitInfo;
    public Highlight GetHighlight => highlight;
    public Collider GetCollider => collider;
   // public Image GetImage => InteractImage;

   [SerializeField] private string interactHitInfo = "First Touch";
   [SerializeField] private string newInfo = "Touched";
    [SerializeField] private Transform objectPickupTransformParent;
    [SerializeField] private PlayerInteract player;

    [SerializeField] private Collider defaultObjCollider;
    [SerializeField] private Collider firstTouchObjCollider;
    //[SerializeField] private Image InteractImage;
    private Collider collider;
    private Highlight highlight;
    private Rigidbody rb;

    private GameObject defaultObj;
    private GameObject firstTouchObj;
    private bool wasPicked = false;
    private float targetRotationY = 0f;
    private float currentRotationY = 0f;
    private float scrollSpeed = 10f;
    

    [SerializeField] private float rotationSmoothing = 5f;


    private void Awake()
    {
        collider = GetComponent<Collider>();
        highlight = transform.GetComponent<Highlight>();
        rb= GetComponent<Rigidbody>();

        defaultObj = transform.GetChild(0).gameObject;
        firstTouchObj = transform.GetChild(1).gameObject;

        defaultObjCollider = defaultObj.GetComponent<Collider>();
        firstTouchObjCollider = firstTouchObj.GetComponent<Collider>();
    }

    private void Start()
    {
        interactHitInfo = "First Touch";
        newInfo = "Touched";
    }

    private void Pickup()
    {
        AudioManager.Instance.PlayOneShot("PickUp");
        this.gameObject.layer = 0;
        ////ilk bunu kaldirdim  this.transform.parent = objectPickupTransformParent;
        defaultObjCollider.isTrigger= true;
        firstTouchObjCollider.isTrigger = true;
        // rb.useGravity = false;

        if (!wasPicked)
        {
            defaultObj.SetActive(false);
            firstTouchObj.SetActive(true);
            //rb = firstTouchKey.GetComponent<Rigidbody>();
            firstTouchObjCollider.enabled = true;
            wasPicked = true;
        }
        else
        {
            defaultObj.SetActive(true);
            defaultObjCollider.enabled = true;
            firstTouchObj.SetActive(false);
           // rb = defaultKey.GetComponent<Rigidbody>();
        }
        
        rb.useGravity = false;
        rb.isKinematic= true;
        collider.enabled = false;
    }


    public void OnInteract()
    {
        Pickup();
    }
    

    public void Drop()
    {
        //drop
        Debug.Log("???");
        //this.transform.parent = null;
        defaultObjCollider.isTrigger = false;
        firstTouchObjCollider.isTrigger = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        collider.enabled = true;
        //rb.AddForce(Vector3.down * 2, ForceMode.Impulse);
        // rb = null;
        player.inHand = false;
        player.inHandObject = null;
        player.interactable = null;
        defaultObj.SetActive(true);
        firstTouchObj.SetActive(false);
        Invoke(nameof(ChangeLayer), 1f);
        interactHitInfo = newInfo;
    }
    private void Update()
    {
        if (player.inHand == false) return;
        if (player.inHandObject != this.gameObject) return;

        if (Input.GetMouseButtonDown(1))
        {
            Drop();
        }

        // Handle rotation with mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            UpdateTargetRotation(scrollInput);
        }
    }
    private void FixedUpdate()
    {
        if (player.inHand == false) return;
        if (objectPickupTransformParent == null) return;
        if (player.inHandObject != this.gameObject) return;

        Vector3 newPos = Vector3.Lerp(this.transform.position, objectPickupTransformParent.position,
            Time.fixedDeltaTime * 10);
        this.rb.MovePosition(newPos);
        
        currentRotationY = Mathf.LerpAngle(currentRotationY, targetRotationY, Time.fixedDeltaTime * rotationSmoothing);
        Quaternion targetRotation = Quaternion.Euler(0, currentRotationY, 0);
        this.rb.MoveRotation(targetRotation);
    }
    public void UpdateTargetRotation(float scrollInput)
    {
        if (scrollInput > 0f)
        {
            targetRotationY += 90f * scrollSpeed * Time.fixedDeltaTime;
        }
        else if (scrollInput < 0f)
        {
            targetRotationY -= 90f * scrollSpeed * Time.fixedDeltaTime;
        }
    }
    void ChangeLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}