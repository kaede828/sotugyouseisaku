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
        // BGM������1.0�ōĐ�
        Play(zombieSE, 1.0f);
    }

    // 1. �Đ�
    private void Play(AudioClip audioClip, float volume)
    {
        // AudioClip
        audio.clip = audioClip;

        // ����
        audio.volume = volume;

        // �Đ�
        audio.Play();
    }

    // 2. �ꎞ��~
    private void Pause()
    {
        audio.Pause();
    }

    // 3. ��~
    private void Stop()
    {
        audio.Stop();
    }
}
