using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject lantern;

    public float xCap = 10;
    public float yCap = 7;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        lantern = GameObject.Find("Lantern");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mathf.Abs(lantern.transform.position.x - player.transform.position.x) < xCap && Mathf.Abs(lantern.transform.position.y - player.transform.position.y) < yCap)
        //{
            transform.position = new Vector3(player.transform.position.x + (lantern.transform.position.x - player.transform.position.x) / 4f, player.transform.position.y + 1 + (lantern.transform.position.y - player.transform.position.y) / 4f, -10f);
        //} 
        //else
        //{
        //    transform.position = new Vector3(player.transform.position.x + (lantern.transform.position.x - player.transform.position.x) / 4f, player.transform.position.y + 1 + (lantern.transform.position.y - player.transform.position.y) / 4f, -10f);
        //}

    }
}