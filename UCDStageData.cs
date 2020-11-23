using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDStageData
{
    public const int HEADER_COUNT = 2;
    public int stageNum;
    public int blockCount;
    public List<Vector2> blocksPos;
    public UCDStageData()
    {
        this.stageNum = 0;
        this.blockCount = 0;
        this.blocksPos = new List<Vector2>();
    }
    public UCDStageData(UCDStageData copySrc)
    {
        this.stageNum = copySrc.stageNum;
        this.blockCount = copySrc.blockCount;
        this.blocksPos = new List<Vector2>(copySrc.blocksPos);
    }
    public void Clear()
    {
        this.stageNum = 0;
        this.blockCount = 0;
        this.blocksPos.Clear();
    }
}
