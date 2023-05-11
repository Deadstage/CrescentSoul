using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //private AttackDetails attackDetails;

    private float speed;
    private float travelDistance;
    private float xStartPos;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;

    private bool isGravityOn;
    private bool hasHitGround;

    [SerializeField]
    private LayerMask whatIsGround;
    // [SerializeField]
    // private LayerMask whatIsPlayer;
    [SerializeField]
    private Transform damagePosition;

    protected D_RangedAttackState stateData;

    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;

    private Core core;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    public AudioSource projectile;
    public AudioClip glassShatter;

    public float maxDistance = 10f;
    public float volumeMultiplier = 1f;

    public string playerTag = "Player";
    private Transform playerTransform;

    private bool hasHitPlayer = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject with tag '" + playerTag + "' not found.");
        }
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            //attackDetails.position = transform.position;

            if (isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance > maxDistance)
        {
            projectile.volume = 0f;
        }
        else
        {
            float volume = Mathf.SmoothStep(0f, 1f, 1f - (distance / maxDistance)) * volumeMultiplier;
            projectile.volume = volume;
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(damagePosition.position, damageRadius);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);
            Collider2D playerCombatHit = null;

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.CompareTag(playerTag) && hit.transform.parent != null && hit.transform.parent.GetComponent<Core>() != null)
                {
                    playerCombatHit = hit;
                    break;
                }
            }

            if (playerCombatHit && !hasHitPlayer)
            {
                if (playerCombatHit.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(10);
                    hasHitPlayer = true;
                }
                DeactivateProjectile();
            }

            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                DeactivateProjectile();
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }

        }
    }

    private void DeactivateProjectile()
    {
        // Disable components
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        //GetComponent<Collider2D>().enabled = false;

        // Hide renderer
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }

        // Set as inactive
        PlayGlassShatterAndDestroy();
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        //attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }

    public void PlayGlassShatterAndDestroy()
    {   
        projectile.PlayOneShot(glassShatter);
        Destroy(gameObject, 1.0f); // destroy the GameObject with a 0.1 second delay
    }
}