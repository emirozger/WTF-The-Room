using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wrench : MonoBehaviour
{
    private float timer = 0;
    //[SerializeField] private InputActionReference lookAction;
    private float holdTime = 0f;
    private bool rotating = false;
    private bool hasPlayedSound = false;
    [SerializeField] private GameObject pipeBehindDoor;
    private int brokenPipesCount = 0;
    private int totalPipes = 9;
    [SerializeField] private ParticleSystem pipeParticle;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            if (Input.GetMouseButton(0))
            {
                
                if (hasPlayedSound == false)
                {
                    AudioManager.Instance.PlayOneShot("Pipe_Sound");
                    var temp = Instantiate(pipeParticle, other.transform.position, Quaternion.identity);
                    temp.transform.parent= other.transform;
                    DOVirtual.DelayedCall(1f, () => AudioManager.Instance.PlayOneShot("Pipe_Sound"));
                    DOVirtual.DelayedCall(2f, () => AudioManager.Instance.PlayOneShot("Pipe_Sound"));
                    hasPlayedSound = true;
                }
                holdTime += Time.deltaTime;
                
                if (holdTime >= 1.5f && !rotating)
                {
                    rotating = false;
                    holdTime = 0f;
                    Destroy(other.gameObject);
                    brokenPipesCount++;
                    if (brokenPipesCount >= totalPipes)
                    {
                        pipeBehindDoor.tag = "Door";
                    }
                }
            }
            else
            {
                
                hasPlayedSound = false;
                holdTime = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0;
    }
}
