using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frieze : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 0);
    }
}
