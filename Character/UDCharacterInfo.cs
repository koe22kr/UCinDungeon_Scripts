using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDCharacterInfo : MonoBehaviour
{
    public int maxHp = 10;
    public int hp = 10;
    public UDPrograssBar hpBar;
    public UDStatusText hpText;

    public int maxMp = 10;
    public int mp = 10;
    public UDPrograssBar mpBar;//not use yet
    public UDStatusText mpText;//not use yet
    public int attack = 1;
    // Start is called before the first frame update
    void Start()
    {
      

        if (hpBar != null)
        {
            hpBar.Setting(maxHp, hp);
        }
        if (mpBar != null)
        {
            mpBar.Setting(maxMp, mp);
        }
        if (hpText != null)
        {
            hpText.Setting(hp, maxHp);
        }
        if (mpText != null)
        {
            mpText.Setting(mp, maxMp);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetPrograssBar(UDPrograssBar inHpBar,UDPrograssBar inMpBar)
    {
        hpBar = inHpBar;
        mpBar = inMpBar;
    }
    public void TakeDamage(int dmg)
    {
        SetHpRelative(-dmg);
        if (hpBar != null)
        {
            hpBar.SetValue(hp);
        }
        if (hpText != null)
        {
            hpText.SetValue(hp);
        }
        if (IsDead())
        {
            Vector3 pos = this.transform.position;
            UDEventManager.characterDeadDelegate.Invoke((int)pos.x, (int)pos.z);
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    public void SetHpRelative(int inHpRelative)
    {
        hp += inHpRelative;
    }
    public void SetHp(int inHp)
    {
        hp = inHp;
    }
    public void SetMpRelative(int inMpRelative)
    {
        mp += inMpRelative;
    }
    public void SetMp(int inMp)
    {
        mp = inMp;
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
