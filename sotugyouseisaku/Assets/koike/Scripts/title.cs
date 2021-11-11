using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;


public class title : MonoBehaviour
{
    private float titleTime;
    bool timeF;
    // Start is called before the first frame update
    void Start()
    {
        titleTime = 0;
        timeF = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            timeF = true;
            
        }
    
        if (timeF == true)
        {
            titleTime += Time.deltaTime;
            Debug.Log("ŽžŠÔ" + titleTime);
            if (titleTime >= 2.0f)
            {
                
                //SceneManager.LoadScene("Game");
                FadeManager.Instance.LoadScene("Game", 1.0f);
                titleTime = 0.0f;
                timeF = false;
            }
        }

        
    }
}
