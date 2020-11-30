using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDBlock : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public float posX = -100f;
    public float posY = 0.5f;
    public float posZ = -100f;
    public Color color;
    private ge.ObjectType type = ge.ObjectType.BLOCK;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ////
        float red =Random.Range(0.0f, 1.0f);
        float green = Random.Range(0.0f, 1.0f);
        float blue = Random.Range(0.0f, 1.0f);
        color = new Color(red, green, blue);
        meshRenderer.material.color = color;

        
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    public void ResetBlock()
    {
        posY = 0.5f;
        posX = posZ = -100;
        transform.position = new Vector3(posX, posY, posZ);
    }
    public void SetDestructible(bool flag)
    {
        if (flag)
        {
            type = ge.ObjectType.BLOCK;
        }
        else
        {
            type = ge.ObjectType.WALL;
        }
        
    }
    
    public void SetBlock(float x, float z, float y = 1f)
    {
        posX = x;
        posY = y;
        posZ = z;
        transform.position = new Vector3(posX, posY, posZ);
        UDEventManager.boardSetObjectDelegate((int)x, (int)z, type);// x,z is x,y for gameboard.
    }
   
    public void SetColor(Color inColor)
    {
        color = inColor;
        meshRenderer.material.color = color;
    }
    public void SetExit()
    {
        this.gameObject.SetActive(false);
        type = ge.ObjectType.NONE;
    }
    public void ResetExit()
    {
        this.gameObject.SetActive(true);
    }
}
