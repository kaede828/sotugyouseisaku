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
    [SerializeField]
    int hatudenkicount;//現在の発電機を起動した数
    int hatudenkigoal;//いくつ開ければ扉が開くか

    // Start is called before the first frame update
    void Start()
    {
        //slider = sliderObject.GetComponent<Slider>();
        hatudenkicount = 0;
        hatudenkigoal = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(hatudenkicount>=hatudenkigoal)
        {//発電機を規定個稼働させたらボスの扉を開く
            Debug.Log("扉があいた");
        }

        if (hit)
        {          
            Bbutton.color = new Color(1, 1, 1, 1);
        }
        else Bbutton.color = new Color(0, 0, 0, 0);
        //Debug.Log(value);
        if(value>=100)
        {
            value = 0;
            hatudenkicount += 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "hatudenkiHit")
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
        if(other.gameObject.tag == "hatudenkiHit")
        {
            hit = false;
            value = 0;
        }
    }
}
