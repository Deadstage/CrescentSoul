using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField]
    private float maxKnockbackTime = 0.2f;

    [SerializeField] private GameObject damageParticles;

    private ParticleManager ParticleManager { get => particleManager ?? core.GetCoreComponent(ref particleManager); }
    private ParticleManager particleManager;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Stats stats;
    public Stats playerStats;

    private bool isDamageImmune;
    private float damageImmuneStartTime;

    private bool isKnockbackImmune;
    private float knockbackImmuneStartTime;

    [SerializeField]
    private float maxDamageImmuneTime = 1.0f;
    [SerializeField]
    private float maxKnockbackImmuneTime = 1.0f;

    [SerializeField]
    public bool canImmuneDamage;
    [SerializeField]
    public bool canImmuneKnockback;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deflectedSound;


    public override void LogicUpdate()
    {
        CheckKnockback();
        CheckDamageImmune();
        CheckKnockbackImmune();
    }

public void Damage(float amount)
{
    if (isDamageImmune == false)
    {
        // Additional check for Enemy3 in stun state
        Enemy3 enemy = core.GetComponentInParent<Enemy3>();
        
        //Debug.Log($"playerStats is {(playerStats == null ? "null" : "not null")}");
        //Debug.Log($"enemy is {(enemy == null ? "null" : "not null")} and {(enemy != null && !enemy.IsInStunState() ? "not stunned" : "stunned or doesn't exist")}");

        if (enemy != null && !enemy.IsInStunState())
        {
            //Debug.Log("Attempting to attack Enemy3 when it's not stunned by object: " + this.gameObject.name);
            // Play deflected sound
            audioSource.PlayOneShot(deflectedSound);
            //Debug.Log("This is not the player. Resetting player stamina...");
            playerStats?.ResetStamina();  // Reset the player's stamina here
            return; // If enemy is not in stun state, no damage is inflicted
        }

        // Original code...
        Stats?.DecreaseHealth(amount);
        Stats?.ExternalDecreaseStamina(amount);
        ParticleManager?.StartParticlesWithRandomRotation(damageParticles);

        if (canImmuneDamage == true)
        {
            isDamageImmune = true;
            damageImmuneStartTime = Time.time;
        }
    }
}

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        if (isKnockbackImmune == false)
        {
            // Additional check for Enemy3 in stun state
            Enemy3 enemy = core.GetComponentInParent<Enemy3>();
            if (enemy != null)
            {
                if (!enemy.IsInStunState())
                {
                    Movement?.SetVelocity(strength, angle, direction);
                    Movement.CanSetVelocity = false;
                    isKnockbackActive = true;
                    knockbackStartTime = Time.time;

                    if (canImmuneKnockback == true)
                    {
                        isKnockbackImmune = true;
                        knockbackImmuneStartTime = Time.time;
                    }
                }
            }
            else
            {
                Movement?.SetVelocity(strength, angle, direction);
                Movement.CanSetVelocity = false;
                isKnockbackActive = true;
                knockbackStartTime = Time.time;

                if (canImmuneKnockback == true)
                {
                    isKnockbackImmune = true;
                    knockbackImmuneStartTime = Time.time;
                }
            }
        }
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && (Movement?.CurrentVelocity.y <= 0.01f && (CollisionSenses.Ground) || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }

    private void CheckKnockbackImmune()
    {
        if (isKnockbackImmune && Time.time >= knockbackImmuneStartTime + maxKnockbackImmuneTime)
        {
            isKnockbackImmune = false;
        }
    }

    private void CheckDamageImmune()
    {
        if (isDamageImmune && Time.time >= damageImmuneStartTime + maxDamageImmuneTime)
        {
            isDamageImmune = false;
        }
    }
}
