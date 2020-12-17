using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UDStatusText : MonoBehaviour
{
    
    public TextMesh valueText;
    public TextMesh maxValueText;
    public int value;
    public int maxValue;
    // Start is called before the first frame update
    void Start()
    {
        UDCharacterInfo info = GetComponent<UDCharacterInfo>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int inValue,int inMaxValue)
    {
        value = inValue;
        maxValue = inMaxValue;

        valueText.text = value.ToString();
        maxValueText.text = maxValue.ToString();
    }


    public void SetValue(int inValue)
    {
        valueText.text = inValue.ToString();
    }
}
