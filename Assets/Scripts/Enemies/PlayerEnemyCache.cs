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
        player = FindObjectOfType<Player>();
        playerIsStunned = false;
    }
    public void CheckPlayerState()
    {
        //Debug.Log("CheckPlayerState called");
        FindObjectOfType<Player>();

        IPlayerState playerState = player.GetComponent<IPlayerState>();

        if (playerState != null)
        {
            var returnedState = playerState.CurrentPlayerState();
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
