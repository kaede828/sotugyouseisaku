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
        Debug.Log("ボタンが押された");
        pauseUI.SetActive(!pauseUI.activeSelf);

        //　ポーズUIが表示されてる時は停止
        if (pauseUI.activeSelf)
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
}
