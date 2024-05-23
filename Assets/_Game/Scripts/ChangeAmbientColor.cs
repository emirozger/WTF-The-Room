using UnityEngine;

public class ChangeAmbientColor : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color defaultAmbientColor;
    
    [ColorUsage(true, true)]
    public Color darkAmbientColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DefaultLight"))
        {
            SetAmbientColor(defaultAmbientColor);
        }
        else if (other.CompareTag("DarkLight"))
        {
            SetAmbientColor(darkAmbientColor);
        }
    }

    public void SetAmbientColor(Color color)
    {
        RenderSettings.ambientLight = color;
    }
}