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
        //実態が消えたら自分も消す
        if (other == null)
        {
            Destroy(gameObject);
        }
        else
        {
            //敵についていく
            transform.position = other.position;
        }
    }
}
