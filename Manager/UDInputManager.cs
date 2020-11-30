using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UDInputManager : MonoBehaviour
{
    UDEventManager eventManager;

    [HideInInspector]
    public Vector3 moveDir;


    // Start is called before the first frame update
    void Start()
    {
        eventManager = GetComponent<UDEventManager>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
       
    }
    void Move()
    {

        if (Input.GetButton("Vertical"))
        {
            if (0 < Input.GetAxis("Vertical"))
            {
                moveDir = Vector3.forward;
                CallMoveDelegate();
            }
            else
            {
                moveDir = Vector3.back;
                CallMoveDelegate();
            }
        }
        else if (Input.GetButton("Horizontal"))
        {
            if (0 < Input.GetAxis("Horizontal"))
            {
                moveDir = Vector3.right;
                CallMoveDelegate();
            }
            else
            {
                moveDir = Vector3.left;
                CallMoveDelegate();
            }
        }
        //else
        //{
        //    moveDir = Vector3.zero;
        //}
    }
    void CallMoveDelegate()
    {

        UDEventManager.moveDelegate.Invoke(moveDir);
        UDEventManager.moveStartDelegate.Invoke();
    }
    
}
