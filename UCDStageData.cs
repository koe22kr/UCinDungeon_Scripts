using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDStageData
{
    public const int HEADER_COUNT = 2;
    public const int EXIT_COUNT = 4;
    public int stageNum;
    public int blockCount;
    public List<Vector2Int> blocksPos;
    public List<int> exitPosXorZ;
    public UCDStageData()
    {
        stageNum = 0;
        blockCount = 0;
        blocksPos = new List<Vector2Int>();
        exitPosXorZ = new List<int>();

    }
    public UCDStageData(UCDStageData copySrc)
    {
        stageNum = copySrc.stageNum;
        blockCount = copySrc.blockCount;
        blocksPos = new List<Vector2Int>(copySrc.blocksPos);
        exitPosXorZ = new List<int>(copySrc.exitPosXorZ);

    }
    public void Reset()
    {
        stageNum = 0;
        blockCount = 0;
        blocksPos.Clear();
        exitPosXorZ.Clear();

    }
}
