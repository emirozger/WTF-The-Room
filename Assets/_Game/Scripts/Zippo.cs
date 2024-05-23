using UnityEngine;
using DG.Tweening;

public class Zippo : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInteract player;
    [SerializeField] private Transform objectPickupTransformParent;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private float fireDuration = 5f;
    [SerializeField] private GameObject flameDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rope"))
        {
            Debug.Log("ovv fire");
            var temp = Instantiate(fire, other.transform.position, Quaternion.identity);
            temp.transform.parent = other.transform;
            DOVirtual.DelayedCall(fireDuration, () => Destroy(other.gameObject))
                .OnComplete(() =>
                {
                    if (flameDoor != null) flameDoor.tag = "Door";
                });
        }
    }
}