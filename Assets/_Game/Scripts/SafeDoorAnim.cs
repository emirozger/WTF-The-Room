using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SafeDoorAnim : MonoBehaviour
{
    [SerializeField] private Vector3 openRotation = new Vector3(0, -110, 0);
    [SerializeField] private float openDuration = 1.5f;
    [SerializeField] private Ease openEase;
    
    public void OpenCase()
    {
        this.transform.DOLocalRotate(openRotation, openDuration).SetEase(openEase);
    }
}