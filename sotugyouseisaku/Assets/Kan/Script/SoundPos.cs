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
        //���Ԃ��������玩��������
        if (other == null)
        {
            Destroy(gameObject);
        }
        else
        {
            //�G�ɂ��Ă���
            transform.position = other.position;
            var p = transform.position;
            transform.position = p;
        }
    }
}
