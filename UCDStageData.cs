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
        this.stageNum = 0;
        this.blockCount = 0;
        this.blocksPos = new List<Vector2Int>();
        this.exitPosXorZ = new List<int>();

    }
    public UCDStageData(UCDStageData copySrc)
    {
        this.stageNum = copySrc.stageNum;
        this.blockCount = copySrc.blockCount;
        this.blocksPos = new List<Vector2Int>(copySrc.blocksPos);
        this.exitPosXorZ = new List<int>(copySrc.exitPosXorZ);

    }
    public void Reset()
    {
        this.stageNum = 0;
        this.blockCount = 0;
        this.blocksPos.Clear();
        this.exitPosXorZ.Clear();

    }
}
