using DG.Tweening;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] private GameObject doorHandle;
    [SerializeField] private InteractablesParent interactables;
    [SerializeField] private float openDuration = 3f;
    [SerializeField] private float openAngle = 180f;
    [SerializeField] private float waitDuration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            var firstRot = doorHandle.transform.parent.localEulerAngles;
            doorHandle.SetActive(true);
            interactables.Drop();
            this.transform.parent.gameObject.SetActive(false);
            AudioManager.Instance.PlayOneShot("Door_Opening");
            doorHandle.transform.parent.DORotate(new Vector3(0, openAngle, 0), openDuration)
                .OnComplete(() =>
                {
                    AudioManager.Instance.PlayOneShot("Door_Closing");
                    doorHandle.transform.parent.DORotate(firstRot, openDuration).SetDelay(waitDuration)
                        .OnComplete((() => doorHandle.transform.parent.tag = "Untagged"));
                });
        }
    }
}