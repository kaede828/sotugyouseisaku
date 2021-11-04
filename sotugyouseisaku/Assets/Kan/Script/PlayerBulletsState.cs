using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletsState : MonoBehaviour
{
    [SerializeField]
    private AddForceBullet add;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ammunition")
        {
            add.bulletCount += col.gameObject.GetComponent<Ammunition_Item>().Bullets();
            Debug.Log(col.gameObject.GetComponent<Ammunition_Item>().Bullets());
        }
    }
}
