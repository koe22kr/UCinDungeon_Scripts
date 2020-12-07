using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UDObject
{
    public ge.ObjectType type;
    public GameObject go;
    public UDCharacterInfo info;
    public UDObject(ge.ObjectType inType, GameObject inGo)
    {
        type = inType;
        go = inGo;
        info = inGo.GetComponent<UDCharacterInfo>();
    }
    public void Reset()
    {
        type = ge.ObjectType.NONE;
        go = null;
        info = null;
    }

}

public class UDGameboard : MonoBehaviour
{

    public int gameBoardWidth = 10;
    public int gameBoardHeight = 10;
    public UDObject[,] gameBoard;

    // Start is called before the first frame update
    void Start()
    {
        UDEventManager.boardSetObjectDelegate += SetObject;
        UDEventManager.characterDeadDelegate += ResetObject;
        ResetBoard(gameBoardWidth, gameBoardHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool CheckGameBoard(int posX, int posZ)
    {
        if (gameBoardWidth < posX || gameBoardHeight < posZ)
        {
            return false;
        }
        return true;
    }
    private ref UDObject GetObjectRef(int posX, int posZ)
    {
        int idxX = posX - 1;
        int idxZ = posZ - 1;
        return ref gameBoard[idxZ, idxX];
    }


    private void ResetObject(int posX, int posZ)
    {
        if (CheckGameBoard(posX,posZ))
        {
            GetObjectRef(posX, posZ).Reset();
        }
    }
    private void ResetObject(float posX, float posZ)
    {
        int intPosX = (int)posX;
        int intposZ = (int)posZ;
        if (CheckGameBoard(intPosX, intposZ))
        {
            GetObjectRef(intPosX, intposZ).Reset();
        }
    }

    private void Swap(int lhsX, int lhsZ, int rhsX, int rhsZ)
    {
        if (CheckGameBoard(lhsX, lhsZ) && CheckGameBoard(rhsX, rhsZ))
        {
            UDObject temp = GetObjectRef(lhsX, lhsZ);
            GetObjectRef(lhsX, lhsZ) = GetObjectRef(rhsX, rhsZ);
            GetObjectRef(rhsX, rhsZ) = temp;
        }
       
    }
    private void Swap(float lhsX, float lhsZ, float rhsX, float rhsZ)
    {
        Swap((int)lhsX, (int)lhsZ, (int)rhsX, (int)rhsZ);
    }
    private void SetObject(int posX, int posZ, ge.ObjectType type, GameObject go)
    {
        if (CheckGameBoard(posX, posZ))
        {
            GetObjectRef(posX, posZ) = new UDObject(type, go);
        }
    }
    private void SetObject(float posX, float posZ, ge.ObjectType type, GameObject go)
    {
        SetObject((int)posX, (int)posZ, type, go);
    }


    public UDObject GetObject(int posX,int posZ)
    {
        if (CheckGameBoard(posX, posZ))
        {
            return GetObjectRef(posX, posZ);
        }
        return new UDObject();
    }

    public UDObject GetObject(float posX, float posZ)
    {
        return GetObject((int)posX, (int)posZ);
    }

    private void ResetBoard()
    {
        for (int z = 0; z < gameBoardHeight; z++)
        {
            for (int x = 0; x < gameBoardWidth; x++)
            {
                gameBoard[z, x].Reset();
            }
        }
    }

    private void ResetBoard(int width, int height)
    {
        gameBoardWidth = width;
        gameBoardHeight = height;
        gameBoard = new UDObject[gameBoardWidth, gameBoardHeight];
        ResetBoard();
    }

    public void Move(float currentX, float currentZ, float targetX, float targetZ)
    {
        Swap(currentX, currentZ, targetX, targetZ);
    }
    public void Move(int currentX, int currentZ, int targetX, int targetZ)
    {
        Swap(currentX, currentZ, targetX, targetZ);
    }

    public void Attack(float ownerX, float ownerZ, float targetX, float targetZ)
    {
        int ownerAttack = GetObject(ownerX, ownerZ).info.attack;
        GetObject(targetX, targetZ).info.TakeDamage(ownerAttack);
    }

    public bool IsDamageAble(float posX, float posZ)
    {
        ge.ObjectType targetType = GetObject(posX, posZ).type;
        if (ge.ObjectType.DAMAGEABLE < targetType
            && targetType < ge.ObjectType.NUM)
        {
            return true;
        }
        return false;
    }
    public bool IsPlayer(float posX, float posZ)
    {
        ge.ObjectType targetType = GetObject(posX, posZ).type;
        if (ge.ObjectType.PLAYER == targetType)            
        {
            return true;
        }
        return false;
    }
    public bool IsNotWallAndBlock(float posX, float posZ)
    {
        ge.ObjectType targetType = GetObject(posX, posZ).type;
        if (ge.ObjectType.WALL == targetType
            ||ge.ObjectType.BLOCK == targetType)
        {
            return false;
        }
        return true;
    }
}
