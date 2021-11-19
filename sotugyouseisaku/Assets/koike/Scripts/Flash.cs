using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    private Image img;
    bool titleFlash;

    // Start is called before the first frame update
    void Start()
    {
        titleFlash = false;
        img = GetComponent<Image>();
        img.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            if (titleFlash == false)
            {
                img.color = new Color(1, 1, 1, 1);
                titleFlash = true;
            }
        }
        else
        {
            img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
        }
    }
}
