using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayerState
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerCrouchAttackState CrouchAttackState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerSecondaryAttackState SecondaryAttackState { get; private set; }
    public PlayerStunState StunState { get; private set; }
    public PlayerEngagedState EngagedState { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    public Stats stats;
    private Stats Stats => stats ? stats : Core.GetCoreComponent(ref stats);

    public Movement movement;
    private Movement Movement => movement ? movement : Core.GetCoreComponent(ref movement);

    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    #endregion

    public SceneDictionary sceneDictionary;

    #region Other Variables

    private Vector2 workspace;

    public bool canFlip = true;
    public bool isEngaged = false;

    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        DashState = new PlayerDashState(this, StateMachine, playerData, "backDash");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        CrouchAttackState = new PlayerCrouchAttackState(this, StateMachine, playerData, "crouchAttack");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerSecondaryAttackState(this, StateMachine, playerData, "secondaryAttack");
        StunState = new PlayerStunState(this, StateMachine, playerData, "stun");
        EngagedState = new PlayerEngagedState(this, StateMachine, playerData, "engage");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Inventory = GetComponent<PlayerInventory>();

        PrimaryAttackState.SetStance(Inventory.stances[(int)CombatInputs.primary]);
        SecondaryAttackState.SetStance(Inventory.stances[(int)CombatInputs.secondary]);
        CrouchAttackState.SetStance(Inventory.stances[(int)CombatInputs.primary]);

        StateMachine.Initialize(IdleState);

        Stats.StaminaZero += () => StateMachine.ChangeState(StunState);
    }

    private void Update()
    {
        if (Core == null)
        {
            Debug.LogError("Core is null");
            return;
        }
        if (StateMachine == null)
        {
            Debug.LogError("StateMachine is null");
            return;
        }
        if (StateMachine.CurrentState == null)
        {
            Debug.LogError("StateMachine.CurrentState is null");
            return;
        }

        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();

        if(isEngaged == true)
        {
            StateMachine.ChangeState(EngagedState);
        }
    }

    private void FixedUpdate()
    {
        if (StateMachine == null)
        {
            Debug.LogError("StateMachine is null");
            return;
        }
        if (StateMachine.CurrentState == null)
        {
            Debug.LogError("StateMachine.CurrentState is null");
            return;
        }

        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    private void AnimationTriggerFunction() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public PlayerState CurrentPlayerState()
    {
        return StateMachine.CurrentState;

    }

    public void PlayerEngaged()
    {
        movement.SetVelocityX(0f);
    }


    #endregion
}
