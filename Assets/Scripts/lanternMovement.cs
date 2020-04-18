using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternMovement : MonoBehaviour
{
    [Header("Follow Player")]
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed = 5;

    [Header("Follow Mouse")]
    [SerializeField] float delay = 5;


    // Update is called once per frame
    void Update() 
    {
        FollowMouse();
    }
    void FollowPlayer()
    {
        Vector3 offset = new Vector3(this.offset.x * -player.transform.localScale.x, this.offset.y, 0);
      
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * speed);
    }
    void FollowMouse()
    {
        Vector3 mosPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        mosPos.z = 0;

        transform.position = Vector3.Lerp(mosPos, transform.position, delay * Time.deltaTime);
    }
}
