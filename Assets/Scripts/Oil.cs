using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Oil : MonoBehaviour
{
    [SerializeField] float maxOil = 100;
    [SerializeField] float currentOil = 100;
    [SerializeField] OilBar oilBar;
    [SerializeField] LevelManager levelManager;
    private void Start()
    {
        oilBar = GameObject.FindGameObjectWithTag("Oil Bar").GetComponent<OilBar>();
        oilBar.SetMaxHealth(maxOil);
        oilBar.SetHealth(currentOil);
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }
    public bool isOutOfOil()
    {
        if(currentOil <= 0)
        {
            levelManager.ReloadScene();
            setOil(maxOil);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void setOil(float amount)
    {
        if(amount > maxOil)
        {
            currentOil = maxOil;
        }
        else
        {
            currentOil = amount;
        }
        oilBar.SetHealth(currentOil);
        isOutOfOil();
    }

    public float getOil()
    {
        return currentOil;
    }

    public void removeOil(float amount)
    {
        currentOil -= amount;
        if(currentOil < 0)
        {
            currentOil = 0;
        }
        else if(currentOil > maxOil)
        {
            currentOil = maxOil;
        }
        oilBar.SetHealth(currentOil);
        isOutOfOil();
    }
    public void addOil(float amount)
    {
        currentOil += amount;
        if (currentOil < 0)
        {
            currentOil = 0;
        }
        else if (currentOil > maxOil)
        {
            currentOil = maxOil;
        }
        oilBar.SetHealth(currentOil);
        isOutOfOil();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
