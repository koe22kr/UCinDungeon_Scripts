using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDGameboard : MonoBehaviour
{
    public TextAsset stageDataCSV;

    public int gameBoardWidth = 10;
    public int gameBoardHeight = 10;
    private ge.ObjectType[,] gameBoard;


    // Start is called before the first frame update
    void Start()
    {
        UCDEventManager.boardSetObjectDelegate += SetObject;

        ResetBoard(gameBoardWidth, gameBoardHeight);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private bool CheckGameBoard(int posX, int posY)
    {
        if (gameBoardWidth < posX || gameBoardHeight < posY)
        {
            return false;
        }
        return true;
    }
    private ref ge.ObjectType GetObjectRef(int posX, int posY)
    {
        int idxX = posX - 1;
        int idxY = posY - 1;
        return ref gameBoard[idxY, idxX];
    }
    

    public void ResetObject(int posX, int posY)
    {
        if (CheckGameBoard(posX,posY))
        {
            GetObjectRef(posX,posY) = ge.ObjectType.NONE;
        }
    }
       
    public void SetObject(int posX, int posY, ge.ObjectType type)
    {
        if (CheckGameBoard(posX, posY))
        {
            GetObjectRef(posX, posY) = type;
        }
    }


    public ge.ObjectType GetObject(int posX,int posY)
    {
        if (CheckGameBoard(posX, posY))
        {
            return GetObjectRef(posX, posY);
        }
        return ge.ObjectType.NONE;
    }

    public void ResetBoard()
    {
        for (int y = 0; y < gameBoardHeight; y++)
        {
            for (int x = 0; x < gameBoardWidth; x++)
            {
                gameBoard[y, x] = ge.ObjectType.NONE;
            }
        }
    }

    public void ResetBoard(int width, int height)
    {
        gameBoardWidth = width;
        gameBoardHeight = height;
        gameBoard = new ge.ObjectType[gameBoardWidth, gameBoardHeight];
        ResetBoard();
    }
}
