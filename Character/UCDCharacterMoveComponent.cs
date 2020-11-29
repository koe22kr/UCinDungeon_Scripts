using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDCharacterMoveComponent : MonoBehaviour
{
    private UCDInputManager inputManager;
    private bool isMoving;
    private Vector3 movingDir = Vector3.zero;
    private Vector3 nextMovingDir = Vector3.zero;
    public float moveSpeed = 1;
    public float motionLengthSecond = 0.8f;
    private float elapsedTime;

    //
    public float inputDisableTime = 0.5f;
    private float lastInputTime = 0;
    //
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.Find("GameManager").GetComponent<UCDInputManager>();
        UCDEventManager.moveDelegate += SetMove;

    }

    // Update is called once per frame
    
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float speedPerSec = moveSpeed / motionLengthSecond;

            transform.position += movingDir * (Time.deltaTime * speedPerSec);
            transform.rotation = Quaternion.LookRotation(movingDir);

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
    public void SetMove(Vector3 moveDir)
    {
        if (moveDir == Vector3.zero ||  Time.time < lastInputTime + inputDisableTime)
        {
            return;
        }
        if (isMoving)
        {
            nextMovingDir = moveDir;
            return;
        }
        
        isMoving = true;
        movingDir = moveDir;
        lastInputTime = Time.time;
        return;
    }
    public void MoveIsDone()
    {
        UCDEventManager.moveEndDelegate.Invoke();
    }
}
