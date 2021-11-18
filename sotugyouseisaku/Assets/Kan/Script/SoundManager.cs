using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bgmList;

    [SerializeField]
    private Transform zombies;
    [SerializeField]
    private Transform spiders;
    [SerializeField]
    private Transform titan;

    public List<GameObject> enemyList;

    [SerializeField]
    private GameObject zombieSE;
    [SerializeField]
    private GameObject spiderSE;
    [SerializeField]
    private GameObject titanSE;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i < zombies.transform.childCount;i++)
        {
            enemyList.Add(zombies.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < spiders.transform.childCount; i++)
        {
            enemyList.Add(spiders.transform.GetChild(i).gameObject);
        }

        enemyList.Add(titan.gameObject);


        for (int i = 0; i < enemyList.Count; i++)
        {

            string name = enemyList[i].tag;

            switch(name)
            {
                case "Zombie":
                    var z = Instantiate(zombieSE);
                    z.GetComponent<SoundPos>().SetTrans(enemyList[i].transform);
                    break;
                case "Enemy":
                    var y = Instantiate(spiderSE);
                    y.GetComponent<SoundPos>().SetTrans(enemyList[i].transform);
                    break;
                case "Boss":
                    var x = Instantiate(titanSE);
                    x.GetComponent<SoundPos>().SetTrans(enemyList[i].transform);
                    break;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
