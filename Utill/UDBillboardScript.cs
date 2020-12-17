using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDBillboardScript : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        this.transform.rotation = mainCamera.transform.rotation;
    }
}
