using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDCharacterActionHandler : MonoBehaviour
{
    public ge.ObjectType objectType = ge.ObjectType.PLAYER;
    private UDGameboard gameBoard;
    //IAction
    public UDCharacterAttackComponent normalAttackComponent;
    public UDCharacterMoveComponent moveComponent;
    public UDCharacterRotationComponent rotationComponent;

    bool isOnTurn = true;
    // Start is called before the first frame update
    void Start()
    {
        gameBoard = FindObjectOfType<UDGameboard>();
        UDEventManager.playerActionDelegate += PlayerAction;
        UDEventManager.actionHandlerStartDelegate += SetTurnOn;
        UDEventManager.actionHandlerFinishDelegate += SetTurnOff;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SetTurnOn()
    {
        isOnTurn = true;
    }
    private void SetTurnOff()
    {
        isOnTurn = false;
    }

    private void PlayerAction(Vector3 dir)
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = pos + dir;
        UDObject targetobject = gameBoard.GetObject(targetPos.x, targetPos.z);

        if (!isOnTurn)
        {
            return;
        }

        //rotate always
        rotationComponent.Action(dir);
        switch (targetobject.type)
        {
            case ge.ObjectType.NONE:
                {
                    //move
                    if (moveComponent != null)
                    {
                        if (moveComponent.Check(dir))
                        {
                            gameBoard.Move(pos.x, pos.z, targetPos.x, targetPos.z);
                            moveComponent.Action();
                            UDEventManager.actionHandlerFinishDelegate.Invoke();
                        }
                    }
                }
                break;
            case ge.ObjectType.WALL:
                {
                    //notting? dont mining ?
                }
                break;
            case ge.ObjectType.BLOCK:
                {
                    //mining
                    //if (miningComponent != null)
                    //{
                    //    miningComponent.Action(dir);
                    //}

                }
                break;
            case ge.ObjectType.ENEMY:
                {
                    //attack
                    if (normalAttackComponent != null)
                    {
                        if (normalAttackComponent.Check())
                        {
                            normalAttackComponent.Action();
                            UDEventManager.actionHandlerFinishDelegate.Invoke();
                        }
                    }
                }
                break;
            case ge.ObjectType.PLAYER:
                {
                    //to multiplay?

                }
                break;
            default:
                break;
        }
    }
}
