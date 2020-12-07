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

    // Start is called before the first frame update
    void Start()
    {
        gameBoard = FindObjectOfType<UDGameboard>();
        UDEventManager.moveActionDelegate += CheckMoveAction;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CheckMoveAction(Vector3 dir)
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = pos + dir;
        UDObject targetobject = gameBoard.GetObject(targetPos.x, targetPos.z);

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
