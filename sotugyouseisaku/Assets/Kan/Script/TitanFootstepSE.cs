using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanFootstepSE : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] bool randomizePitch = true;
    [SerializeField] float pitchRange = 0.1f;

    private AudioSource source;

    private void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void PlayFootstepSE()
    {
        if (randomizePitch)
            source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);

        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);

        Debug.Log("‚È‚Á‚½");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
