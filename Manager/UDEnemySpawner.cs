using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float fallHeight = 10;
    // Start is called before the first frame update
    void Start()
    {
        UDEventManager.settingEnemyDelegate += SpawnEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        if (this.enemyPrefab != null)
        {
            int stageIdx = UDGameManager.StageNum - 1;
            for (int i = 0; i < CSVData.stageData[stageIdx].enemysPos.Count; i++)
            {
                int posX = CSVData.stageData[stageIdx].enemysPos[i].x;
                int posZ = CSVData.stageData[stageIdx].enemysPos[i].y;
                GameObject go = Instantiate(enemyPrefab, new Vector3(posX, fallHeight, posZ), Quaternion.identity);
                UDEventManager.boardSetObjectDelegate(posX, posZ, ge.ObjectType.ENEMY, go);
            }

            UDEventManager.enemySettingCompleteDelegate.Invoke();
        }
    }
}
