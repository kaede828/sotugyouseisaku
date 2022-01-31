using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class postEffect : MonoBehaviour
{
    public  float vigparam = 0.6f;

    PostProcessVolume postProcess;
    Vignette vignette;

    public float exposure;//postExposure‚Ì’l
    ColorGrading colGrad;



    //public GameObject postEffe;
    // Start is called before the first frame update
    void Start()
    {
        exposure = 0.3f;

        //PostVolumeŽæ“¾
        postProcess = GameObject.Find("postEffect").gameObject.GetComponent<PostProcessVolume>();

        foreach (PostProcessEffectSettings item in postProcess.profile.settings)
        {
            if (item as Vignette)
            {
                vignette = item as Vignette;
            };

            if (item as ColorGrading)
            {
                colGrad = item as ColorGrading;
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        //Vignette‚ÌIntensity‰ÁŽZ
        if (vignette)
        {
            vignette.intensity.value = vigparam;
        }

        if(colGrad)
        {
            colGrad.postExposure.value = exposure;
        }
    }
}
