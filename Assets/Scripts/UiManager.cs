using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;//Singleton Created
    
    public TMP_Text gunName;
    public TMP_Text ammo;

    string[] guns = { "rifle", "sniper", "pistol" };
    void Awake()
    {
        if(instance!=null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    public void GunName(int k)
    {
        gunName.text = guns[k];
    }
    public void AmmoCount(int ammoCount,bool isReload)
    {
        if (!isReload)
            ammo.text = "reload";
        else
        ammo.text = ammoCount.ToString();
        
    }
}
