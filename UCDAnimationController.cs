using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        UCDEventManager.moveStartDelegate += SetMoveTrue;
        UCDEventManager.moveEndDelegate += SetMoveFalse;
    }
    // Update is called once per frame
    void Update()
    {
        this.animator.SetBool("isMove", this.isMove);
    }
    void SetMoveTrue()
    {
        this.isMove = true;
        this.animator.SetBool("isMove", this.isMove);
    }
    void SetMoveFalse()
    {
        this.isMove = false;
        this.animator.SetBool("isMove", this.isMove);
    }
}
