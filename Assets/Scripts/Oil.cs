using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Oil : MonoBehaviour
{
    [SerializeField] float maxOil = 100;
    [SerializeField] float currentOil = 100;
    [SerializeField] OilBar oilBar;

    private void Start()
    {
        oilBar = GameObject.FindGameObjectWithTag("Oil Bar").GetComponent<OilBar>();
        oilBar.SetMaxHealth(maxOil);
    }
    public bool isOutOfOil()
    {
        if(currentOil <= 0)
        {
            ReloadScence();
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
        oilBar.SetHealth(currentOil);
        isOutOfOil();
    }

    public void ReloadScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
