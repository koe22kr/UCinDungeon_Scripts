using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UDInputManager : MonoBehaviour
{
    private bool canInput = true;

    // Start is called before the first frame update
    void Start()
    {
        UDEventManager.OnInputDelegate += OnInput;
        UDEventManager.OffInputDelegate += OffInput;
    }
    // Update is called once per frame
    void Update()
    {
        MoveInput();
    }
    
    private void OnInput()
    {
        canInput = true;
    }
    private void OffInput()
    {
        canInput = false;   
    }

    void MoveInput()
    {
        if (!canInput)
        {
            return;
        }
        Vector3 moveDir;

        if (Input.GetButtonDown("Vertical"))
        {
            if (0 < Input.GetAxis("Vertical"))
            {
                moveDir = Vector3.forward;
                CallMoveInputDelegate(moveDir);
            }
            else
            {
                moveDir = Vector3.back;
                CallMoveInputDelegate(moveDir);
            }
        }
        else if (Input.GetButtonDown("Horizontal"))
        {
            if (0 < Input.GetAxis("Horizontal"))
            {
                moveDir = Vector3.right;
                CallMoveInputDelegate(moveDir);
            }
            else
            {
                moveDir = Vector3.left;
                CallMoveInputDelegate(moveDir);
            }
        }
    }
    void CallMoveInputDelegate(Vector3 moveDir)
    {
        UDEventManager.playerActionDelegate.Invoke(moveDir);
    }
    
}
