using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDBlock : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public float posX = -100f;
    public float posY = 0.5f;
    public float posZ = -100f;
    public Color color;
    
    // Start is called before the first frame update
    void Start()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();
        ////
        float red =Random.Range(0.0f, 1.0f);
        float green = Random.Range(0.0f, 1.0f);
        float blue = Random.Range(0.0f, 1.0f);
        this.color = new Color(red, green, blue);
        this.meshRenderer.material.color = color;
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
        this.posX = x;
        this.posY = y;
        this.posZ = z;
        this.transform.position = new Vector3(this.posX, this.posY, this.posZ);
        
    }

    public void SetColor(Color inColor)
    {
        this.color = inColor;
        this.meshRenderer.material.color = this.color;
    }
}
