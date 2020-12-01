using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDCharacterMoveComponent : UDActionComponent
{

    private bool isMoving;
    private Vector3 movingDir = Vector3.zero;
    private Vector3 nextMovingDir = Vector3.zero;
    public float moveSpeed = 1;
    public float motionLengthSecond = 0.2f;
    private float elapsedTime;

    //
    public float inputDisableTime = 0.3f;
    private float lastInputTime = 0;
    //

    private void Awake()
    {
        actionType = ge.ActionType.MOVE;
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float speedPerSec = moveSpeed / motionLengthSecond;

            transform.position += movingDir * (Time.deltaTime * speedPerSec);
            

            if (motionLengthSecond < elapsedTime)
            {
                Vector3 pos = new Vector3();
                pos.x = Mathf.Round(transform.position.x);
                pos.z = Mathf.Round(transform.position.z);
                pos.y = transform.position.y;
                transform.position = pos;
                
                if (nextMovingDir == Vector3.zero)
                {
                    isMoving = false;
                    MoveIsDone();
                }
                else
                {
                    movingDir = nextMovingDir;
                    nextMovingDir = Vector3.zero;
                }
                elapsedTime = 0f;
            }
        }
    }

    private void MoveIsDone()
    {
        UDEventManager.moveEndDelegate.Invoke();
    }

    public override void Action(Vector3 dir)
    {

        if (dir == Vector3.zero || Time.time < lastInputTime + inputDisableTime)
        {
            return;
        }

        if (isMoving)
        {
            nextMovingDir = dir;
            return;
        }

        isMoving = true;
        movingDir = dir;
        lastInputTime = Time.time;
        UDEventManager.moveStartDelegate.Invoke();
        return;
    }
}
