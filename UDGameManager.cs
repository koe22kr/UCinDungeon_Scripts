using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDGameManager : MonoBehaviour
{
    public float blockPlayerSettingTerm = 5;
    public float playerEnemySettingTerm = 2;
    private bool isSetting = false;
    private bool isBlockSetComplete = false;
    private bool isPlayerSetComplete = false;
    private bool isEnemySetComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        UDEventManager.blockSettingCompleteDelegate += BlockSettingComplete;
        UDEventManager.playerSettingCompleteDelegate += PlayerSettingComplete;
        UDEventManager.enemySettingCompleteDelegate += EnemySettingComplete;

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SettingGame()
    {
        if (!isSetting)
        {
            isSetting = true;
            StartCoroutine(SettingCoroutine());
        }
    }
    private IEnumerator SettingCoroutine()
    {
        isBlockSetComplete = false;
        UDEventManager.preSettingBlocksDelegate.Invoke();
        yield return new WaitForSeconds(5);
        //UDEventManager.postSettingBlocksDelegate.Invoke();
        while (!isBlockSetComplete)
        {
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(blockPlayerSettingTerm);

        isPlayerSetComplete = false;
        UDEventManager.settingPlayerDelegate.Invoke();
        while (!isPlayerSetComplete)
        {
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(playerEnemySettingTerm);

        isEnemySetComplete = false;
        UDEventManager.settingEnemyDelegate.Invoke();
        while (!isEnemySetComplete)
        {
            yield return new WaitForSeconds(1);
        }
    }
    private void BlockSettingComplete()
    {
        isBlockSetComplete = true;
    }
    private void PlayerSettingComplete()
    {
        isPlayerSetComplete = true;
    }
    private void EnemySettingComplete()
    {
        isEnemySetComplete = true;
    }
}
