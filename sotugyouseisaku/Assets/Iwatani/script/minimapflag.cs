using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapflag : MonoBehaviour
{
    GameObject mapposobj;
    minimapPos minimappos;
    // Start is called before the first frame update
    void Start()
    {
        mapposobj = GameObject.Find("Pimage");
        minimappos = mapposobj.GetComponent<minimapPos>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="1Fflag")
        {
            minimappos.nikaiimageflag = false;
            minimappos.ikkaiimageflag = true;
        }

        if (other.gameObject.tag=="2Fflag")
        {
            minimappos.ikkaiimageflag = false;
            minimappos.nikaiimageflag = true;           
        }
    }
}
