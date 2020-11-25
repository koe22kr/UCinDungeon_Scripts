﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UCDParser
{
   
    static public List<UCDStageData> LoadStageData(TextAsset data)
    {
        string srcText = data.text;
        string[] lines = srcText.Split('\n',' ','"',',','\r');
        List<UCDStageData> ret=new List<UCDStageData>();
        UCDStageData stage = new UCDStageData();
        int itemCounter = 0;
        bool isPosX = true;
        int posX = 0;
        int posY = 0;
        int exitIdx = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == "") 
            {
                continue;
            }
            if (lines[i].StartsWith("#"))
            {
                if (lines[i] == "#END")
                {
                    ret.Add(new UCDStageData(stage));
                    stage.Reset();
                    itemCounter = 0;
                }
                continue;
            }
            if (itemCounter < UCDStageData.HEADER_COUNT) 
            {
                switch (itemCounter)
                {
                    case 0:
                        {
                            stage.stageNum = int.Parse(lines[i]);
                        }
                        break;
                    case 1:
                        {
                            stage.blockCount = int.Parse(lines[i]);

                        }
                        break;
                    default:
                        {
                            Debug.LogWarning("LoadStageData->ItemCounter->switch->default! Counter is Wrong");
                        }
                        break;
                }
            }
            else if (itemCounter < UCDStageData.HEADER_COUNT + stage.blockCount)
            {
                if (isPosX)
                {
                    posX = int.Parse(lines[i]);
                    isPosX = false;
                    itemCounter--;
                }
                else
                {
                    posY = int.Parse(lines[i]);
                    stage.blocksPos.Add(new Vector2Int(posX, posY));
                    posX = posY = 0;
                    isPosX = true;
                }
            }
            else if (itemCounter < UCDStageData.HEADER_COUNT + stage.blockCount + UCDStageData.EXIT_COUNT)
            {
                exitIdx = int.Parse(lines[i]);
                stage.exitPosXorZ.Add(exitIdx);
                exitIdx = 0;
            }

            itemCounter++;
        }
        return ret;
    }
}
