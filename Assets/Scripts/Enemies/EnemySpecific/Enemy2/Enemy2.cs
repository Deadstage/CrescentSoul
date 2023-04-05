using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity, ICollisionHandler
{
    public E2_IdleState idleState { get; private set; }
    public E2_MoveState moveState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_ChargeState chargeState { get; private set; }
    public E2_EngagedState engagedState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;
    [SerializeField]
    private D_EngagedState engagedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;

    public SceneDictionary sceneDictionary;
    private EnemyOriginalPosition enemyOriginalPosition;

    public bool isEngaged = false;


    public override void Awake()
    {
        base.Awake();

        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);
        dodgeState = new E2_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangedAttackState = new E2_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        engagedState = new E2_EngagedState(this, stateMachine, "engage", engagedStateData, this);
        chargeState = new E2_ChargeState(this, stateMachine, "charge", chargeStateData, this);

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
                    Debug.Log("Crit Damage!");
                    sceneDictionary.M1HScene();

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
