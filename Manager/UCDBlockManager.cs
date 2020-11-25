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


        //Wall make T -> B -> L -> R
        for (int i = 1; i <= WALL_BLOCK_NUMBER; i++)
        {
            if (i < HORIZONTAL_WALL_NUMBER)
            {
                walls[i].SetPosition(i, RIGHT_TOP_POS);
                if (i == topExitX)
                {
                    walls[i].enabled = false;
                }
            }
            else if (i < 2 * HORIZONTAL_WALL_NUMBER)
            {
                walls[i].SetPosition(i, LEFT_BOTTOM_POS);
                if (i == botExitX)
                {
                    walls[i].enabled = false;
                }
            }
            else if (i < 2 * HORIZONTAL_WALL_NUMBER + VERTICAL_WALL_NUMBER)
            {
                walls[i].SetPosition(LEFT_BOTTOM_POS, i);
                if (i == leftExitZ)
                {
                    walls[i].enabled = false;
                }
            }
            else
            {
                walls[i].SetPosition(RIGHT_TOP_POS, i);
                if (i == rightExitZ)
                {
                    walls[i].enabled = false;
                }
            }

        }
    }
        
}
