using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using PEventCenter;

public enum PlayerState
{
    Normal,
    RedInfection,
    GreenInfection
}


public class PlayerState_Demo : MonoBehaviour
{
    
    public PlayerState state;
    public Weapon normal;
    public Weapon red;
    public Weapon green;
    private SpriteRenderer sprite;
   // private CharacterHandleWeapon_Demo characterHandleWeapon;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //characterHandleWeapon = GetComponent<CharacterHandleWeapon_Demo>();
        state = PlayerState.Normal;
        sprite.color = Color.white;
        EventCenter.AddListener<PlayerState>(PEventType.SetPlayerState, SetState);
    }

    public void SetState(PlayerState state)
    {
        this.state = state;
        switch(this.state)
        {
            case PlayerState.Normal:
                //
                sprite.color = Color.white;
                //characterHandleWeapon.ChangeWeapon(normal, normal.name);
                break;
            case PlayerState.RedInfection:
                //
                sprite.color = Color.red;
                //characterHandleWeapon.ChangeWeapon(red, red.name);
                break;
            case PlayerState.GreenInfection:
                //
                sprite.color = Color.green;
                //characterHandleWeapon.ChangeWeapon(green, green.name);
                break;
        }


    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<PlayerState>(PEventType.SetPlayerState, SetState);
    }
}
