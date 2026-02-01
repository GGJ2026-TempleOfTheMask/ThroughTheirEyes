using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class All_Audio_Manager : MonoBehaviour
{
    public AudioSource musicSource1;
    public AudioSource musicSource2;
    public AudioSource musicOneShots;
    public AudioSource sfxSource1;
    public AudioSource sfxSource2;
    public AudioSource sfxOneShots;
    private AudioSource monsterSourceBed;
    private AudioSource monsterSourceOneShots;
    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;
    public AudioClip[] monsterClips;
    public AudioMixerSnapshot[] audioMixerSnapshots;
    private bool firstTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioMixerSnapshots[0].TransitionTo(1f);
        musicSource1.clip = musicClips[0];
        musicSource1.Play();
        firstTime = true;
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
            musicOneShots.PlayOneShot(musicClips[3]);
            sfxSource1.clip = sfxClips[0];
            sfxSource1.Play();
            audioMixerSnapshots[2].TransitionTo(1f);//fade menu music
            StartCoroutine(StopAudio(musicSource1));//stop menu music
            break;

            case 2: ////mask on
            //play monster layer
            sfxSource2.clip = sfxClips[2];
            sfxSource2.loop = true;
            sfxSource2.Play();
            sfxOneShots.PlayOneShot(sfxClips[3]);
            audioMixerSnapshots[3].TransitionTo(1f);
            break;

            case 3: //mask off
            //fade monster layer w snapshot
            //then stop monster layer
            if (firstTime)
                {
                    monsterSourceBed = GameObject.Find("MonsterBed").GetComponent<AudioSource>();
                    monsterSourceOneShots = GameObject.Find("MonsterOneShots").GetComponent<AudioSource>();
                    monsterSourceBed.Play();
                     musicSource1.clip = musicClips[2];
                    musicSource1.Play();
                    StartCoroutine(MonsterNoise());
                    firstTime = !firstTime;
                }
            sfxOneShots.PlayOneShot(sfxClips[4]);
            sfxOneShots.loop = false;
            audioMixerSnapshots[4].TransitionTo(1f);//fade monster music
            StartCoroutine(StopAudio(sfxSource2));//stop mask noisees
            break;
        }
    }

    public IEnumerator StopAudio(AudioSource audioSource)
    {
        yield return new WaitForSeconds(3);
        audioSource.Stop();
        //audioMixerSnapshots[4].TransitionTo(1f);//bring all music back up
    }

    IEnumerator MonsterNoise()
    {
        yield return new WaitForSeconds(Random.Range(1, 4));
        monsterSourceOneShots.clip = monsterClips[Random.Range(0, monsterClips.Length)];
        monsterSourceOneShots.Play();
        StartCoroutine(MonsterNoise());
    }

    public void JumpScareAudio()
    {
        audioMixerSnapshots[5].TransitionTo(1f); //turn off music source 2
        musicOneShots.PlayOneShot(musicClips[3]);
        sfxOneShots.PlayOneShot(sfxClips[1]);
        //add sfx here
    }

    public void StopAudio2()
    {
        musicSource1.Stop();
        musicSource2.Stop();
        sfxSource1.Stop();
        monsterSourceBed.Stop();
        monsterSourceOneShots.Stop();
    }
}
