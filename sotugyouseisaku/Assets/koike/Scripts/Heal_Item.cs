using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Item : MonoBehaviour
{

    
    public int heal = 20;
    public Player player;
    public postEffect post;

    [SerializeField]
    private AudioClip getSE;
    [SerializeField]
    private AudioClip useSE;

    bool healing;
    // Start is called before the first frame update
    void Start()
    {
        healing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Heal());
        if(Input.GetButtonDown("joystick R1"))
        {
            Debug.Log("１回");
            healing = true;
            Debug.Log("回復フラグ" + healing);
            healing = false;
        }
        
        Debug.Log("回復フラグ" + healing);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("回復ストック" + count);
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(getSE);
            Destroy(gameObject);
            player.healCount += 1;
        }
    }

     void Heal()
    {
        //healing = false;
        if (Input.GetButtonDown("joystick R1"))
        {
            
            if (player.hp < 100 && player.healCount > 0 && healing == false)
            {
               
                    healing = true;
                    
                    post.vigparam -= 0.061f * 2;
                    Debug.Log("回復ストック" + player.healCount);
                    player.GetComponent<AudioSource>().PlayOneShot(useSE);
                    player.healCount -= 1;
                    player.hp += heal;
            }
            
        }
    }
}
