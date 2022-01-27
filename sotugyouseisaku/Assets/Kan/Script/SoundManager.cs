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
    private Transform zombies2;
    [SerializeField]
    private Transform zombiesEx;
    [SerializeField]
    private Transform zombiesEx2;
    [SerializeField]
    private Transform spiders;
    [SerializeField]
    private Transform spiders2;
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
            z.name = "ˆêŠKƒ]ƒ“ƒr:" + i.ToString();
            z.GetComponent<SoundPos>().SetTrans(zombies.transform.GetChild(i).transform);
            z.GetComponent<ZombieSound>().py = false;
            z.GetComponent<ZombieSound>().zombie = zombies.transform.GetChild(i).gameObject;
            seList.Add(z);
        }
        for (int i = 0; i < zombies2.transform.childCount; i++)
        {
            var z = Instantiate(zombieSE);
            z.name = "“ñŠKƒ]ƒ“ƒr:" + i.ToString();
            z.GetComponent<SoundPos>().SetTrans(zombies2.transform.GetChild(i).transform);
            z.GetComponent<ZombieSound>().py = true;
            z.GetComponent<ZombieSound>().zombie = zombies2.transform.GetChild(i).gameObject;
            seList.Add(z);
        }
        for (int i = 0; i < spiders.transform.childCount; i++)
        {
            var y = Instantiate(spiderSE);
            y.name = "ˆêŠKƒNƒ‚:" + i.ToString();
            y.GetComponent<SoundPos>().SetTrans(spiders.transform.GetChild(i).transform);
            y.GetComponent<ZombieSound>().py = false;
            seList.Add(y);
        }
        for (int i = 0; i < spiders2.transform.childCount; i++)
        {
            var y = Instantiate(spiderSE);
            y.name = "“ñŠKƒNƒ‚:" + i.ToString();
            y.GetComponent<SoundPos>().SetTrans(spiders2.transform.GetChild(i).transform);
            y.GetComponent<ZombieSound>().py = true;
            seList.Add(y);
        }
        ///

        for (int i = 0; i < zombiesEx.transform.childCount; i++)
        {
            var z = Instantiate(zombieSE);
            z.name = "ˆêŠKƒXƒeƒbƒvƒ]ƒ“ƒr:" + i.ToString();
            z.GetComponent<SoundPos>().SetTrans(zombiesEx.transform.GetChild(i).transform);
            z.GetComponent<ZombieSound>().py = false;
            z.GetComponent<ZombieSound>().zombie = zombiesEx.transform.GetChild(i).gameObject;
            seList.Add(z);
        }
        for (int i = 0; i < zombiesEx2.transform.childCount; i++)
        {
            var z = Instantiate(zombieSE);
            z.name = "“ñŠKƒXƒeƒbƒvƒ]ƒ“ƒr:" + i.ToString();
            z.GetComponent<SoundPos>().SetTrans(zombiesEx2.transform.GetChild(i).transform);
            z.GetComponent<ZombieSound>().py = true;
            z.GetComponent<ZombieSound>().zombie = zombiesEx2.transform.GetChild(i).gameObject;
            seList.Add(z);
        }
        ///
        var x = Instantiate(titanSE);
        x.name = "ƒ{ƒX";
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
