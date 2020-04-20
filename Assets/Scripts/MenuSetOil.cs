using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSetOil : MonoBehaviour
{
    [SerializeField] OilData oilData;
    public void setUpOilData()
    {
        oilData.maxOil = 100;
        oilData.currentOil = 100;
    }
}
