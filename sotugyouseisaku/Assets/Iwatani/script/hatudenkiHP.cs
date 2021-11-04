using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hatudenkiHP : MonoBehaviour
{
    GameObject canvasObject;
    GameObject sliderObject;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        canvasObject = transform.GetChild(0).gameObject;
        sliderObject = canvasObject.transform.GetChild(0).gameObject;
        //Debug.Log(sliderObject);
        slider = sliderObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(slider.value);
        if(slider.value>=100)
        {
            Destroy(gameObject);
        }
    }
}
