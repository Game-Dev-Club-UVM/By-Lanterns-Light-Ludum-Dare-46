using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Vector3 CheckPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetCheckPoint();
    }

    public void ReloadScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.transform.position = CheckPoint;
    }
    public void SetCheckPoint()
    {
        CheckPoint = player.transform.position;
    }
}
