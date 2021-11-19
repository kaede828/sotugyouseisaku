using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip zombieSE;

    private new AudioSource audio;
    // Start is called before the first frame update

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        // BGM‚ğ‰¹—Ê1.0‚ÅÄ¶
        Play(zombieSE, 1.0f);
    }

    // 1. Ä¶
    private void Play(AudioClip audioClip, float volume)
    {
        // AudioClip
        audio.clip = audioClip;

        // ‰¹—Ê
        audio.volume = volume;

        // Ä¶
        audio.Play();
    }

    // 2. ˆê’â~
    private void Pause()
    {
        audio.Pause();
    }

    // 3. ’â~
    private void Stop()
    {
        audio.Stop();
    }
}
