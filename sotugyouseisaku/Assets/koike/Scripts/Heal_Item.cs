using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Item : MonoBehaviour
{

    [SerializeField]
    private int heal = 20;

    public Player player;

    public postEffect post;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if(player.hp < 100)
            {
                player.hp += heal;
                post.vigparam -= 0.061f * 2;
                Destroy(gameObject);           
            }           
        }
    }
}
