using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public static DamageDealer Instance { get; private set; }

    public int PlayerAtkDmg = 10;

    void Awake()
    {
        Instance = this;
    }


}
