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

    public void SceneSwitchMusic(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0: //start
            //return to beginning when you fail?
            break;

            case 1: //from menu into temple
            musicSource2.clip = musicClips[1];
            musicSource2.Play();
            audioMixerSnapshots[1].TransitionTo(1f);
            break;

            case 2: //temple to maze
            //fade source1 (menu music) w snapshots
            //wait a bit then stop source1
            break;

            case 3: //mask on — may be an issue here bc of first time?
            //play monster layer
            break;

            case 4: //mask off
            //fade monster layer w snapshot
            //then stop monster layer
            break;


        }
    }
}
