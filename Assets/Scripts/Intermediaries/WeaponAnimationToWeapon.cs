using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Stance stance;
    private BoxCollider2D parryHitbox;
    private void Start()
    {
        stance = GetComponentInParent<Stance>();
        parryHitbox = GetComponentInChildren<BoxCollider2D>();
    }
    

    private void AnimationFinishTrigger()
    {
        stance.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()
    {
        stance.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()
    {
        stance.AnimationStopMovementTrigger();
    }

    private void AnimationStartMovementTriggerReverse()
    {
        stance.AnimationStartMovementTriggerReverse();
    }
    private void AnimationTurnOffFlipTrigger()
    {
        stance.AnimationTurnOffFlipTrigger();
    }
    private void AnimationTurnOnFlipTrigger()
    {
        stance.AnimationTurnOnFlipTrigger();
    }

    private void AnimationActionTrigger()
    {
        stance.AnimationActionTrigger();
    }
    
    private void AnimationMoveDownwardsTrigger()
    {
        stance.AnimationMoveDownwardsTrigger();
    }

    private void AnimationStartUpwardMovementTrigger()
    {
        stance.AnimationStartUpwardMovementTrigger();
    }

    private void EnableParryHitbox()
    {
        parryHitbox.enabled = true;
    }

    private void DisableParryHitbox()
    {
        parryHitbox.enabled = false;
    }
}