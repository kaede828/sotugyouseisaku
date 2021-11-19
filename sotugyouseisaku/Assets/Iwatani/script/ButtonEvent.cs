using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseSoundUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        //�@�����A�C�e���I�𒆂ɃX�e�[�^�X��ʂ𔲂������Ƀ{�^���������������܂܂̏ꍇ������̂ŗ����グ���ɗL��������
        //GetComponent<Button>().interactable = true;
    }

    public void OnGameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    public void PauseExit()
    {
        Debug.Log("�{�^���������ꂽ");
        gameObject.SetActive(!gameObject.activeSelf);

        //�@�|�[�YUI���\������Ă鎞�͒�~
        if (gameObject.activeSelf)
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

    //�T�E���h��ʂ��o��
    public void PauseSound()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        pauseSoundUI.SetActive(!pauseSoundUI.activeSelf);
    }
}