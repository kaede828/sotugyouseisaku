using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class setselected : MonoBehaviour
{
    // Start is called before the first frame update
    EventSystem eventSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
