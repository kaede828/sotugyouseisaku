using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Item : MonoBehaviour
{

    [SerializeField]
    private int heal = 20;
    public Player player;
    public postEffect post;

    [SerializeField]
    private AudioClip getSE;
    [SerializeField]
    private AudioClip useSE;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("joystick R1"))
        {
            if (player.hp < 100 && player.healCount > 0)
            {
                player.hp += heal;
                post.vigparam -= 0.061f * 2;
                player.healCount -=1;
                Debug.Log("回復ストック" + player.healCount);              
                player.GetComponent<AudioSource>().PlayOneShot(useSE);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("回復ストック" + count);
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(getSE);
            Destroy(gameObject);
        }
    }
}
