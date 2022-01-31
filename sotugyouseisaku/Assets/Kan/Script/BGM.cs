using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField]
    private AudioClip bossBGM;

    private new AudioSource audio;

    public bool isBoss;
    public bool isClear;
    public bool IsFade;
    public double FadeOutSeconds = 1.0;
    double FadeDeltaTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        isBoss = false;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBoss)
        {
            audio.clip = bossBGM;
            audio.Play();
            isBoss = false;
        }

        if(isClear)
        {
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime >= FadeOutSeconds)
            {
                FadeDeltaTime = FadeOutSeconds;
                isClear =  false;
            }
            audio.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds);          
        }
    }
}
