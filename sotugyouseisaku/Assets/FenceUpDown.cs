using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceUpDown : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DownFence()
    {
        animator.SetBool("IsDown", true);
    }

    public void UpFence()
    {
        animator.SetBool("IsUp", true);
    }
}
