using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    [SerializeField] private GameObject player;

    // [SerializeField] private Transform 
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
