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
    int hatudenkiCount = 0;
    [SerializeField]
    private GameObject eventCamera;
    [SerializeField]
    private GameObject door;
    Camera camera;
    void Start()
    {
        hatudenkiCount = hatudenkiList.Count;
        camera = eventCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hatudenkiList.Count; i++)
        {
            if (hatudenkiList[i] == null)
            {
                hatudenkiList.Remove(hatudenkiList[i]);
                hatudenkiCount = hatudenkiList.Count;
                Debug.Log("‰ó‚ê‚½”­“d‹@‚Ì”:" + hatudenkiCount);
            }
        }

        if(hatudenkiCount==0)
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
