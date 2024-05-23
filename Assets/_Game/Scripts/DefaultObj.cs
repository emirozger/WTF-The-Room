using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DefaultObj : MonoBehaviour
{
    [SerializeField] private GameObject door;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInteract player;
    [SerializeField] private Transform objectPickupTransformParent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            door.transform.DORotate(new Vector3(0,0,0), 1f);
        }
    }
    
    
}
