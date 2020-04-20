using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OilData", menuName = "ScriptableObjects/OilData", order = 1)]
public class OilData : ScriptableObject
{
    public float maxOil = 100;
    public float currentOil = 100;
}
