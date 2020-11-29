using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCDBoxCollider : MonoBehaviour
{
    BoxCollider boxCollider;
    private bool isCollide;//게임보드 제작후 삭제

    public bool Flag
    {
        get
        {
            return isCollide;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
