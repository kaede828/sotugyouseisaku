using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPos : MonoBehaviour
{
    private Transform other = null;

    public void SetTrans(Transform t)
    {
        other = t;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //é¿ë‘Ç™è¡Ç¶ÇΩÇÁé©ï™Ç‡è¡Ç∑
        if (other == null)
        {
            Destroy(gameObject);
        }
        else
        {
            //ìGÇ…Ç¬Ç¢ÇƒÇ¢Ç≠
            transform.position = other.position;
            var p = transform.position;
            transform.position = p;
        }
    }
}
