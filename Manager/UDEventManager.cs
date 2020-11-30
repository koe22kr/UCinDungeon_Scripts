using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UDEventManager : MonoBehaviour
{

    public delegate void MoveDelegate(Vector3 vec3);
    static public MoveDelegate moveDelegate;

    public delegate void VoidDelegate();
    static public VoidDelegate moveStartDelegate;
    static public VoidDelegate moveEndDelegate;


    //
    static public VoidDelegate preSettingBlocksDelegate;
    static public VoidDelegate postSettingBlocksDelegate;
    static public VoidDelegate blockSettingCompleteDelegate;

    static public VoidDelegate settingPlayerDelegate;
    static public VoidDelegate playerSettingCompleteDelegate;

    static public VoidDelegate settingEnemyDelegate;
    static public VoidDelegate enemySettingCompleteDelegate;

    static public VoidDelegate startGameDelegate;
    //
    public delegate void BoardSetObjectDelegate(int posX, int posZ, ge.ObjectType type);
    static public BoardSetObjectDelegate boardSetObjectDelegate;

    //
    

}
