using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class postEffect : MonoBehaviour
{
    public  float vigparam = 0.6f;

    PostProcessVolume postProcess;
    Vignette vignette;

    public float exposure;//postExposure�̒l
    ColorGrading colGrad;

    //���邳UI
    [SerializeField]
    private GameObject pauseSoundUI;

    //public GameObject postEffe;
    // Start is called before the first frame update
    void Start()
    {
        exposure = 0.3f;

        //PostVolume�擾
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
       
        //Vignette��Intensity���Z
        if (vignette)
        {
            vignette.intensity.value = vigparam;
        }

        if(colGrad)
        {
            //UI�̂΂��[���ύX���ꂽ�炻�̒l������
            colGrad.postExposure.value = pauseSoundUI.transform.GetChild(5).GetComponent<Slider>().value;
            //colGrad.postExposure.value = exposure;
        }
    }
}
