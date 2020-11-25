using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDBlockManager : MonoBehaviour
{
    public const int MAX_BLOCK_NUMBER = 50;
    public const int WALL_BLOCK_NUMBER = 36;
    public const int HORIZONTAL_WALL_NUMBER = 10;
    public const int VERTICAL_WALL_NUMBER = 8;
    public const int LEFT_BOTTOM_POS = 1;
    public const int RIGHT_TOP_POS = 10;

    public GameObject blockPrefab;
    public List<UCDBlock> blocks;
    public List<UCDBlock> walls;
    public List<UCDBlock> exitWall;
    public TextAsset stageDataCSV;
    private List<UCDStageData> stageData;

    private int currentStageNumber = 1; //NotHaveTitle.... Must be Change.

 
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
        SetBlocks();
        for (int i = 0; i < WALL_BLOCK_NUMBER; i++)
        {
            UCDBlock temp = GameObject.Instantiate(this.blockPrefab).GetComponent<UCDBlock>();

            if (temp != null)
            {
                temp.ResetBlock();
                this.walls.Add(temp);
            }
        }
        SetWall();
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
    private void SetBlocks()
    {
        int stageIdx = currentStageNumber - 1;

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
    private void SetWall()
    {
        int stageIdx = currentStageNumber - 1;
        int topExitX = this.stageData[stageIdx].exitPosXorZ[0];
        int botExitX = this.stageData[stageIdx].exitPosXorZ[1];
        int leftExitZ = this.stageData[stageIdx].exitPosXorZ[2];
        int rightExitZ = this.stageData[stageIdx].exitPosXorZ[3];
        int posX = 0;
        int posZ = 0;
        const int HORIZONTAL_OFFSET = 1;
        const int VERTICAL_OFFSET = 2;
        //Wall make T -> B -> L -> R
        for (int i = 0; i < WALL_BLOCK_NUMBER; i++)
        {
            if (i < HORIZONTAL_WALL_NUMBER)
            {
                posX = i+ HORIZONTAL_OFFSET;
                posZ = RIGHT_TOP_POS;
                if (posX == topExitX)
                {
                    this.walls[i].gameObject.SetActive(false);
                    this.exitWall.Add(this.walls[i]);
                }
            }
            else if (i < 2 * HORIZONTAL_WALL_NUMBER)
            {
                posX = i+ HORIZONTAL_OFFSET - HORIZONTAL_WALL_NUMBER;
                posZ = LEFT_BOTTOM_POS;
                if (posX == botExitX)
                {
                    this.walls[i].gameObject.SetActive(false);
                    this.exitWall.Add(this.walls[i]);
                }
            }
            else if (i < 2 * HORIZONTAL_WALL_NUMBER + VERTICAL_WALL_NUMBER)
            {
                posX = LEFT_BOTTOM_POS;
                posZ = i + VERTICAL_OFFSET - 2 * HORIZONTAL_WALL_NUMBER;
                if (posZ == leftExitZ)
                {
                    this.walls[i].gameObject.SetActive(false);
                    this.exitWall.Add(this.walls[i]);
                }
            }
            else
            {
                posX = RIGHT_TOP_POS;
                posZ = i + VERTICAL_OFFSET - 2 * HORIZONTAL_WALL_NUMBER - VERTICAL_WALL_NUMBER;
                if (posZ == rightExitZ)
                {
                    this.walls[i].gameObject.SetActive(false);
                    this.exitWall.Add(this.walls[i]);
                }
            }
            this.walls[i].SetPosition(posX, posZ);
        }
    }
    private void ResetExit()
    {
        foreach (var item in this.exitWall)
        {
            item.gameObject.SetActive(true);
        }
        this.exitWall.Clear();
    }

}
