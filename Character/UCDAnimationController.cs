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
        animator.SetBool("isMove", isMove);
    }
    void SetMoveTrue()
    {
        isMove = true;
        animator.SetBool("isMove", isMove);
    }
    void SetMoveFalse()
    {
        isMove = false;
        animator.SetBool("isMove", isMove);
    }
}
