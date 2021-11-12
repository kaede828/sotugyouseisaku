using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_UI : MonoBehaviour
{
    private AddForceBullet addForceBullet;
    private GameObject slider, reticle;
    // Start is called before the first frame update
    void Start()
    {
        addForceBullet = GameObject.Find("Muzzle").GetComponent<AddForceBullet>();
        slider = transform.Find("Slider").gameObject;
        reticle = transform.Find("Reticle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (addForceBullet.isReload)
        {
            slider.SetActive(true);
            reticle.SetActive(false);
        }
        else
        {
            reticle.SetActive(true);
            slider.SetActive(false);
        }
    }
}
