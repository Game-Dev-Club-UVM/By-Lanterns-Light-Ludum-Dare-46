using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    [SerializeField] private Oil lantern;
    private AudioSource audioSource;

    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;
    private bool played = false;

    private float laughTimer = 4f;
    private float laughCount = 0f;

    public bool alive = false;

    [SerializeField] public WitchChasing witchChasing;
    private void Awake()
    {
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();
        lantern = GameObject.FindGameObjectWithTag("Lantern").GetComponent<Oil>();

    }

    private void Update()
    {
        if(lantern.getOil() <= 0)
        {
            transform.position = startPos;
        }
        if (!alive) { return; }
        if (laughCount <= 0)
        {
            audioSource.Play();
            laughCount = laughTimer;
            played = true;
        }
        else
        {
            laughCount -= Time.deltaTime;
        }
        if (!audioSource.isPlaying && played)
        {
            if(Random.Range(0,100) % 2 == 0)
            {
                audioSource.clip = clip1;
            }
            else
            {
                audioSource.clip = clip2;
            }
            
            played = false;
        }
    }
}
