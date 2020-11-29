using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDCharacterSpawner : MonoBehaviour
{
    public float fallHeight = 10;
    GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        UCDEventManager.playerSpawnDelegate += SpawnPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayer(int posX, int posZ)
    {
        if (this.playerPrefab != null)
        {
            Instantiate(playerPrefab, new Vector3(posX, fallHeight, posZ), Quaternion.identity);
        }
    }
}
