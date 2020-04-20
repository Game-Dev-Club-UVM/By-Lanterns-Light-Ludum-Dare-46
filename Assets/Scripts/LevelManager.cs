using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    [SerializeField] private GameObject player;

    [SerializeField] private Transform CheckPoint;

    private void Start()
    {
        CheckPoint = player.transform;
    }

    public void ReloadScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SetCheckPoint()
    {
        CheckPoint = player.transform;
    }
}
