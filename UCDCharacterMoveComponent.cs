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
        this.inputManager = GameObject.Find("GameManager").GetComponent<UCDInputManager>();
        UCDEventManager.moveDelegate += SetMove;

    }

    // Update is called once per frame
    
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (this.isMoving)
        {
            this.elapsedTime += Time.deltaTime;
            float speedPerSec = this.moveSpeed / this.motionLengthSecond;

            this.transform.position += this.movingDir * (Time.deltaTime * speedPerSec);
            this.transform.rotation = Quaternion.LookRotation(this.movingDir);

            if (this.motionLengthSecond < this.elapsedTime)
            {
                Vector3 pos = new Vector3();
                pos.x = Mathf.Round(this.transform.position.x);
                pos.z = Mathf.Round(this.transform.position.z);
                pos.y = this.transform.position.y;
                this.transform.position = pos;
                
                if (this.nextMovingDir == Vector3.zero)
                {
                    this.isMoving = false;
                    MoveIsDone();
                }
                else
                {
                    this.movingDir = this.nextMovingDir;
                    this.nextMovingDir = Vector3.zero;
                }
                this.elapsedTime = 0f;
            }
        }
    }
    public void SetMove(Vector3 moveDir)
    {
        if (moveDir == Vector3.zero ||  Time.time < this.lastInputTime + this.inputDisableTime)
        {
            return;
        }
        if (this.isMoving)
        {
            this.nextMovingDir = moveDir;
            return;
        }
        
        this.isMoving = true;
        this.movingDir = moveDir;
        this.lastInputTime = Time.time;
        return;
    }
    public void MoveIsDone()
    {
        UCDEventManager.moveEndDelegate.Invoke();
    }
}
