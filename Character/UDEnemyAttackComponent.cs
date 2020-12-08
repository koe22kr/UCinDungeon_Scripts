using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDEnemyAttackComponent : UDActionComponent
{
    private UDGameboard gameBoard;
    public int attackRange = 2;
    public int attackWidthInit = 1;
    public int attackWidthByRange = 0;
    private List<Vector2> targets;
    // Start is called before the first frame update
    private void Awake()
    {
        targets = new List<Vector2>();
        this.actionType = ge.ActionType.TRACE;
        gameBoard = FindObjectOfType<UDGameboard>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Action()
    {
        Debug.Log("FSM_Attack_Action");

        Vector3 pos = transform.position;
        foreach (var item in targets)
        {
            gameBoard.Attack(pos.x, pos.z, item.x, item.y);
        }
    }

    public bool Check()
    {
        Debug.Log("FSM_Attack_Check");
        Vector3 pos = transform.position;
        Vector3 forward = transform.rotation * Vector3.forward;
        Vector3 right = transform.rotation * Vector3.right;
        Vector3 left = transform.rotation * Vector3.left;
        Vector3 back = transform.rotation * Vector3.back;
        forward.y = 0;

        if (CheckForward(pos, forward, right))
        {
            return true;
        }
        else if (CheckForward(pos, right, back))
        {
            return true;
        }
        else if (CheckForward(pos, back, left))
        {
            return true;
        }
        else if (CheckForward(pos, left, forward))
        {
            return true;
        }

        return false;
    }
    private bool CheckForward(Vector3 pos, Vector3 forward,Vector3 right)
    {
        targets.Clear();
        Vector3 StartPos = pos + forward;
        for (int z = 0; z < attackRange; z++)
        {
            int offsetStartToZero = -1;
            int offset = (int)(attackWidthByRange * z);
            int halfWidth = attackWidthInit + offsetStartToZero + offset;
            if (halfWidth < 0)
            {
                //do notthing
            }
            else
            {
                int minWidthPos = -halfWidth;
                int maxWidthPos = halfWidth;
                for (int x = minWidthPos; x <= maxWidthPos; x++)
                {
                    Vector3 attackPos = StartPos + (forward * z) + (right * x);
                    UDObject target = gameBoard.GetObject(attackPos.x, attackPos.z);

                    if (gameBoard.IsDamageAble(attackPos.x, attackPos.z))
                    {
                        //공 격
                        targets.Add(new Vector2(attackPos.x, attackPos.z));
                        if (gameBoard.IsPlayer(attackPos.x, attackPos.z))
                        {
                            transform.rotation = Quaternion.LookRotation(forward);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
