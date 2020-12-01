using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UDActionComponent : MonoBehaviour
{
    public ge.ActionType actionType
    {
        get; set;
    }
    public abstract void Action(Vector3 dir);
}
