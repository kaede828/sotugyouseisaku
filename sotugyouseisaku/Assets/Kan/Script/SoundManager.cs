using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bgmObj;

    private List<GameObject> seList;

    [SerializeField]
    private Transform zombies;
    [SerializeField]
    private Transform spiders;
    [SerializeField]
    private Transform titan;

    [SerializeField]
    private GameObject zombieSE;
    [SerializeField]
    private GameObject spiderSE;
    [SerializeField]
    private GameObject titanSE;
    [SerializeField]
    private GameObject playerSE;

    [SerializeField]
    private GameObject pauseSoundUI;

    // Start is called before the first frame update
    void Start()
    {
        seList = new List<GameObject>();
        for (int i = 0; i < zombies.transform.childCount; i++)
        {
            var z = Instantiate(zombieSE);
            z.GetComponent<SoundPos>().SetTrans(zombies.transform.GetChild(i).transform);
            seList.Add(z);
        }
        for (int i = 0; i < spiders.transform.childCount; i++)
        {
            var y = Instantiate(spiderSE);
            y.GetComponent<SoundPos>().SetTrans(spiders.transform.GetChild(i).transform);
            seList.Add(y);
        }

        var x = Instantiate(titanSE);
        x.GetComponent<SoundPos>().SetTrans(titan.transform);
        seList.Add(x);
        seList.Add(playerSE);

        bgmObj = Instantiate(bgmObj);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < seList.Count; i++)
        {
            if(seList[i] == null)
            {
                seList.RemoveAt(i);
            }
        }

        if (pauseSoundUI == null)
            return;

        if (pauseSoundUI.activeSelf)
        {
            for (int i = 0; i < seList.Count; i++)
            {
                float all = pauseSoundUI.transform.GetChild(1).GetComponent<Slider>().value;//‘S‘Ì
                float se = pauseSoundUI.transform.GetChild(3).GetComponent<Slider>().value;//se
                seList[i].GetComponent<AudioSource>().volume = se * all;
            }

            {
                float all = pauseSoundUI.transform.GetChild(1).GetComponent<Slider>().value;//‘S‘Ì
                float bgm = pauseSoundUI.transform.GetChild(2).GetComponent<Slider>().value;//bgm
                bgmObj.GetComponent<AudioSource>().volume = bgm * all;
            }
        }
    }
}
