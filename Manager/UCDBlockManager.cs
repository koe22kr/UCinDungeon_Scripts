using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDBlockManager : MonoBehaviour
{
    public const int MAX_BLOCK_NUMBER = 30;
    public GameObject blockPrefab;
    public List<UCDBlock> blocks;
    public TextAsset stageDataCSV;
    private List<UCDStageData> stageData;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MAX_BLOCK_NUMBER; i++)
        {
            UCDBlock temp = GameObject.Instantiate(this.blockPrefab).GetComponent<UCDBlock>();

            if (temp != null)
            {
                temp.ResetBlock();
                this.blocks.Add(temp);
            }
        }
        
        this.stageData = UCDParser.LoadStageData(this.stageDataCSV);
        SetBlocks(1);
    }
    // Update is called once per frame
    void Update()
    {
        if (this.blocks.Count != MAX_BLOCK_NUMBER) 
        {
            int count = this.blocks.Count;
            if (count < MAX_BLOCK_NUMBER)
            {
                for (int i = 0; i < MAX_BLOCK_NUMBER- count; i++)
                {
                    this.blocks.Add(new UCDBlock());
                }
            }
            else
            {
                int removeCount = count - MAX_BLOCK_NUMBER;
                int idx = this.blocks.Count - 1 - removeCount;
                this.blocks.RemoveRange(idx, removeCount);
            }
        }
    }
    public void SetBlocks(int stageNum)
    {
        int stageIdx = stageNum - 1;

        for (int i = 0; i < MAX_BLOCK_NUMBER; i++)
        {
            if (i < this.stageData[stageIdx].blockCount)
            {
                Vector2 pos = this.stageData[stageIdx].blocksPos[i];
                this.blocks[i].SetPosition(pos.x, pos.y);
            }
            else
            {
                this.blocks[i].ResetBlock();
            }
        }
        
    }
}
