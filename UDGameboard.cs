using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDGameboard : MonoBehaviour
{

    public int gameBoardWidth = 10;
    public int gameBoardHeight = 10;
    private ge.ObjectType[,] gameBoard;

    // Start is called before the first frame update
    void Start()
    {
        UDEventManager.boardSetObjectDelegate += SetObject;

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
    private ref ge.ObjectType GetObjectRef(int posX, int posZ)
    {
        int idxX = posX - 1;
        int idxZ = posZ - 1;
        return ref gameBoard[idxZ, idxX];
    }


    private void ResetObject(int posX, int posZ)
    {
        if (CheckGameBoard(posX,posZ))
        {
            GetObjectRef(posX,posZ) = ge.ObjectType.NONE;
        }
    }
    private void ResetObject(float posX, float posZ)
    {
        int intPosX = (int)posX;
        int intposZ = (int)posZ;
        if (CheckGameBoard(intPosX, intposZ))
        {
            GetObjectRef(intPosX, intposZ) = ge.ObjectType.NONE;
        }
    }

    private void SetObject(int posX, int posZ, ge.ObjectType type)
    {
        if (CheckGameBoard(posX, posZ))
        {
            GetObjectRef(posX, posZ) = type;
        }
    }
    private void SetObject(float posX, float posZ, ge.ObjectType type)
    {
        int intPosX = (int)posX;
        int intposZ = (int)posZ;
        if (CheckGameBoard(intPosX, intposZ))
        {
            GetObjectRef(intPosX, intposZ) = type;
        }
    }


    public ge.ObjectType GetObjectType(int posX,int posZ)
    {
        if (CheckGameBoard(posX, posZ))
        {
            return GetObjectRef(posX, posZ);
        }
        return ge.ObjectType.NONE;
    }

    public ge.ObjectType GetObjectType(float posX, float posZ)
    {
        int intPosX = (int)posX;
        int intposZ = (int)posZ;
        if (CheckGameBoard(intPosX, intposZ))
        {
            return GetObjectRef(intPosX, intposZ);
        }
        return ge.ObjectType.NONE;
    }

    private void ResetBoard()
    {
        for (int y = 0; y < gameBoardHeight; y++)
        {
            for (int x = 0; x < gameBoardWidth; x++)
            {
                gameBoard[y, x] = ge.ObjectType.NONE;
            }
        }
    }

    private void ResetBoard(int width, int height)
    {
        gameBoardWidth = width;
        gameBoardHeight = height;
        gameBoard = new ge.ObjectType[gameBoardWidth, gameBoardHeight];
        ResetBoard();
    }

    public void Move(Vector3 ownerPos, Vector3 targetPos, ge.ObjectType type)
    {
        ResetObject(ownerPos.x, ownerPos.z);
        SetObject(targetPos.x, targetPos.z, type);
    }
}
