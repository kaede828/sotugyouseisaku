using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            img.color = new Color(1, 1, 1, 1);
        }
        else
        {
            img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
        }
    }
}
