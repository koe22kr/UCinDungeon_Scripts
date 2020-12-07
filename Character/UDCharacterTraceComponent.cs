using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDCharacterTraceComponent : UDActionComponent
{
    private UDGameboard gameBoard;

    public int searchLength = 3;
    public int searchWidthInit = 1;
    public float searchWidthByRange = 1f;
    public Vector3 tracePos = Vector3.zero;
    private void Awake()
    {
        actionType = ge.ActionType.TRACE;
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
        Debug.Log("FSM_Trace_Action");
        //이동

        Vector3 relativePos = tracePos - transform.position;

        if (0.5f < relativePos.magnitude)
        {
            Vector3 roundedRelative = new Vector3(Mathf.Round(relativePos.x), 0, Mathf.Round(relativePos.z));
            Vector3 largeer = Mathf.Abs(relativePos.x) < Mathf.Abs(relativePos.z) ? new Vector3(0, 0, relativePos.z) : new Vector3(relativePos.x, 0, 0);
            largeer.Normalize();
            //transform.Translate(largeer);
            //if MoveAble
            gameBoard.Move(transform.position.x, transform.position.z, transform.position.x + largeer.x, transform.position.z + largeer.z);
            transform.position += largeer;

        }

    }
    private bool IsTraceTarget(UDObject obj)
    {
        if (obj.type == ge.ObjectType.PLAYER)
        {
            return true;
        }
        return false;
    }
    public bool Check()
    {
        Debug.Log("FSM_Trace_Check");

        Vector3 pos = transform.position;
        Vector3 forward = transform.rotation * Vector3.forward;
        Vector3 right = transform.rotation * Vector3.right;
        forward.y = 0;

        Vector3 searchStartPos = pos + forward;
        for (int z = 0; z < searchLength; z++)
        {
            int offsetStartToZero = -1;
            int offset = (int)(searchWidthByRange * z);
            int halfWidth = searchWidthInit + offsetStartToZero + offset;
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
                    Vector3 searchPos = searchStartPos + (forward * z) + (right * x);

                    if (gameBoard.IsNotWallAndBlock(searchPos.x, searchPos.z))
                    {



                        UDObject obj = gameBoard.GetObject(searchPos.x, searchPos.z);

                        if (IsTraceTarget(obj))
                        {
                            tracePos = searchPos;
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
