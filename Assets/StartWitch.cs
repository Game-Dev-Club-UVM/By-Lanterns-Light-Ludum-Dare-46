using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWitch : MonoBehaviour
{
   [SerializeField] private WitchController witch;
    bool active = false;
    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( !active && collision.tag == "Player")
        {
            witch.alive = true;
            witch.witchChasing.enabled = true;
            active = true;
        }
    }
}
