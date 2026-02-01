using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class All_Audio_Manager : MonoBehaviour
{
    public AudioSource musicSource1;
    public AudioSource musicSource2;
    public AudioSource sfxSource;
    public AudioClip[] musicClips;
    public AudioMixerSnapshot[] audioMixerSnapshots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        musicSource1.clip = musicClips[0];
        musicSource1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
