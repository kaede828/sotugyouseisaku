using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hatudenki : MonoBehaviour
{
    [SerializeField]
    Image Bbutton;
    bool hit=false;
    Slider slider=null;
    float value;

    // Start is called before the first frame update
    void Start()
    {
        //slider = sliderObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hit)
        {          
            Bbutton.color = new Color(1, 1, 1, 1);
        }
        else Bbutton.color = new Color(0, 0, 0, 0);
        //Debug.Log(value);
        if(value>=100)
        {
            value = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "hatudenkiHit"|| other.gameObject.tag == "2FhatudenkiHit")
        {
            GameObject canvasObject = other.gameObject.transform.GetChild(0).gameObject;
            GameObject sliderObject= canvasObject.gameObject.transform.GetChild(0).gameObject;
            slider = sliderObject.GetComponent<Slider>();
            //Debug.Log("a");
            if (Input.GetButton("joystick B"))
            {
                sliderObject.SetActive(true);
                value += 1.0f;
                slider.value = value;
                hit = false;
            }
            else
            {
                sliderObject.SetActive(false);
                hit = true;
                value = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "hatudenkiHit"|| other.gameObject.tag == "2FhatudenkiHit")
        {
            hit = false;
        }
    }
}
