using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalBullet : MonoBehaviour
{
    private AddForceBullet addForceBullet;

    // Start is called before the first frame update
    void Start()
    {
        addForceBullet = GameObject.Find("Muzzle").GetComponent<AddForceBullet>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = addForceBullet.bulletHave.ToString();
    }
}
