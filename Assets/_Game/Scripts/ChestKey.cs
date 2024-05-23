using System;
using DG.Tweening;
using UnityEngine;

public class ChestKey : MonoBehaviour
{
    [SerializeField] private Animation chestAnim;
    [SerializeField] private InteractablesParent interactables;
    private bool isOpened = false;


    private void Start()
    {
        interactables = this.transform.parent.GetComponent<InteractablesParent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chest") && isOpened == false)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        chestAnim.Play();
        AudioManager.Instance.PlayOneShot("Chest_Opening");
        isOpened = true;
        interactables.Drop();
        interactables.gameObject.SetActive(false);
    }
}