using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition_Item : MonoBehaviour
{
    [SerializeField]
    private int bullets = 30;
    [SerializeField]
    private AudioClip se;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Bullets()
    {
        return bullets;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            var p = other.gameObject.GetComponentInChildren<AddForceBullet>();
            p.bulletHave += bullets;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(se);
            Destroy(gameObject);
        }
    }
}
