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
        audioMixerSnapshots[0].TransitionTo(1f);
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
            case 0:
            break;
            
            case 1: //from menu into temple
            musicSource2.clip = musicClips[1];//set clip to low amb
            musicSource2.Play();
            sfxSource.PlayOneShot(musicClips[3]);
            audioMixerSnapshots[2].TransitionTo(1f);//fade menu music
            StartCoroutine(StopAudio(musicSource1));//stop menu music
            break;

            case 2: ////mask on — may be an issue here bc of first time?
            //play monster layer
            musicSource1.clip = musicClips[2];
            musicSource1.Play();
            audioMixerSnapshots[3].TransitionTo(1f);
            break;

            case 3: //mask off
            //fade monster layer w snapshot
            //then stop monster layer
            audioMixerSnapshots[2].TransitionTo(1f);//fade monster music
            StartCoroutine(StopAudio(musicSource1));//stop monster music
            break;
        }
    }

    public IEnumerator StopAudio(AudioSource audioSource)
    {
        yield return new WaitForSeconds(3);
        audioSource.Stop();
        audioMixerSnapshots[4].TransitionTo(1f);//bring all music back up
    }

    public void JumpScareAudio()
    {
        audioMixerSnapshots[5].TransitionTo(1f); //turn off music source 2
        musicSource1.PlayOneShot(musicClips[3]);
        //add sfx here
    }
}
