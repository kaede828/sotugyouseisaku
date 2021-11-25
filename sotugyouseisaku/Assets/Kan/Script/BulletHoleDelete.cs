using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleDelete : MonoBehaviour
{
    private float countTime = 0.0f;
    [SerializeField]
    private float deadTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(countTime);
        countTime += Time.deltaTime;
        if (countTime > deadTime)
        {        
            Destroy(gameObject);
        }
    }
}
