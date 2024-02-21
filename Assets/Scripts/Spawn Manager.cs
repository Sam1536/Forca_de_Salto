using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnObjectPrefab;
    private int obstacleIndex;
    private Vector3 spawnPos = new Vector3(25, 0, 0);

    private float startDelay;
    private float repeatRate;
    
    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("SpawnObstacle", startDelay,repeatRate);
       playerController = FindObjectOfType<PlayerController>();

    }   

    private void SpawnObstacle()
    {
        obstacleIndex = Random.Range(0, 5);
        
        if (playerController.gameOver == false)
        {
            Instantiate(spawnObjectPrefab[obstacleIndex], spawnPos, spawnObjectPrefab[obstacleIndex].transform.rotation);
            startDelay = Random.Range(3f, 6f);
            repeatRate = Random.Range(3f, 7f);
            CancelInvoke();
            InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
          

        }
        

    }

}