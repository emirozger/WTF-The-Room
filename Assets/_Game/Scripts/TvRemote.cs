using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TvRemote : MonoBehaviour
{
    private string interactText = "Pick up TV Remote";
    private Highlight highlight;
    private Collider collider;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject textObject;
    [SerializeField] private AudioSource source;

    public string GetInteractText => interactText;
    public Highlight GetHighlight => highlight;
    public Collider GetCollider => collider;


    private void Awake()
    {
        highlight = GetComponent<Highlight>();
        collider = GetComponent<Collider>();
        videoPlayer.enabled = false;
    }

    public void OnInteract()
    {
        AudioManager.Instance.PlayOneShot("TV_Remote_Button_1");

        bool isVideoPlayerEnabled = videoPlayer.enabled;

        if (isVideoPlayerEnabled)
        {
            // TV is currently on, turn it off
            videoPlayer.enabled = false;
            source.Stop();
            if (textObject != null)
            {
                textObject.SetActive(false); // Disable the text
            }
        }
        else
        {
            // TV is currently off, turn it on
            videoPlayer.enabled = true;
            source.Play();
            if (textObject != null)
            {
                textObject.SetActive(true); // Enable the text
            }
        }
    }
}