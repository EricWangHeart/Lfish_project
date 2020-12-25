using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
public class CharacterHandleSword : CharacterHandleWeapon
{




    protected override void HandleInput()
    {
        if ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) || (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonDown))
        {

            ShootStart();
        }

        if (CurrentWeapon != null)
        {
            if (ContinuousPress && (CurrentWeapon.TriggerMode == Weapon.TriggerModes.Auto) && (_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed))
            {
                ShootStart();
            }
            if (ContinuousPress && (CurrentWeapon.TriggerMode == Weapon.TriggerModes.Auto) && (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonPressed))
            {
                ShootStart();
            }
        }

        if (_inputManager.ReloadButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            Reload();
        }

        if ((_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) || (_inputManager.ShootAxis == MMInput.ButtonStates.ButtonUp))
        {
            ShootStop();
        }

        if (CurrentWeapon != null)
        {
            if ((CurrentWeapon.WeaponState.CurrentState == Weapon.WeaponStates.WeaponDelayBetweenUses)
            && ((_inputManager.ShootAxis == MMInput.ButtonStates.Off) && (_inputManager.ShootButton.State.CurrentState == MMInput.ButtonStates.Off)))
            {
                CurrentWeapon.WeaponInputStop();
            }
        }
    }
}
