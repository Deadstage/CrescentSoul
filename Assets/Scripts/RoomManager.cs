using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject virtualCam;
    public GameObject CMvcam1;

    private EnemyOriginalPosition[] enemyPositions; // Array of EnemyOriginalPosition objects
    private TorchOriginalPosition[] torchPositions; // Array of TorchOriginalPosition objects
    private PolygonCollider2D boundary; // reference to the boundary of this room

    private void Start()
    {
        CMvcam1 = GameObject.FindGameObjectWithTag("firstCam");
        CMvcam1.SetActive(true);
        boundary = GetComponent<PolygonCollider2D>(); // get the boundary at start

        // For Enemies
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy"); // Get all enemies in the scene
        List<EnemyOriginalPosition> enemiesInRoom = new List<EnemyOriginalPosition>();
        foreach (var enemy in allEnemies)
        {
            if (boundary.OverlapPoint(enemy.transform.position))
            {
                var enemyPos = enemy.GetComponent<EnemyOriginalPosition>();
                enemiesInRoom.Add(enemyPos);
                enemyPos.gameObject.SetActive(false);
            }
        }
        enemyPositions = enemiesInRoom.ToArray();

        // For Torches
        GameObject[] allTorches = GameObject.FindGameObjectsWithTag("Torch"); // Get all torches in the scene
        List<TorchOriginalPosition> torchesInRoom = new List<TorchOriginalPosition>();
        foreach (var torch in allTorches)
        {
            if (boundary.OverlapPoint(torch.transform.position))
            {
                var torchPos = torch.GetComponent<TorchOriginalPosition>();
                torchesInRoom.Add(torchPos);
                torchPos.gameObject.SetActive(false);
            }
        }
        torchPositions = torchesInRoom.ToArray();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);

            // For Enemies
            foreach (var enemyPos in enemyPositions)
            {
                if (enemyPos != null)
                {
                    enemyPos.gameObject.SetActive(true);
                    enemyPos.EnemyRespawn();
                }
            }

            // For Torches
            foreach (var torchPos in torchPositions)
            {
                if (torchPos != null)
                {
                    torchPos.gameObject.SetActive(true);
                    torchPos.TorchRespawn();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);

            // For Enemies
            foreach (var enemyPos in enemyPositions)
            {
                enemyPos.gameObject.SetActive(false);
            }

            // For Torches
            foreach (var torchPos in torchPositions)
            {
                torchPos.gameObject.SetActive(false);
            }
        }
    }
}