using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class hatudenkiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> hatudenkiList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> hatudenkiHitList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> spawnPosList = new List<GameObject>();
    int hatudenkiCount = 0;
    [SerializeField]
    private GameObject eventCamera;
    [SerializeField]
    private GameObject door;
    Camera camera;
    GameObject canvasObject;
    GameObject sliderObject;

    void Start()
    {
        hatudenkiCount = hatudenkiList.Count;
        camera = eventCamera.GetComponent<Camera>();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("hatudenki");
        for (int i = 0; i < hatudenkiList.Count; i++)
        {
            hatudenkiHitList.Add(gameObjects[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hatudenkiHitList.Count; i++)
        {
            if(hatudenkiHitList[i]!=null&&hatudenkiHitList.Count>0)
            {
                Debug.Log(hatudenkiHitList[i].GetComponent<hatudenkiHP>().Hp);
                if (hatudenkiHitList[i].GetComponent<hatudenkiHP>().Hp >= 99)
                {
                    hatudenkiHitList.Remove(hatudenkiHitList[i]);
                    hatudenkiCount = hatudenkiHitList.Count;
                    //Debug.Log("‰ó‚ê‚½”­“d‹@‚Ì”:" + hatudenkiCount);
                }
            }
        }

        if (hatudenkiCount==0)
        {
            camera.depth = 1;
            StartCoroutine(DelayCoroutine(180, () =>
            {
                door.SetActive(false);
            }));
            StartCoroutine(DelayCoroutine(600, () =>
             {
                 camera.depth = -1;
             }));

        }
    }

    private IEnumerator DelayCoroutine(int Count, Action action)
    {
        for (var i = 0; i < Count; i++)
        {
            yield return null;
        }

        action?.Invoke();
    }
}
