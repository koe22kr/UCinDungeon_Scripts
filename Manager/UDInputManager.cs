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
        MoveInput();
       
    }
    void MoveInput()
    {

        if (Input.GetButton("Vertical"))
        {
            if (0 < Input.GetAxis("Vertical"))
            {
                moveDir = Vector3.forward;
                CallMoveInputDelegate();
            }
            else
            {
                moveDir = Vector3.back;
                CallMoveInputDelegate();
            }
        }
        else if (Input.GetButton("Horizontal"))
        {
            if (0 < Input.GetAxis("Horizontal"))
            {
                moveDir = Vector3.right;
                CallMoveInputDelegate();
            }
            else
            {
                moveDir = Vector3.left;
                CallMoveInputDelegate();
            }
        }
        //else
        //{
        //    moveDir = Vector3.zero;
        //}
    }
    void CallMoveInputDelegate()
    {

        UDEventManager.moveActionDelegate.Invoke(moveDir);
    }
    
}
