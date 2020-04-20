using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject lantern;
    [SerializeField] private int oilRefilAmount = 5;

    [SerializeField] private GameObject flame;
    private static CheckPoint lastCheckPoint = null;
    [SerializeField] private bool activated = false;
    private void Awake()
    {
        lantern = GameObject.FindGameObjectWithTag("Lantern");
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        flame.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            lantern.GetComponent<Oil>().addOil(oilRefilAmount);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated && collision.tag == "Player")
        {
            activated = true;
            levelManager.SetCheckPoint();
            flame.SetActive(true);
            if(lastCheckPoint != null)
            {
                lastCheckPoint.ResetCheckPoint();
            }
            lastCheckPoint = this;
        }
    }

    public void ResetCheckPoint()
    {
        activated = false;
        flame.SetActive(false);
    }
}
