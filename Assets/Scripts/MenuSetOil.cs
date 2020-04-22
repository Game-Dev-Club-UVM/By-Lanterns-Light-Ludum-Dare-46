using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSetOil : MonoBehaviour
{
    [SerializeField] OilData oilData;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void setUpOilData()
    {
        oilData.maxOil = 100;
        oilData.currentOil = 100;
    }
}
