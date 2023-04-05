using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOriginalPosition : MonoBehaviour
{
    public Vector2 originalPosition;

    public RoomManager roomManager { get; private set; }

    public bool respawnTrigger = false;

    //private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats Stats { get => stats ?? (stats = core.GetComponentInChildren<Stats>()); }
    private Stats stats;
    private Core core;

    private Rigidbody2D deathRB;

    private Entity entity;
    private State state;

    private void Start()
    {

        this.originalPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        this.core = GetComponentInChildren<Core>();
        this.roomManager = GetComponent<RoomManager>();

        FiniteStateMachine stateMachine = new FiniteStateMachine();


        //Debug.Log(this.originalPosition);
        //Debug.Log(this.gameObject);
        //Debug.Log(respawnTrigger);

    }

    public void EnemyRespawn()
    {
        //Debug.Log("Resetting");
        
        this.gameObject.SetActive(true);
        this.Stats.currentHealth = this.stats.maxHealth;
        this.gameObject.transform.position = this.originalPosition;

        deathRB = core.transform.parent.gameObject.GetComponent<Rigidbody2D>();

        deathRB.constraints = RigidbodyConstraints2D.None;
        deathRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        BoxCollider2D[] colliders = this.core.transform.parent.gameObject.GetComponentsInChildren<BoxCollider2D>();

        foreach(BoxCollider2D collider2D in colliders)
        {
            collider2D.enabled = true;
        }
        //Debug.Log("Reset");
    }

}
