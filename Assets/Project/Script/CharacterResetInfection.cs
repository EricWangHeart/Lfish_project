using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

using PEventCenter;
public class CharacterResetInfection : CharacterAbility
{
    public MMFeedbacks ResetFeedbacks;
    Health_Demo health;

    protected override void Initialization()
    {
        base.Initialization();
        health = GetComponent<Health_Demo>();
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            health.ResetInfection();
            EventCenter.Broadcast<PlayerState>(PEventType.SetPlayerState, PlayerState.Normal);
        }
    }



}
