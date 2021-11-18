using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSoundExit : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("joystick start"))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void PauseSound()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        pauseUI.SetActive(!pauseUI.activeSelf);
    }
}
