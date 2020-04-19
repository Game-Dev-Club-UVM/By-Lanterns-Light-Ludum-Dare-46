using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Oil : MonoBehaviour
{
    [SerializeField] int maxOil = 100;
    [SerializeField] int currentOil = 100;


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
    public void setOil(int amount)
    {
        if(amount > maxOil)
        {
            currentOil = maxOil;
        }
        else
        {
            currentOil = amount;
        }
        isOutOfOil();
    }

    public int getOil()
    {
        return currentOil;
    }

    public void removeOil(int amount)
    {
        currentOil -= amount;
        isOutOfOil();
    }

    public void ReloadScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
