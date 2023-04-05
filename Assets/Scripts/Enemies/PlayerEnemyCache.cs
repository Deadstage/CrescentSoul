using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCache : MonoBehaviour
{
    public Player player;
    public bool playerIsStunned;

    private Entity entity;

    void Start()
    {
        
        entity = GetComponent<Entity>();
        player = FindObjectOfType<Player>();
        entity.detectedIPlayerState = player.GetComponent<IPlayerState>();
        playerIsStunned = false;
        //Debug.Log("Player is Stunned = false");

    }
    public void CheckPlayerState()
    {
        //Debug.Log("CheckPlayerState called");
        FindObjectOfType<Player>();

        if (entity.detectedIPlayerState != null)
        {
            var returnedState = entity.detectedIPlayerState.CurrentPlayerState();
            //Debug.Log("PlayerState detected");

            if (returnedState.GetType() == typeof(PlayerStunState))
            {
                playerIsStunned = true;
                //Debug.Log("Player is Stunned");
            }

            else
            {
                playerIsStunned = false;
                //Debug.Log("Player is Not Stunned");
            }
        }
    }
}
