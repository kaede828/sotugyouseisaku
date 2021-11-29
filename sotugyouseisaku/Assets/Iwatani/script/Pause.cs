using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    //�@�|�[�Y�������ɕ\������UI�̃v���n�u
    [SerializeField]
    private  GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("joystick start"))
        {
            
            //�@�|�[�YUI�̃A�N�e�B�u�A��A�N�e�B�u��؂�ւ�
            pauseUI.SetActive(!pauseUI.activeSelf);

            //�@�|�[�YUI���\������Ă鎞�͒�~
            if (pauseUI.activeSelf)
            {
                Debug.Log("�|�[�Y��");
                Time.timeScale = 0f;
                //�@�|�[�YUI���\������ĂȂ���Βʏ�ʂ�i�s
            }
            else
            {
                Debug.Log("�|�[�Y����");
                Time.timeScale = 1f;
            }
        }

        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
    }
}
