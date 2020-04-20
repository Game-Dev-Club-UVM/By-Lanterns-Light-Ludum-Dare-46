using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolFlame : MonoBehaviour
{
    private GameObject lantern;
    AudioSource audioSource;
    private GameObject image;
    bool collected = false;
    [SerializeField] int increaseAmount = 20; 
    private void Awake()
    {
        lantern = GameObject.FindGameObjectWithTag("Lantern");
        audioSource = GetComponent<AudioSource>();
        image = this.transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected && collision.tag == "Player")
        {
            lantern.GetComponent<Oil>().increaseMaxOil(increaseAmount);
            collected = true;
            audioSource.Play();
            StartCoroutine(refillOil());
            image.SetActive(false);
        }
    }
    IEnumerator refillOil()
    {
        for(int i =0; i < increaseAmount; i++)
        {
            yield return new WaitForSeconds(.2f);
            lantern.GetComponent<Oil>().addOil(1);
        } 
    }
}
