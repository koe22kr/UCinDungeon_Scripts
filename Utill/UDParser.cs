using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UDParser
{
   
    static public List<UDStageData> LoadStageData(TextAsset data)
    {
        string srcText = data.text;
        string[] lines = srcText.Split('\n',' ','"',',','\r');
        List<UDStageData> ret=new List<UDStageData>();
        UDStageData stage = new UDStageData();
        int componentCounter = 0;
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
                    ret.Add(new UDStageData(stage));
                    stage.Reset();
                    componentCounter = 0;
                }
                continue;
            }
            if (componentCounter < UDStageData.HEADER_COUNT) 
            {
                switch (componentCounter)
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
                    case 2:
                        {
                            stage.enemyCount = int.Parse(lines[i]);
                        }
                        break;
                    default:
                        {
                            Debug.LogWarning("LoadStageData->ItemCounter->switch->default! Counter is Wrong");
                        }
                        break;
                }
            }
            else if (componentCounter < UDStageData.HEADER_COUNT + stage.blockCount)
            {
                if (isPosX)
                {
                    posX = int.Parse(lines[i]);
                    isPosX = false;
                    componentCounter--;
                }
                else
                {
                    posY = int.Parse(lines[i]);
                    stage.blocksPos.Add(new Vector2Int(posX, posY));
                    posX = posY = 0;
                    isPosX = true;
                }
            }
            else if (componentCounter < UDStageData.HEADER_COUNT + stage.blockCount + UDStageData.EXIT_COUNT)
            {
                exitIdx = int.Parse(lines[i]);
                stage.exitPosXorZ.Add(exitIdx);
                exitIdx = 0;
            }
            else if (componentCounter < UDStageData.HEADER_COUNT + stage.blockCount + UDStageData.EXIT_COUNT+ stage.enemyCount)
            {
                if (isPosX)
                {
                    posX = int.Parse(lines[i]);
                    isPosX = false;
                    componentCounter--;
                }
                else
                {
                    posY = int.Parse(lines[i]);
                    stage.enemysPos.Add(new Vector2Int(posX, posY));
                    posX = posY = 0;
                    isPosX = true;
                }
            }

            componentCounter++;
        }
        return ret;
    }
}
