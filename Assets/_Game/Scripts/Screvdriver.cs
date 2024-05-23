using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Add this for DOTween

public class Screvdriver : MonoBehaviour
{
    public GameObject screwDriver;
    private float holdTime = 0f;
    private bool rotating = false;
    private float rotationAngle = 90f;
    private float rotationDuration = 1f;
    private int screwsRotatedCount = 0;
    private int totalScrews = 4; 
    private bool isRotatingRight = false;
    private bool isRotatingLeft = false;
    [SerializeField] private Rigidbody wentRb;
    [SerializeField] private PlayerInteract player;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Screw"))
        {
            if (Input.GetMouseButton(0))
            {
                holdTime += Time.deltaTime;

                if (holdTime >= 1f && !rotating)
                {
                    RotateScrewDriver(other);
                }
            }
            else
            {
                holdTime = 0f;
            }
        }
    }

    private void RotateScrewDriver(Collider other)
    {
        rotating = true;
        AudioManager.Instance.PlayOneShot("Screw_Sound");
        screwDriver.transform.DORotate(new Vector3(0, rotationAngle, 0), rotationDuration, RotateMode.LocalAxisAdd)
            .OnComplete(() =>
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                
                rotating = false;
                holdTime = 0f;

                screwsRotatedCount++;
                if (screwsRotatedCount >= totalScrews)
                {
                    wentRb.isKinematic = false;
                    AudioManager.Instance.PlayOneShot("Went_Drop");
                }
            });
    }
    private bool isScrewing = false;
    private void Update()
    {
        if (player.inHandObject != this.transform.parent.gameObject)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            isScrewing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isScrewing = false;
        }
        if (isScrewing)
        {
            // TODO: Rotate sound
            
            transform.Rotate(Vector3.up * Time.deltaTime * 100);
        }
    }
}