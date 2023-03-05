using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targget : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100f;
    public void score(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Debug.Log("dead");
        }
    }
}
