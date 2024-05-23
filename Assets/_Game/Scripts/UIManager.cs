using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI interactHitInfo;
    [SerializeField] private Image interactImage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideInteractUI();
    }

    public void SetupInteractUI(string text)
    {
        interactHitInfo.text = text;
        interactHitInfo.gameObject.SetActive(true);
        interactImage.gameObject.SetActive(true);
    }
    public void HideInteractUI()
    {
        interactHitInfo.gameObject.SetActive(false);
        interactImage.gameObject.SetActive(false);
        interactHitInfo.text = string.Empty;
    }
}