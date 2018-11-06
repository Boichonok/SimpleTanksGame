using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tank;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerTank;

    [SerializeField]
    private GameObject aiTank;

    [SerializeField]
    private Transform[] spawnPoints;
   


    private void Start()
    {
        playerTank.GetComponent<PlayerTank>().SpawnPoint = spawnPoints[0];
  
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            aiTank.GetComponent<AITank>().SpawnPoint = spawnPoints[i];
            Instantiate(aiTank, aiTank.GetComponent<AITank>().SpawnPoint);
        }
    }

   
}
