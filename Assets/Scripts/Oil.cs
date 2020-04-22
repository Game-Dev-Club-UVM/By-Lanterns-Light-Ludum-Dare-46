using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
public class Oil : MonoBehaviour
{
    [SerializeField] OilData oilData;
    [SerializeField] OilBar oilBar;
    LevelManager levelManager;
    GameObject lantern;
    Light2D lanternLight;
    [SerializeField] float timeSpent = 0;
    [SerializeField] float deathTime = 3;
    [SerializeField] float radiusNormal = 4;
    [SerializeField] float radiusDeath = 100;
    [SerializeField] float intensityNormal = 1;
    [SerializeField] float intensityDeath = 8;
    [SerializeField] float timeSpent2 = 0;
    [SerializeField] float deathTime2 = 1;

    [SerializeField] Light2D deathLight;
    bool explode = false;
    private void Start()
    {
        Cursor.visible = false;
        oilBar = GameObject.FindGameObjectWithTag("OilBar").GetComponent<OilBar>();
        oilBar.SetMaxHealth(oilData.maxOil);
        oilBar.SetHealth(oilData.currentOil);
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        lantern = GameObject.Find("Lantern");
        lanternLight = lantern.GetComponentInChildren<Light2D>();
    }
    public bool isOutOfOil()
    {
        if(oilData.currentOil <= 0 && !explode)
        {
            //levelManager.ReloadScene();
            explode = true;
            StartCoroutine(resetLantern());
            //setOil(oilData.maxOil);
            //timeSpent = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        if (explode && timeSpent < deathTime)
        {
            deathLight.enabled = true;
            timeSpent += Time.deltaTime;
            deathLight.pointLightOuterRadius = Mathf.Lerp(radiusNormal, radiusDeath, timeSpent / deathTime);
            deathLight.intensity = Mathf.Lerp(intensityNormal, intensityDeath, timeSpent / deathTime);
        } 
        else if (deathLight.pointLightOuterRadius != radiusNormal)
        {
            deathLight.enabled = true;
            timeSpent2 += Time.deltaTime;
            deathLight.pointLightOuterRadius = Mathf.Lerp(radiusDeath, radiusNormal, timeSpent2 / deathTime2);
            deathLight.intensity = Mathf.Lerp(intensityDeath, intensityNormal, timeSpent2 / deathTime2);
        } 
        else
        {
            
            timeSpent2 = 0;
            deathLight.pointLightOuterRadius = radiusNormal;
            deathLight.intensity = intensityNormal;
            deathLight.enabled = false;
        }
    }

    IEnumerator resetLantern()
    {
        yield return new WaitForSeconds(deathTime);

        levelManager.ReloadScene();
        setOil(oilData.maxOil);
        timeSpent = 0;
        explode = false;
        deathLight.enabled = false;
        //yield return new WaitForSeconds(deathTime);

        //lanternLight.pointLightOuterRadius = radiusNormal;
        //lanternLight.intensity = intensityNormal;
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
