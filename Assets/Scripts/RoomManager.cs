using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject virtualCam;
    public GameObject CMvcam1;

    private EnemyOriginalPosition[] enemyPositions; // Define an array of EnemyOriginalPosition objects

    private void Start()
    {
        CMvcam1 = GameObject.FindGameObjectWithTag("firstCam");
        CMvcam1.SetActive(true);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Populate the enemyPositions array with the EnemyOriginalPosition components of the enemies
        enemyPositions = new EnemyOriginalPosition[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyPositions[i] = enemies[i].GetComponent<EnemyOriginalPosition>();
        }     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);

            // Loop through the enemyPositions array and call the EnemyRespawn() method for each enemy
            foreach(var enemyPos in enemyPositions)
            {
                enemyPos.respawnTrigger = true;
                enemyPos.EnemyRespawn();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);

            foreach(var enemyPos in enemyPositions)
            {

                enemyPos.respawnTrigger = false;

            }
            // enemyOriginalPosition[1].respawnTrigger = false;
            // enemyOriginalPosition[2].respawnTrigger = false;
            // enemyOriginalPosition[3].respawnTrigger = false;
            //Debug.Log(enemyOriginalPosition.respawnTrigger);

        }
    }

}