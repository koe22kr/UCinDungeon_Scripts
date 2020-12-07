using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDEnemyFSM : MonoBehaviour
{
   
    public ge.ActionType currentState;
    public UDCharacterAttackComponent attackComponent;
    public UDCharacterTraceComponent traceComponent;
    

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = ge.ActionType.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckFSM();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Action();
        }

    }
    
    private void CheckFSM()
    {
        //현 fsm의  상태가 3가지 이고 모든 상태들이 상호 전환 가능해서 if elseif else 로도 끝낼 수 있지만 추가될 것을 고려해서 이렇게 배치.
        switch (currentState)
        {
            case ge.ActionType.IDLE:
                {
                    if (attackComponent.Check())
                    {
                        currentState = ge.ActionType.ATTACK;
                    }
                    else if (traceComponent.Check())
                    {
                        currentState = ge.ActionType.TRACE;
                    }
                    else
                    {
                        currentState = ge.ActionType.IDLE;
                    }
                }
                break;
            case ge.ActionType.TRACE:
                {
                    if (attackComponent.Check())
                    {
                        currentState = ge.ActionType.ATTACK;
                    }
                    else if (traceComponent.Check())
                    {
                        currentState = ge.ActionType.TRACE;
                    }
                    else 
                    {
                        currentState = ge.ActionType.IDLE;
                    }
                }
                break;
            case ge.ActionType.ATTACK:
                {
                    if (attackComponent.Check())
                    {
                        currentState = ge.ActionType.ATTACK;
                    }
                    else if (traceComponent.Check())
                    {
                        currentState = ge.ActionType.TRACE;
                    }
                    else
                    {
                        currentState = ge.ActionType.IDLE;
                    }
                }
                break;
            default:
                {
                    Debug.Log("UDEnemyFSM. currentState is wrong");
                }
                break;
        }
        Debug.Log("EnemyFSM.CheckFSM.currentState : " + currentState.ToString());
    }

    private void Action()
    {
        switch (currentState)
        {
            case ge.ActionType.IDLE:
                break;
            case ge.ActionType.ATTACK:
                {
                    attackComponent.Action();
                }
                break;
            case ge.ActionType.MINING:
                break;
            case ge.ActionType.TRACE:
                {
                    traceComponent.Action();
                }
                break;
            default:
                break;
        }
        Debug.Log("EnemyFSM.Action.currentState : " + currentState.ToString());
    }
}
