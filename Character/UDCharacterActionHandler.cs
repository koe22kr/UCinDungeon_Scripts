using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDCharacterActionHandler : MonoBehaviour
{
    public GameObject prefab;

    BoxCollider forwardCollider;
    BoxCollider backCollider;
    BoxCollider leftCollider;
    BoxCollider rightCollider;

    enum Action
    {
        NONE=0,
        IDLE,
        ATTACK,
        MINING,
        MOVE,
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject left = Instantiate(prefab,  transform);
        leftCollider = left.GetComponent<BoxCollider>();
        leftCollider.center = new Vector3(-1f, 0.5f, 0f);

        GameObject right = Instantiate(prefab,  transform);
        rightCollider = right.GetComponent<BoxCollider>();
        rightCollider.center = new Vector3(1f, 0.5f, 0f);

        GameObject forward = Instantiate(prefab,  transform);
        forwardCollider = forward.GetComponent<BoxCollider>();
        forwardCollider.center = new Vector3(0f, 0.5f, 1f);

        GameObject back = Instantiate(prefab,  transform);
        backCollider = back.GetComponent<BoxCollider>();
        backCollider.center = new Vector3(0f, 0.5f, -1f);


        //
        //BoxCollider[] colliders = GetComponents<BoxCollider>();
        //const int ColliderCount = 4;
        //if (colliders.Length != ColliderCount)
        //{
        //    Debug.Log("BoxCollider count is not 4!");
        //}
    }

    // Update is called once per frame
    void Update()
    {
    }



    private void CheckAction(Vector3 dir)
    {
        
        BoxCollider targetCollider;

        if (dir == Vector3.forward)
        {
            targetCollider =  forwardCollider;
        }
        else if (dir == Vector3.back)
        {
            targetCollider =  backCollider;
        }
        else if (dir == Vector3.left)
        {
            targetCollider =  leftCollider;
        }
        else if (dir == Vector3.right)
        {
            targetCollider =  rightCollider;
        }
        else
        {
            Debug.Log("Direction Vector is wrong. in CheckAction");
            return;
        }
        //if (targetCollider.)
        {

        }
    }

}
