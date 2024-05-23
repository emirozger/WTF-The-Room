using System;
using DG.Tweening;
using UnityEngine;

public class OldKey : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float openDuration = 1f;
    [SerializeField] private float openAngle = 200;
    [SerializeField] private float waitDuration = 2f;
    [SerializeField] private InteractablesParent interactables;
    private bool hasOpened = false;
    private Vector3 startRot;


    private void Start()
    {
        interactables = this.transform.parent.GetComponent<InteractablesParent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            var firstRot = other.transform.localEulerAngles;
            interactables.Drop();
            this.transform.parent.gameObject.SetActive(false);
            AudioManager.Instance.PlayOneShot("Door_Opening");
            other.transform.DOLocalRotate(new Vector3(0, openAngle, 0), openDuration)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(waitDuration, (() => AudioManager.Instance.PlayOneShot("Door_Closing")));
                    other.transform.DOLocalRotate(firstRot, 2f).SetDelay(waitDuration);
                });
            other.tag = "Untagged";
        }
    }
}