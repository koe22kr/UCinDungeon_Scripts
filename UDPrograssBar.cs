using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDPrograssBar : MonoBehaviour
{
    public int maxValue;
    public int value;
    public int prevValue;
    private float elapseTime;
    public float lerpTime = 1f;
    public Image ValueImage;
    public Image preValueImage;
    private float valuePer;
    private float preValuePer;
    // Start is called before the first frame update
    void Start()
    {
        elapseTime = lerpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapseTime < lerpTime)
        {
            if (preValueImage != null && ValueImage != null)
            {
                preValueImage.fillAmount = Mathf.Lerp(preValuePer, valuePer, elapseTime);
            }
            elapseTime += Time.deltaTime;
        }
    }

    private void CalculateValue()
    {
        valuePer = (float)value / maxValue;
        preValuePer = (float)prevValue / maxValue;
        ValueImage.fillAmount = valuePer;
        preValueImage.fillAmount = preValuePer;
    }

    public void Setting(int inMaxValue,int inValue)
    {
        maxValue = inMaxValue;
        value = inValue;
        prevValue = value;
        CalculateValue();
    }
   
    public void SetValue(int inValue)
    {
        elapseTime = 0;
        prevValue = value;
        value = inValue;
        CalculateValue();
        // - 
    }
}
