using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class hatudenkiHP : MonoBehaviour
{
    GameObject canvasObject;
    GameObject sliderObject;
    Slider slider;
    float value;
    AudioSource source;
    [SerializeField]
    AudioClip se;
    public float Hp;

    // Start is called before the first frame update
    void Start()
    {
        canvasObject = transform.GetChild(0).gameObject;
        sliderObject = canvasObject.transform.GetChild(0).gameObject;
        //Debug.Log(sliderObject);
        slider = sliderObject.GetComponent<Slider>();
        value = slider.value;
        Hp = value;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Hp = value;
        //Debug.Log(slider.value);
        if(slider.value>=100)
        {
            Destroy(gameObject);
        }
        if (value < slider.value&&sourceCheck(source))
        {
            source.PlayOneShot(se);
            //Debug.Log("音が鳴ったよ");
        }
        value = slider.value;
        //Debug.Log("source.time:" + source.time);
        //Debug.Log("source.isPlaying:" + !source.isPlaying);

        if (Input.GetButtonUp("joystick B"))
        {
            source.Stop();
        }
    }
    static bool sourceCheck(AudioSource source)
    {
        return source.time == 0.0f && !source.isPlaying;
    }
}