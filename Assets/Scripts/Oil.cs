using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Oil : MonoBehaviour
{
    [SerializeField] OilData oilData;
    OilBar oilBar;
    LevelManager levelManager;
    private void Awake()
    {
        oilBar = GameObject.FindGameObjectWithTag("OilBar").GetComponent<OilBar>();
        oilBar.SetMaxHealth(oilData.maxOil);
        oilBar.SetHealth(oilData.currentOil);
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }
    public bool isOutOfOil()
    {
        if(oilData.currentOil <= 0)
        {
            levelManager.ReloadScene();
            setOil(oilData.maxOil);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void setOil(float amount)
    {
        if(amount > oilData.maxOil)
        {
            oilData.currentOil = oilData.maxOil;
        }
        else
        {
            oilData.currentOil = amount;
        }
        oilBar.SetHealth(oilData.currentOil);
        isOutOfOil();
    }

    public void increaseMaxOil(float amount)
    {
        oilData.maxOil += amount;
        oilBar.SetMaxHealth(oilData.maxOil);
    }

    public float getOil()
    {
        return oilData.currentOil;
    }

    public void removeOil(float amount)
    {
        oilData.currentOil -= amount;
        if(oilData.currentOil < 0)
        {
            oilData.currentOil = 0;
        }
        else if(oilData.currentOil > oilData.maxOil)
        {
            oilData.currentOil = oilData.maxOil;
        }
        oilBar.SetHealth(oilData.currentOil);
        isOutOfOil();
    }
    public void addOil(float amount)
    {
        oilData.currentOil += amount;
        if (oilData.currentOil < 0)
        {
            oilData.currentOil = 0;
        }
        else if (oilData.currentOil > oilData.maxOil)
        {
            oilData.currentOil = oilData.maxOil;
        }
        oilBar.SetHealth(oilData.currentOil);
        isOutOfOil();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
