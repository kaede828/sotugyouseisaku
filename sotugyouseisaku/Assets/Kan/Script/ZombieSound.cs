using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip zombieSE;

    private new AudioSource audio;
    // Start is called before the first frame update

    private GameObject player;
    private bool isplay;

    [SerializeField]
    private Vector3 point;

    float count = 0.0f;
    [SerializeField]
    float stop = 4.0f;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        // BGMÇâπó 1.0Ç≈çƒê∂
        //Play(zombieSE, 1.0f);

        player = GameObject.FindGameObjectWithTag("Player");
        isplay = false;
        count = 0.0f;
    }

    private void Update()
    {
        var p = new Vector3(player.transform.position.x + point.x, player.transform.position.y + point.y, player.transform.position.z + point.z);
        var d = new Vector3(gameObject.transform.position.x + point.x, gameObject.transform.position.y + point.y, gameObject.transform.position.z + point.z);
        RaycastHit hit;
        var dir = (p - d).normalized;
        Debug.DrawRay(d, dir * 15.0f, Color.blue, 0.01f);
        if (Physics.Raycast(d, dir, out hit, 30.0f))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                if(!isplay)
                {
                    //Debug.Log("on");
                    isplay = true;
                    Play(zombieSE, 1.0f);
                }
            }
            else
            {
                //Debug.Log(hit.collider.gameObject.tag);
                count += Time.deltaTime;
                if(count > stop)
                {
                    Stop();
                    isplay = false;
                    count = 0.0f;
                }
            }
        }
    }

    // 1. çƒê∂
    private void Play(AudioClip audioClip, float volume)
    {
        // AudioClip
        audio.clip = audioClip;

        // âπó 
        audio.volume = volume;

        // çƒê∂
        audio.Play();
    }

    // 2. àÍéûí‚é~
    private void Pause()
    {
        audio.Pause();
    }

    // 3. í‚é~
    private void Stop()
    {
        audio.Stop();
    }
}
