using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    int z = 1;
    void Start()
    {
        Switching(z);
    }

    // Update is called once per frame
    void Update()
    {
        //HAVE REVERSED THE SCROLL WHEEL CONDITION FOR GETTING THE REQUIRED FLOW OF GUNS
        if(Input.GetAxis("Mouse ScrollWheel")<0f)
        {
            Debug.Log(Input.GetAxis("Mouse ScrollWheel") );
            z++;
            if (z > 2)
            {
                z = 0;
                Switching(z);
            }
            else
                Switching(z);
        }
        else if(Input.GetAxis("Mouse ScrollWheel")>0f)
        {
            z--;
            if (z<0)
            {
                z = 2;
                Switching(z);
            }
            else
                Switching(z);
        }
    }

    private void Switching(int i)
    { int j = 0;
        foreach(Transform weapon in transform)
        {
          
            if (j == i)
            {
                weapon.gameObject.SetActive(true);
                //Debug.Log(j);
                // Debug.Log(guns[i]);
                UiManager.instance.GunName(j);
            }
            else { weapon.gameObject.SetActive(false); }
            
            j++;
        }    
    }
}
