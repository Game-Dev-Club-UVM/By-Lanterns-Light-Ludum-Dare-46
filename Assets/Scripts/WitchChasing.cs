using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchChasing : MonoBehaviour
{
    public float speed = 5;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }
}
