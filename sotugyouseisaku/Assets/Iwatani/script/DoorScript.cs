using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

    //　ドアエリアに入っているかどうか
    private bool isNear;
    //　ドアのアニメーター
    private Animator animator;

    void Start()
    {
        isNear = false;
        animator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("joystick B") && isNear)
        {
            Debug.Log("open");
            animator.SetBool("Open", !animator.GetBool("Open"));
        }

        Debug.Log(isNear);
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
}
