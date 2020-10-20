using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_AudioRandom : MonoBehaviour
{
    public int Delay = 2, LimitCount = 500;
    public float AddRandomDelay = 1f;
    public AudioClip[] AudioClips;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        StartCoroutine(SelectAudio());
    }

    IEnumerator SelectAudio()
    {
        while (LimitCount > 0)
        {
            source.clip = AudioClips[Random.Range(0, AudioClips.Length)];
            source.pitch = Random.Range(.9f, 1.1f);
            source.Play();

            LimitCount--;
            yield return new WaitForSeconds(Delay + Random.Range(0f, AddRandomDelay));
        }
    }
}
