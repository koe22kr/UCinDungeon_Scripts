using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDCharacterInfo : MonoBehaviour
{
    public int hp = 10;
    public int attack = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (IsDead())
        {
            Vector3 pos = this.transform.position;
            UDEventManager.characterDeadDelegate.Invoke((int)pos.x, (int)pos.z);
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
    public bool IsDead()
    {
        if (hp <= 0)
        {
            return true;
        }
        return false;
    }
}
