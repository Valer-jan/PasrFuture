using UnityEngine;

public class AN_SimpleSoundRandom : MonoBehaviour
{
    void Start()
    {
        AudioSource aud = GetComponent<AudioSource>();
        if (aud != null) aud.pitch = Random.Range(.9f, 1.1f);
    }
}
