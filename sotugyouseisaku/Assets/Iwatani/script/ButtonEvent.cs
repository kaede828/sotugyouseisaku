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
        //　装備アイテム選択中にステータス画面を抜けた時にボタンが無効化したままの場合もあるので立ち上げ時に有効化する
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
        Debug.Log("ボタンが押された");
        gameObject.SetActive(!gameObject.activeSelf);

        //　ポーズUIが表示されてる時は停止
        if (gameObject.activeSelf)
        {
            Debug.Log("ポーズ中");
            Time.timeScale = 0f;
            //　ポーズUIが表示されてなければ通常通り進行
        }
        else
        {
            Debug.Log("ポーズ解除");
            Time.timeScale = 1f;
        }

        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
    }

    //サウンド画面を出す
    public void PauseSound()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        pauseSoundUI.SetActive(!pauseSoundUI.activeSelf);
    }
}