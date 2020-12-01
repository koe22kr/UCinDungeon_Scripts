using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVData : MonoBehaviour
{
    public TextAsset stageDataCSV;
    public static List<UDStageData> stageData;

    private void Awake()
    {
        LoadStageData();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadStageData()
    {
        stageData = UDParser.LoadStageData(stageDataCSV);
    }
}
