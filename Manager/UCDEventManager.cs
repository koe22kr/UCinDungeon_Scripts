using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UCDEventManager : MonoBehaviour
{

    public delegate void MoveDelegate(Vector3 vec3);
    static public MoveDelegate moveDelegate;

    public delegate void VoidDelegate();
    static public VoidDelegate moveStartDelegate;
    static public VoidDelegate moveEndDelegate;


    //call when editor color change
    static public VoidDelegate blockSetColorDelegate;
}
