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
        ENEMY,
        PLAYER,
        NUM,
    }

    public enum ActionType
    {
        NONE = 0,
        ATTACK,
        MINING,
        MOVE,
        ROTATION,
    };
}
