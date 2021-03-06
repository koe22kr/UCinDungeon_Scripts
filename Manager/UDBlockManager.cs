﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDBlockManager : MonoBehaviour
{
    public const int MAX_BLOCK_NUMBER = 50;
    public const int WALL_BLOCK_NUMBER = 36;
    public const int HORIZONTAL_WALL_NUMBER = 10;
    public const int VERTICAL_WALL_NUMBER = 8;
    public const int LEFT_BOTTOM_POS = 1;
    public const int RIGHT_TOP_POS = 10;

    public GameObject blockPrefab;
    public List<UDBlock> blocks;
    public List<UDBlock> walls;
    public List<UDBlock> exits;

    public Transform parentOfBlocks;
    // Start is called before the first frame update
    void Start()
    {
        UDEventManager.preSettingBlocksDelegate += Setting;

        for (int i = 0; i < MAX_BLOCK_NUMBER; i++)
        {
            UDBlock temp = GameObject.Instantiate(blockPrefab, parentOfBlocks).GetComponent<UDBlock>();
            if (temp != null)
            {
                blocks.Add(temp);
            }
        }
        for (int i = 0; i < WALL_BLOCK_NUMBER; i++)
        {
            UDBlock temp = GameObject.Instantiate(blockPrefab, parentOfBlocks).GetComponent<UDBlock>();
            if (temp != null)
            {
                temp.SetDestructible(false);
                walls.Add(temp);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (blocks.Count != MAX_BLOCK_NUMBER) 
        {
            int count = blocks.Count;
            if (count < MAX_BLOCK_NUMBER)
            {
                for (int i = 0; i < MAX_BLOCK_NUMBER- count; i++)
                {
                    blocks.Add(new UDBlock());
                }
            }
            else
            {
                int removeCount = count - MAX_BLOCK_NUMBER;
                int idx = blocks.Count - 1 - removeCount;
                blocks.RemoveRange(idx, removeCount);
            }
        }
    }

    private void Setting()
    {
        SetWalls();
        SetBlocks();
        UDEventManager.blockSettingCompleteDelegate.Invoke();
    }

    private void SetBlocks()
    {
        int stageIdx = UDGameManager.StageNum - 1;
        for (int i = 0; i < MAX_BLOCK_NUMBER; i++)
        {
            if (i < CSVData.stageData[stageIdx].blockCount)
            {
                Vector2 pos = CSVData.stageData[stageIdx].blocksPos[i];
                blocks[i].SetBlock(pos.x, pos.y);
            }
            else
            {
                blocks[i].ResetBlock();
            }
        }
    }
    private void SetWalls()
    {
        int stageIdx = UDGameManager.StageNum - 1;
        int topExitX = CSVData.stageData[stageIdx].exitPosXorZ[0];
        int botExitX = CSVData.stageData[stageIdx].exitPosXorZ[1];
        int leftExitZ = CSVData.stageData[stageIdx].exitPosXorZ[2];
        int rightExitZ = CSVData.stageData[stageIdx].exitPosXorZ[3];
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
                    walls[i].SetExit();
                    exits.Add(walls[i]);
                }
            }
            else if (i < 2 * HORIZONTAL_WALL_NUMBER)
            {
                posX = i+ HORIZONTAL_OFFSET - HORIZONTAL_WALL_NUMBER;
                posZ = LEFT_BOTTOM_POS;
                if (posX == botExitX)
                {
                    walls[i].SetExit();
                    exits.Add(walls[i]);
                }
            }
            else if (i < 2 * HORIZONTAL_WALL_NUMBER + VERTICAL_WALL_NUMBER)
            {
                posX = LEFT_BOTTOM_POS;
                posZ = i + VERTICAL_OFFSET - 2 * HORIZONTAL_WALL_NUMBER;
                if (posZ == leftExitZ)
                {
                    walls[i].SetExit();
                    exits.Add(walls[i]);
                }
            }
            else
            {
                posX = RIGHT_TOP_POS;
                posZ = i + VERTICAL_OFFSET - 2 * HORIZONTAL_WALL_NUMBER - VERTICAL_WALL_NUMBER;
                if (posZ == rightExitZ)
                {
                    walls[i].SetExit();
                    exits.Add(walls[i]);
                }
            }
            walls[i].SetBlock(posX, posZ);
        }
    }
    private void ResetExits()
    {
        foreach (var item in exits)
        {
            item.ResetExit();
        }
        exits.Clear();
    }

}
