using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;


public class title : MonoBehaviour
{
    private float titleTime;
    bool timeF;
    public AudioClip se;
    AudioSource audioSource;
    bool titleSe;

    // Start is called before the first frame update
    void Start()
    {
        titleTime = 0;
        timeF = false;
        titleSe = false;
        audioSource = GetComponent<AudioSource>();
        //audioSource.pitch = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            timeF = true;
            if(titleSe == false)
            {
                audioSource.PlayOneShot(se);
                titleSe = true;
            }        
        }
    
        if (timeF == true)
        {
            titleTime += Time.deltaTime;
            Debug.Log("ŽžŠÔ" + titleTime);
            if (titleTime >= 2.5f)
            {
                FadeManager.Instance.LoadScene("Game", 2.0f);
                titleTime = 0.0f;
                timeF = false;
            }
        }

        
    }
}
