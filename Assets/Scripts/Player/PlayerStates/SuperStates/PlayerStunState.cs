using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerStunState : PlayerState
{
    private Movement movement;
    private Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);

    public PlayerStunState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
        string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    //    SetLayer(10, player.transform);
    }

    public override void Exit()
    {
        base.Exit();
    //    SetLayer(7, player.transform);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.SetVelocityX(0f);

        if (Time.time >= startTime + playerData.stunTime)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    //public void SetLayer(int newLayer, Transform transform)
    //{
    //    transform.gameObject.layer = newLayer;
    //    foreach (Transform child in transform)
    //    {
    //        child.gameObject.layer = newLayer;
    //        if (child.childCount > 0)
    //        {
    //            SetLayer(newLayer, child.transform);
    //        }
    //    }
    //}
}
