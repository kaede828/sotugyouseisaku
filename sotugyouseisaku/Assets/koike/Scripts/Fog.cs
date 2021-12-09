using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    bool fogDen = false;
    float density = 0.03f; //fogDensityの値

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //density値制限
        Mathf.Clamp(density, 0, 0.03f);
        if(fogDen == true && density >= 0)
        {
            //fogDensityの値変化
            density -= 0.01f * Time.deltaTime;
            RenderSettings.fogDensity = density;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            fogDen = true;
        }
    }
}
