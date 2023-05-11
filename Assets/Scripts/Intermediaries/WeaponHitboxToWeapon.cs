using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private AggressiveStance stance;
    //private KickStance kickStance;

    private void Awake()
    {
        stance = GetComponentInParent<AggressiveStance>();
        //kickStance = GetComponentInParent<KickStance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D");
        stance.AddToDetected(collision);
        //kickStance.AddToDetected(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D");
        stance.RemoveFromDetected(collision);
        //kickStance.RemoveFromDetected(collision);
    }
}
