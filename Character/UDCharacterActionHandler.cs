using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDCharacterActionHandler : MonoBehaviour
{
    public ge.ObjectType objectType = ge.ObjectType.PLAYER;
    private UDGameboard gameBoard;
    //IAction
    private UDActionComponent normalAttackComponent;
    private UDActionComponent miningComponent;
    private UDActionComponent moveComponent;
    private UDActionComponent rotationComponent;

    // Start is called before the first frame update
    void Start()
    {
        SettingComponent();
        gameBoard = FindObjectOfType<UDGameboard>();
        UDEventManager.moveActionDelegate += CheckMoveAction;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SettingComponent()
    {
        UDActionComponent[] components = GetComponents<UDActionComponent>();
        foreach (var item in components)
        {
            switch (item.actionType)
            {
                case ge.ActionType.ATTACK:
                    {
                        normalAttackComponent = item;
                    }
                    break;
                case ge.ActionType.MINING:
                    {
                        miningComponent = item;
                    }
                    break;
                case ge.ActionType.MOVE:
                    {
                        moveComponent = item;
                    }
                    break;
                case ge.ActionType.ROTATION:
                    {
                        rotationComponent = item;
                    }
                    break;
                default:
                    break;
            }
        }
    }
    private void CheckMoveAction(Vector3 dir)
    {
        Vector3 targetPos = this.transform.position + dir;
        ge.ObjectType targetType = gameBoard.GetObjectType(targetPos.x, targetPos.z);

        //rotate always
        rotationComponent.Action(dir);
        switch (targetType)
        {
            case ge.ObjectType.NONE:
                {
                    //move
                    if (moveComponent != null)
                    {
                        moveComponent.Action(dir);
                        gameBoard.Move(transform.position, targetPos, objectType);
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
                    if (miningComponent != null)
                    {
                        miningComponent.Action(dir);
                    }
                }
                break;
            case ge.ObjectType.ENEMY:
                {
                    //attack
                    if (normalAttackComponent != null)
                    {
                        normalAttackComponent.Action(dir);
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
