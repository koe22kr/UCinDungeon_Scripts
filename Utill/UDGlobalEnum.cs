using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ge
{
    public enum ObjectType
    {
        NONE,
        WALL,
        BLOCK,

        DAMAGEABLE,
        ENEMY,
        PLAYER,
        NUM,
    }

    public enum ActionType
    {
        NONE = 0,
        IDLE,
        ATTACK,
        MINING,
        MOVE,
        ROTATION,
        TRACE,
        NUM,
    };

    //public enum EnemyState 
    //{
    //    IDLE = 0,
    //    TRACE,
    //    ATTACK,
    //    NUM,
    //
    //}
}
