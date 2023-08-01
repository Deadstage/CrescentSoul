using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : Entity, ICollisionHandler
{
    public E6_IdleState idleState { get; private set; }
    public E6_MoveState moveState { get; private set; }
    public E6_PlayerDetectedState playerDetectedState { get; private set; }
    public E6_MeleeAttackState meleeAttackState { get; private set; }
    public E6_LookForPlayerState lookForPlayerState { get; private set; }
    public E6_StunState stunState { get; private set; }
    public E6_DeadState deadState { get; private set; }
    public E6_ChargeState chargeState { get; private set; }
    public E6_EngagedState engagedState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_EngagedState engagedStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public SceneDictionary sceneDictionary;
    private EnemyOriginalPosition enemyOriginalPosition;

    public bool isEngaged = false;

    public override void Awake()
    {
        base.Awake();
        //Debug.Log("Enemy1 Awake");

        moveState = new E6_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E6_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E6_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E6_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E6_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E6_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new E6_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E6_DeadState(this, stateMachine, "dead", deadStateData, this);
        engagedState = new E6_EngagedState(this, stateMachine, "engage", engagedStateData, this);
        

        stateMachine.Initialize(moveState);

        enemyOriginalPosition = GetComponent<EnemyOriginalPosition>();


    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    private void OnEnable()
    {
        this.stateMachine.ChangeState(idleState);
        //Debug.Log("Enemy1 OnEnable");
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (other.CompareTag("Player")) 
        {
            detectedIPlayerState = other.GetComponent<IPlayerState>();

            if (detectedIPlayerState != null)
            {
                var returnedState = detectedIPlayerState?.CurrentPlayerState();
                var collisionSenses = other.GetComponentInChildren<Core>().GetCoreComponent<CollisionSenses>();
                var returnedGrounded = collisionSenses.Ground;

                if (colliderName == "TouchDamageCollider" && other.tag == "Player" && returnedState.GetType() == typeof(PlayerStunState) && returnedGrounded == true)
                {
                    //Debug.Log("Crit Damage!");
                    sceneDictionary.M4HScene();

                    if (isEngaged == true)
                    {
                        //this.gameObject.SetActive(false);
                        EnemyEngaged();
                    }
                }
            }
        }

        if (colliderName == "TouchDamageCollider" && other.tag == "Player")
        {
            //Debug.Log("Damaged!");
            //other.GetComponent<Player>().stats.DecreaseHealth(1);
            if (other.TryGetComponent(out IDamageable damageable)) { damageable.Damage(10); };
            if (other.TryGetComponent(out IKnockbackable knockbackable))
            {
                knockbackable.Knockback(meleeAttackStateData.knockbackAngle, meleeAttackStateData.knockbackStrength, Movement.FacingDirection);
            }
        }
    }

    public void EnemyEngaged()
    {
        //Movement.SetVelocityX(0f);
        this.Movement.SetVelocityX(0f);
        stateMachine.ChangeState(engagedState);
    }

    public void EnemyDisengaged()
    {
        stateMachine.ChangeState(idleState);
    }

    public void EnemyEngager()
    {
        this.isEngaged = true;
    }

    public void EnemyDisengager()
    {
        this.isEngaged = false;
    }

    public void MovementStopper()
    {
        this.Movement.SetVelocityX(0f);
    }
}
