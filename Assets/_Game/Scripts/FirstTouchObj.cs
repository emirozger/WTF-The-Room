using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstTouchObj : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInteract player;
    [SerializeField] private GameObject door;
    [SerializeField] private Transform objectPickupTransformParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            door.transform.DORotate(new Vector3(0, 90, 0), 1f);
        }
    }

  
}