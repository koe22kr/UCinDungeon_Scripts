using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPlayerSpawner : MonoBehaviour
{
    public int spawnPosX = 3;
    public int spawnPosZ = 3;
    public float fallHeight = 10;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        UDEventManager.settingPlayerDelegate += SpawnPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayer()
    {
        if (this.playerPrefab != null)
        {
            GameObject instant = Instantiate(playerPrefab, new Vector3(spawnPosX, fallHeight, spawnPosZ), Quaternion.identity);
            UDEventManager.boardSetObjectDelegate(spawnPosX, spawnPosZ, ge.ObjectType.PLAYER, instant);
            UDEventManager.playerSettingCompleteDelegate.Invoke();

        }
    }
}
