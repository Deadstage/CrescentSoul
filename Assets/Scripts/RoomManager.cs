using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject virtualCam;
    public GameObject CMvcam1;

    private EnemyOriginalPosition[] enemyPositions; // Array of EnemyOriginalPosition objects
    private PolygonCollider2D boundary; // reference to the boundary of this room

    private void Start()
    {
        CMvcam1 = GameObject.FindGameObjectWithTag("firstCam");
        CMvcam1.SetActive(true);
        boundary = GetComponent<PolygonCollider2D>(); // get the boundary at start

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy"); // Get all enemies in the scene

        // Filter the enemies that are in this room
        List<EnemyOriginalPosition> enemiesInRoom = new List<EnemyOriginalPosition>();
        foreach (var enemy in allEnemies)
        {
            if (boundary.OverlapPoint(enemy.transform.position)) // if the enemy is in this room
            {
                var enemyPos = enemy.GetComponent<EnemyOriginalPosition>();
                enemiesInRoom.Add(enemyPos);

                // Deactivate the enemy initially
                enemyPos.gameObject.SetActive(false);
            }
        }

        enemyPositions = enemiesInRoom.ToArray(); // Convert list to array
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);

            // Activate and reset all the enemies in the room
            foreach(var enemyPos in enemyPositions)
            {
                enemyPos.gameObject.SetActive(true);
                enemyPos.EnemyRespawn(); // reset the enemy to its original state
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);

            // Deactivate all the enemies in the room
            foreach(var enemyPos in enemyPositions)
            {
                enemyPos.gameObject.SetActive(false);
            }
        }
    }

}