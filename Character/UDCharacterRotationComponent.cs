using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDCharacterRotationComponent : UDActionComponent
{

    private void Awake()
    {
        actionType = ge.ActionType.ROTATION;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Action(Vector3 dir)
    {
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
