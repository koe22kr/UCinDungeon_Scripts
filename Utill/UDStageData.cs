using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDStageData
{
    public const int HEADER_COUNT = 3;
    public const int EXIT_COUNT = 4;
    public int stageNum;
    public int blockCount;
    public int enemyCount;
    public List<Vector2Int> blocksPos;
    public List<Vector2Int> enemysPos;
    public List<int> exitPosXorZ;
    public UDStageData()
    {
        stageNum = 0;
        blockCount = 0;
        enemyCount = 0;
        blocksPos = new List<Vector2Int>();
        enemysPos = new List<Vector2Int>();

        exitPosXorZ = new List<int>();

    }
    public UDStageData(UDStageData copySrc)
    {
        stageNum = copySrc.stageNum;
        blockCount = copySrc.blockCount;
        enemyCount = copySrc.enemyCount;
        blocksPos = new List<Vector2Int>(copySrc.blocksPos);
        enemysPos = new List<Vector2Int>(copySrc.enemysPos);

        exitPosXorZ = new List<int>(copySrc.exitPosXorZ);

    }
    public void Reset()
    {
        stageNum = 0;
        blockCount = 0;
        enemyCount = 0;
        blocksPos.Clear();
        enemysPos.Clear();
        exitPosXorZ.Clear();

    }
}
