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
    //[SerializeField]
    // float stop = 4.0f;

    public bool py;

    private float playSECount;
    public GameObject zombie;

    private EnemyMove em;
    private StepZombieMove szm;
    private string name = "";
    private double FadeOutSeconds = 2.0f;
    bool IsFadeOut = false;
    double FadeDeltaTime = 0;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        // BGM������1.0�ōĐ�
        //Play(zombieSE, 1.0f);

        player = GameObject.FindGameObjectWithTag("Player");
        isplay = false;
        count = 0.0f;

        playSECount = Random.Range(0.0f, 5.0f);

        if(zombie != null)
        {
            if (zombie.tag == "StepZombie")
            {
                szm = zombie.GetComponent<StepZombieMove>();
                name = zombie.tag;
            }
            else if(zombie.tag == "Zombie")
            {
                em = zombie.GetComponent<EnemyMove>();
                name = zombie.tag;
            }
        }
        else
        {
            Debug.Log(gameObject);
        }
        IsFadeOut = false;
        FadeOutSeconds = 2.0f;
    }

    private void Update()
    {
        //var p = new Vector3(player.transform.position.x + point.x, player.transform.position.y + point.y, player.transform.position.z + point.z);
        //var d = new Vector3(gameObject.transform.position.x + point.x, gameObject.transform.position.y + point.y, gameObject.transform.position.z + point.z);
        //RaycastHit hit;
        //var dir = (p - d).normalized;
        //Debug.DrawRay(d, dir * 15.0f, Color.blue, 0.01f);
        //if (Physics.Raycast(d, dir, out hit, 30.0f))
        //{
        //    if(hit.collider.gameObject.tag == "Player")
        //    {
        //        if(!isplay)
        //        {
        //            //Debug.Log("on");
        //            isplay = true;
        //            Play(zombieSE, 1.0f);
        //        }
        //    }
        //    else
        //    {
        //        //Debug.Log(hit.collider.gameObject.tag);
        //        count += Time.deltaTime;
        //        if(count > stop)
        //        {
        //            Stop();
        //            isplay = false;
        //            count = 0.0f;
        //        }
        //    }
        //}

        var p = player.transform.position;
        if(py)
        {
            //��K
            if (p.y < 8.0f)
            {
                return;
            }

        }
        else if(!py)
        {
            //��K
            if(p.y > 8.0f)
            {
                Stop();
                isplay = false;
                count = 0.0f;
            }
        }

        if(!isplay)
        {
            if(count > playSECount)
            {
                Play(zombieSE, 1.0f); 
                isplay = true;
            }
            count += Time.deltaTime;
        }

        if(name == "StepZombie")
        {
            if(szm.isDeath && !IsFadeOut)
            {
                IsFadeOut = true;
            }
        }
        else if(name == "Zombie" && !IsFadeOut)
        {
            if(em.isDeath)
            {
                IsFadeOut = true;
            }
        }

        if (IsFadeOut)
        {
            
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime >= FadeOutSeconds)
            {
                FadeDeltaTime = FadeOutSeconds;
                IsFadeOut = false;
            }
            audio.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds);
            //Debug.Log(audio.volume);
        }
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
