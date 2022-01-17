using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

    //　ドアエリアに入っているかどうか
    private bool isNear;
    //　ドアのアニメーター
    private Animator animator;

    [SerializeField]
    private AudioClip openSE;
    [SerializeField]
    private AudioClip closeSE;
    private new AudioSource audio;

    float count = 0;
    void Start()
    {
        isNear = false;
        animator = transform.parent.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("joystick B") && isNear)
        {
            //Debug.Log("open");
            //Debug.Log(animator.GetBool("Open"));
            animator.SetBool("Open", !animator.GetBool("Open"));
            IsPlaySE(!animator.GetBool("Open"));
        }

        //Debug.Log(count);
        if(animator.GetBool("Open"))
        {
            count += Time.deltaTime;
        }
        
        if (count >= 3) 
        {
            animator.SetBool("Open", false);
            count = 0;
        }

        
        //Debug.Log(isNear);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            isNear = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            isNear = false;
            //animator.SetBool("Open", false);
        }
    }

    void IsPlaySE(bool se)
    {
        if(se)
        {
            audio.PlayOneShot(openSE);
        }
        else if(!se)
        {
            audio.PlayOneShot(closeSE);
        }
    }
}
