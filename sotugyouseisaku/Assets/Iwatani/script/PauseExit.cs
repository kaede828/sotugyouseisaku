using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseExit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject pauseUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("�{�^���������ꂽ");
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

        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
    }
}
