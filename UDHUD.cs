using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UDHUD : MonoBehaviour
{
    //이 클래스 제거할 궁리를 해보자.
    public UDCharacterInfo targetInfo;
    public UDPrograssBar hp;
    public UDPrograssBar mp;
    // Start is called before the first frame update
    void Start()
    {
        UDEventManager.SetPlayerDelegate += SetTarget;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetTarget(GameObject go)
    {
        GetComponent<Canvas>().enabled = true;
        if (mp == null || hp == null)
        {
            Debug.Log("UDHUD.SetTarget. PrograssBar is null");
        }
        targetInfo = go.GetComponent<UDCharacterInfo>();
        hp.Setting(targetInfo.maxHp, targetInfo.hp);
        mp.Setting(targetInfo.maxMp, targetInfo.mp);
        targetInfo.SetPrograssBar(hp, mp);
    }

}
