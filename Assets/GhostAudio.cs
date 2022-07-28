using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAudio : MonoBehaviour
{
     private AudioSource GhostAudioSource;
     [SerializeField] private AudioClip[] GhostAudioClips;
     
    void Start()
    {
        GhostAudioSource = GetComponent<AudioSource>();

                GhostAudioSource.clip = GhostAudioClips[Random.Range(0,GhostAudioClips.Length)];
                GhostAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsInvoking("PlayNextClip"))
        {
            Invoke("PlayNextClip", GhostAudioSource.clip.length);
        }               
    }

    void PlayNextClip()
    {
                GhostAudioSource.clip = GhostAudioClips[Random.Range(0, GhostAudioClips.Length)];
                GhostAudioSource.Play();
    }
}
