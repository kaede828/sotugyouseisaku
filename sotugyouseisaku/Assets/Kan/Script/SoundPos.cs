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
        //À‘Ô‚ªÁ‚¦‚½‚ç©•ª‚àÁ‚·
        if (other == null)
        {
            Destroy(gameObject);
        }
        else
        {
            //“G‚É‚Â‚¢‚Ä‚¢‚­
            transform.position = other.position;
        }
    }
}
