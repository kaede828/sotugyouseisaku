using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    bool fogDen = false;
    float density = 0.03f; //fogDensity‚Ì’l

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //density’l§ŒÀ
        Mathf.Clamp(density, 0, 0.03f);
        if(fogDen == true && density >= 0)
        {
            //fogDensity‚Ì’l•Ï‰»
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
