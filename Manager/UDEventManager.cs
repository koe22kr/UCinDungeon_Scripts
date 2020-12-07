using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UDEventManager : MonoBehaviour
{
    public delegate void VoidDelegate();

    //ActionComponent delegate
    public delegate void ActionDelegate(Vector3 vec3);
    static public ActionDelegate moveActionDelegate;
    //AnimationController delegate
    static public VoidDelegate moveStartDelegate;
    static public VoidDelegate moveEndDelegate;

    //BlockManager delegate
    static public VoidDelegate preSettingBlocksDelegate;
    static public VoidDelegate postSettingBlocksDelegate;
    static public VoidDelegate blockSettingCompleteDelegate;
    //PlayerSpawner delegate
    static public VoidDelegate settingPlayerDelegate;
    static public VoidDelegate playerSettingCompleteDelegate;
    //EnemySpawner delegate
    static public VoidDelegate settingEnemyDelegate;
    static public VoidDelegate enemySettingCompleteDelegate;

    //GameBoard delegate
    public delegate void BoardSetObjectDelegate(int posX, int posZ, ge.ObjectType type, GameObject go);
    static public BoardSetObjectDelegate boardSetObjectDelegate;

    public delegate void XZDelegate(int posX, int posZ);
    static public XZDelegate characterDeadDelegate;
    //TO DO
    static public VoidDelegate startGameDelegate;
}
