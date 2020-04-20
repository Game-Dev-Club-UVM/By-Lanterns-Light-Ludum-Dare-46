using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private static GameObject player;
    [SerializeField] private GameObject lantern;
    [SerializeField] private static int dmgOnEnter = 10;
    [SerializeField] private static int dmgOnStay = 1;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lantern = GameObject.FindGameObjectWithTag("Lantern");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Lantern")
        {
            lantern.GetComponent<Oil>().removeOil(dmgOnStay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Lantern")
        {
            lantern.GetComponent<Oil>().removeOil(dmgOnEnter);
        }
    }


}