using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AN_MusicManager : MonoBehaviour
{
    public AudioClip Battle, Defeat;
    public AudioMixerGroup effectsMixer, musicMixer;

    AN_GameManager manager;
    AudioSource source;
    bool defeatTrigger = false;

    private void Start()
    {
        manager = FindObjectOfType<AN_GameManager>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (manager.StartGamePlayTrigger && !manager.ItsOver && !source.isPlaying)
        {
            source.clip = Battle;
            source.Play();
        }
        else if (manager.ItsOver)
        {
            float eff, mus;
            effectsMixer.audioMixer.GetFloat("EffectsVolume", out eff);
            // effectsMixer.audioMixer.GetFloat("MusicVolume", out mus);

            if (eff > -70f) effectsMixer.audioMixer.SetFloat("EffectsVolume", eff - Time.deltaTime * 2);
            if (!defeatTrigger)
            {
                source.clip = Defeat;
                source.Play();
                defeatTrigger = true;
            }
        }
    }
}
