using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternMovement : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] float delay = .5f;
    [SerializeField] float speed = 5;
    

    // Update is called once per frame
    void Update() 
    {
        Vector3 playerPos = player.transform.position * -player.transform.localScale.x;

        transform.position = Vector3.Lerp(transform.position, playerPos + offset, Time.deltaTime * speed);
    }
}
