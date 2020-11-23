using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDBlock : MonoBehaviour
{
    public float posX = -100f;
    public float posY = 0.5f;
    public float posZ = -100f;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetBlock()
    {
        this.posY = 0.5f;
        this.posX = posZ = -100;
        this.transform.position = new Vector3(this.posX, this.posY, this.posZ);

    }
    public void SetPosition(float x, float z, float y = 1f)
    {
        //blocks pivot == center // size 1 => offset 0.5
        float blockPivotOffset = 0.5f;

        this.posX = x - blockPivotOffset;
        this.posY = y;
        this.posZ = z - blockPivotOffset;
        this.transform.position = new Vector3(this.posX, this.posY, this.posZ);
    }
}
